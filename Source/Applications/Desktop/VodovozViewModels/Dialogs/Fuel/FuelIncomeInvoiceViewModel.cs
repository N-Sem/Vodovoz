﻿using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using QS.Commands;
using QS.DomainModel.Entity;
using QS.DomainModel.NotifyChange;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Project.Journal.EntitySelector;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Fuel;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Logistic;
using Vodovoz.EntityRepositories.Fuel;
using Vodovoz.EntityRepositories.Subdivisions;
using Vodovoz.Infrastructure.Services;
using Vodovoz.Services;
using Vodovoz.TempAdapters;

namespace Vodovoz.ViewModels.Dialogs.Fuel
{
	public class FuelIncomeInvoiceViewModel : EntityTabViewModelBase<FuelIncomeInvoice>
	{
		private readonly IEmployeeService employeeService;
		private readonly INomenclatureJournalFactory nomenclatureSelectorFactory;
		private readonly ISubdivisionRepository subdivisionRepository;
		private readonly IFuelRepository fuelRepository;
		private readonly ICounterpartyJournalFactory counterpartyJournalFactory;

		public FuelIncomeInvoiceViewModel
		(
			IEntityUoWBuilder uowBuilder,
			IUnitOfWorkFactory unitOfWorkFactory,
			IEmployeeService employeeService,
			INomenclatureJournalFactory nomenclatureSelectorFactory,
			ISubdivisionRepository subdivisionRepository,
			IFuelRepository fuelRepository,
			ICounterpartyJournalFactory counterpartyJournalFactory,
			ICommonServices commonServices
		) : base(uowBuilder, unitOfWorkFactory, commonServices)
		{
			this.employeeService = employeeService;
			this.nomenclatureSelectorFactory = nomenclatureSelectorFactory ?? throw new ArgumentNullException(nameof(nomenclatureSelectorFactory));
			this.subdivisionRepository = subdivisionRepository ?? throw new ArgumentNullException(nameof(subdivisionRepository));
			this.fuelRepository = fuelRepository ?? throw new ArgumentNullException(nameof(fuelRepository));
			this.counterpartyJournalFactory = counterpartyJournalFactory ?? throw new ArgumentNullException(nameof(counterpartyJournalFactory));
			TabName = "Входящая накладная по топливу";

			if(CurrentEmployee == null) {
				AbortOpening("К вашему пользователю не привязан сотрудник, невозможно открыть документ");
			}

			if(uowBuilder.IsNewEntity) {
				Entity.СreationTime = DateTime.Now;
				Entity.Author = CurrentEmployee;
			}

			FuelBalanceViewModel = new FuelBalanceViewModel(subdivisionRepository, fuelRepository);

			CreateCommands();
			ConfigEntityUpdateSubscribes();
			ConfigureEntityPropertyChanges();
			UpdateCashSubdivisions();
			UpdateBalanceCache();
			ValidationContext.ServiceContainer.AddService(typeof(IFuelRepository), fuelRepository);

			ConfigureEntries();
		}

		private void ConfigEntityUpdateSubscribes()
		{
			NotifyConfiguration.Instance.BatchSubscribeOnEntity<FuelTransferDocument, FuelIncomeInvoice>(OnExternalUpdate);
		}

		private void ConfigureEntityPropertyChanges()
		{
			OnEntityPropertyChanged(UpdateBalanceCache,
				e => e.Subdivision
			);
		}

		void OnExternalUpdate(EntityChangeEvent[] changeEvents)
		{
			UpdateBalanceCache();
		}

		protected override bool BeforeSave()
		{
			Entity.UpdateOperations(fuelRepository);
			return base.BeforeSave();
		}

		#region Properties

		public FuelBalanceViewModel FuelBalanceViewModel { get; }

		private Employee currentEmployee;
		public Employee CurrentEmployee {
			get {
				if(currentEmployee == null) {
					currentEmployee = employeeService.GetEmployeeForUser(UoW, UserService.CurrentUserId);
				}
				return currentEmployee;
			}
		}

		private FuelIncomeInvoiceItem selectedItem;
		[PropertyChangedAlso(nameof(CanDeleteItems))]
		public virtual FuelIncomeInvoiceItem SelectedItem {
			get => selectedItem;
			set => SetField(ref selectedItem, value, () => SelectedItem);
		}

		public bool CanEdit => PermissionResult.CanUpdate;
		public bool CanAddItems => CanEdit;
		public bool CanDeleteItems => CanEdit && SelectedItem != null;

		#endregion Properties

		#region Commands
		public DelegateCommand AddItemCommand { get; private set; }
		public DelegateCommand DeleteItemCommand { get; private set; }

		private void CreateCommands()
		{
			AddItemCommand = new DelegateCommand(
				() =>
				{
					var selector = nomenclatureSelectorFactory.CreateNomenclatureSelectorForFuelSelect();
					selector.OnEntitySelectedResult += (sender, args) =>
					{
						if(args.SelectedNodes == null || !args.SelectedNodes.Any()){
							return;
						}

						var nomenclatures = UoW.GetById<Nomenclature>(args.SelectedNodes.Select(x => x.Id).ToArray());
						foreach(var nomenclature in nomenclatures) {
							Entity.AddItem(nomenclature);
						}
					};
					TabParent.AddTab(selector, this);
				},
				() => { return CanAddItems; }
			);

			DeleteItemCommand = new DelegateCommand(
				() => { Entity.DeleteItem(SelectedItem); },
				() => { return CanDeleteItems; }
			);
		}

		#endregion Commands

		#region Entries

		private void ConfigureEntries()
		{
			CounterpartySelectorFactory = counterpartyJournalFactory.CreateCounterpartyAutocompleteSelectorFactory();
		}

		public IEntityAutocompleteSelectorFactory CounterpartySelectorFactory { get; private set; }

		#endregion Entries

		#region Настройка списков доступных подразделений кассы

		public IEnumerable<Subdivision> AvailableSubdivisions { get; private set; }

		private void UpdateCashSubdivisions()
		{
			AvailableSubdivisions = subdivisionRepository.GetCashSubdivisionsAvailableForUser(UoW, CurrentUser);
		}

		#endregion Настройка списков доступных подразделений кассы

		#region FuelBalance

		private Dictionary<FuelType, decimal> fuelBalanceCache;

		private void UpdateBalanceCache()
		{
			if(fuelBalanceCache == null) {
				fuelBalanceCache = new Dictionary<FuelType, decimal>();
			}
			fuelBalanceCache.Clear();
			if(Entity.Subdivision == null) {
				return;
			}
			using(var localUoW = UnitOfWorkFactory.CreateWithoutRoot()) {
				fuelBalanceCache = fuelRepository.GetAllFuelsBalanceForSubdivision(UoW, Entity.Subdivision);
			}
		}

		private decimal GetAvailableLiters(FuelType fuelType)
		{
			if(fuelBalanceCache == null) {
				UpdateBalanceCache();
			}
			if(!fuelBalanceCache.ContainsKey(fuelType)) {
				return 0m;
			}
			return fuelBalanceCache[fuelType];
		}

		private Dictionary<int, decimal> baseItemsLitersCache = new Dictionary<int, decimal>();

		public decimal GetMinimumAvailableLiters(FuelIncomeInvoiceItem item)
		{
			if(item?.Nomenclature?.FuelType == null) {
				return 0m;
			}
			decimal currentLiters = item.Liters;
			if(!UoW.IsNew && item.Id > 0 ) {
				if(baseItemsLitersCache.ContainsKey(item.Id)) {
					currentLiters = baseItemsLitersCache[item.Id];
				} else {
					using(var localUoW = UnitOfWorkFactory.CreateWithoutRoot()) {
						FuelIncomeInvoiceItem invoiceItem = localUoW.GetById<FuelIncomeInvoiceItem>(item.Id);
						if(invoiceItem != null) {
							currentLiters = invoiceItem.Liters;
							baseItemsLitersCache.Add(invoiceItem.Id, invoiceItem.Liters);
						}
					}
				}
			}

			decimal result = currentLiters - GetAvailableLiters(item.Nomenclature.FuelType);
			return result < 0 ? 0 : result;
		}

		#endregion FuelBalance

		public override void Dispose()
		{
			NotifyConfiguration.Instance.UnsubscribeAll(this);
			base.Dispose();
		}
	}
}
