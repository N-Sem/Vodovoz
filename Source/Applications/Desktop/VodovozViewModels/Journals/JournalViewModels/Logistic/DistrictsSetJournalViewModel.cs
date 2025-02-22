﻿using System;
using System.Linq;
using NHibernate;
using NHibernate.Transform;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Services;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Sale;
using Vodovoz.EntityRepositories.Employees;
using Vodovoz.EntityRepositories.Sale;
using Vodovoz.JournalNodes;
using Vodovoz.Journals.FilterViewModels;
using Vodovoz.Services;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Logistic;
using Vodovoz.ViewModels.TempAdapters;

namespace Vodovoz.Journals.JournalViewModels
{
	public sealed class DistrictsSetJournalViewModel : FilterableSingleEntityJournalViewModelBase<DistrictsSet, DistrictsSetViewModel, DistrictsSetJournalNode, DistrictsSetJournalFilterViewModel>
	{
		private readonly IDeliveryRulesParametersProvider _deliveryRulesParametersProvider;
		private readonly bool _сanChangeOnlineDeliveriesToday;
		
		public DistrictsSetJournalViewModel(
			DistrictsSetJournalFilterViewModel filterViewModel,
			IUnitOfWorkFactory unitOfWorkFactory,
			ICommonServices commonServices,
			IEmployeeRepository employeeRepository,
			IEntityDeleteWorker entityDeleteWorker,
			IDeliveryScheduleJournalFactory deliveryScheduleJournalFactory,
			IDeliveryRulesParametersProvider deliveryRulesParametersProvider,
			bool hideJournalForOpenDialog = false, 
			bool hideJournalForCreateDialog = false)
			: base(filterViewModel, unitOfWorkFactory, commonServices, hideJournalForOpenDialog, hideJournalForCreateDialog)
		{
			this.entityDeleteWorker = entityDeleteWorker ?? throw new ArgumentNullException(nameof(entityDeleteWorker));
			_deliveryScheduleJournalFactory = deliveryScheduleJournalFactory ?? throw new ArgumentNullException(nameof(deliveryScheduleJournalFactory));
			this.unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
			this.employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
			_deliveryRulesParametersProvider = 
				deliveryRulesParametersProvider ?? throw new ArgumentNullException(nameof(deliveryRulesParametersProvider));
			
			canActivateDistrictsSet = commonServices.CurrentPermissionService.ValidatePresetPermission("can_activate_districts_set");
			var permissionResult = commonServices.CurrentPermissionService.ValidateEntityPermission(typeof(DistrictsSet));
			canCreate = permissionResult.CanCreate;
			canUpdate = permissionResult.CanUpdate;
			_сanChangeOnlineDeliveriesToday = 
				commonServices.CurrentPermissionService.ValidatePresetPermission("can_change_online_deliveries_today");

			TabName = "Журнал версий районов";
			UpdateOnChanges(typeof(DistrictsSet));
			SetIsStoppedOnlineDeliveriesToday();
		}

		private readonly IUnitOfWorkFactory unitOfWorkFactory;
		private readonly IEmployeeRepository employeeRepository;
		private readonly IEntityDeleteWorker entityDeleteWorker;
		private readonly IDeliveryScheduleJournalFactory _deliveryScheduleJournalFactory;
		private readonly bool canUpdate;
		private readonly bool canCreate;
		private readonly bool canActivateDistrictsSet;
		
		private bool IsStoppedOnlineDeliveriesToday { get; set; }

		protected override Func<IUnitOfWork, IQueryOver<DistrictsSet>> ItemsSourceQueryFunction => uow => {
			DistrictsSet districtsSetAlias = null;
			DistrictsSetJournalNode resultAlias = null;
			Employee creatorAlias = null;
			
			var query = uow.Session.QueryOver<DistrictsSet>(() => districtsSetAlias)
				.Left.JoinAlias(() => districtsSetAlias.Author, () => creatorAlias);

			query.Where(GetSearchCriterion(
				() => districtsSetAlias.Name,
				() => districtsSetAlias.Id
			));

			return query
				.SelectList(list => list
				   .Select(x => x.Id).WithAlias(() => resultAlias.Id)
				   .Select(x => x.Name).WithAlias(() => resultAlias.Name)
				   .Select(x => x.Status).WithAlias(() => resultAlias.Status)
				   .Select(x => x.DateCreated).WithAlias(() => resultAlias.DateCreated)
				   .Select(x => x.DateActivated).WithAlias(() => resultAlias.DateActivated)
				   .Select(x => x.DateClosed).WithAlias(() => resultAlias.DateClosed)
				   .Select(x => x.Comment).WithAlias(() => resultAlias.Comment)
				   .Select(() => creatorAlias.Name).WithAlias(() => resultAlias.AuthorName)
				   .Select(() => creatorAlias.LastName).WithAlias(() => resultAlias.AuthorLastName)
				   .Select(() => creatorAlias.Patronymic).WithAlias(() => resultAlias.AuthorPatronymic)
				)
				.TransformUsing(Transformers.AliasToBean<DistrictsSetJournalNode>());
		};

		protected override Func<DistrictsSetViewModel> CreateDialogFunction => () =>
			new DistrictsSetViewModel(
				EntityUoWBuilder.ForCreate(),
				unitOfWorkFactory,
				commonServices,
				entityDeleteWorker,
				employeeRepository,
				new DistrictRuleRepository(),
				_deliveryScheduleJournalFactory);

		protected override Func<DistrictsSetJournalNode, DistrictsSetViewModel> OpenDialogFunction => node =>
			new DistrictsSetViewModel(
				EntityUoWBuilder.ForOpen(node.Id),
				unitOfWorkFactory,
				commonServices,
				entityDeleteWorker,
				employeeRepository,
				new DistrictRuleRepository(),
				_deliveryScheduleJournalFactory);
		
		protected override void CreateNodeActions()
		{
			NodeActionsList.Clear();
			CreateDefaultSelectAction();
			CreateDefaultAddActions();
			CreateDefaultEditAction();
			CreateCopyAction();
			
			CreateStartOnlineDeliveriesTodayAction();
			CreateStopOnlineDeliveriesTodayAction();
			
			if(commonServices.UserService.GetCurrentUser(UoW).IsAdmin) {
				CreateDefaultDeleteAction();
			}
		}

		private void CreateCopyAction()
		{
			var copyAction = new JournalAction("Копировать",
				selectedItems => canCreate && selectedItems.OfType<DistrictsSetJournalNode>().FirstOrDefault() != null,
				selected => true,
				selected => {
					var selectedNode = selected.OfType<DistrictsSetJournalNode>().FirstOrDefault();
					if(selectedNode == null)
						return;
					var districtsSetToCopy = UoW.GetById<DistrictsSet>(selectedNode.Id);
					var alreadyCopiedDistrict = UoW.Session.QueryOver<District>().WhereRestrictionOn(x => x.CopyOf.Id).IsIn(districtsSetToCopy.Districts.Select(x => x.Id).ToArray()).Take(1).SingleOrDefault();
					if(alreadyCopiedDistrict != null) {
						commonServices.InteractiveService.ShowMessage(ImportanceLevel.Warning,
							$"Выбранная версия районов уже была скопирована\nКопия: (Код: {alreadyCopiedDistrict.DistrictsSet.Id}) {alreadyCopiedDistrict.DistrictsSet.Name}");
						return;
					}
					
					if(commonServices.InteractiveService.Question($"Скопировать версию районов \"{selectedNode.Name}\"")) {
						var copy = (DistrictsSet)districtsSetToCopy.Clone();
						copy.Name += " - копия";
						if(copy.Name.Length > DistrictsSet.NameMaxLength)
						{
							copy.Name = copy.Name.Remove(DistrictsSet.NameMaxLength);
						}
						copy.Author = employeeRepository.GetEmployeeForCurrentUser(UoW);
						copy.Status = DistrictsSetStatus.Draft;
						copy.DateCreated = DateTime.Now;
						
						UoW.Save(copy);
						UoW.Commit();
						commonServices.InteractiveService.ShowMessage(ImportanceLevel.Info, "Копирование завершено");
						Refresh();
					}
				}
			);
			NodeActionsList.Add(copyAction);
		}

		private void CreateStartOnlineDeliveriesTodayAction()
		{
			var startOnlinesAction = new JournalAction("Запустить онлайны",
				selectedItems => true,
				selected => _сanChangeOnlineDeliveriesToday && IsStoppedOnlineDeliveriesToday,
				selected => 
				{
					_deliveryRulesParametersProvider.UpdateOnlineDeliveriesTodayParameter("false");
					SetIsStoppedOnlineDeliveriesToday();
					UpdateJournalActions?.Invoke();
				}
			);
			NodeActionsList.Add(startOnlinesAction);
		}
		
		private void CreateStopOnlineDeliveriesTodayAction()
		{
			var stopOnlinesAction = new JournalAction("Остановить онлайны",
				selectedItems => true,
				selected => _сanChangeOnlineDeliveriesToday && !IsStoppedOnlineDeliveriesToday,
				selected => 
				{
					_deliveryRulesParametersProvider.UpdateOnlineDeliveriesTodayParameter("true");
					SetIsStoppedOnlineDeliveriesToday();
					UpdateJournalActions?.Invoke();
				}
			);
			NodeActionsList.Add(stopOnlinesAction);
		}
		
		private void SetIsStoppedOnlineDeliveriesToday()
		{
			IsStoppedOnlineDeliveriesToday = _deliveryRulesParametersProvider.IsStoppedOnlineDeliveriesToday;
		}

		protected override void CreatePopupActions()
		{
			PopupActionsList.Clear();
			CreateActivateNodeAction();
			CreateCloseNodeAction();
			CreateToDraftNodeAction();
		}

		private void CreateActivateNodeAction()
		{
			PopupActionsList.Add(
				new JournalAction(
					"Активировать",
					selectedItems => canActivateDistrictsSet && canUpdate
						&& selectedItems.OfType<DistrictsSetJournalNode>().FirstOrDefault()?.Status == DistrictsSetStatus.Draft,
					selectedItems => true,
					selectedItems => {
						var selectedNodes = selectedItems.OfType<DistrictsSetJournalNode>();
						var selectedNode = selectedNodes.FirstOrDefault();
						if(selectedNode == null)
							return;
						var activeDistrictsSet = UoW.Session.QueryOver<DistrictsSet>().Where(x => x.Status == DistrictsSetStatus.Active).Take(1).SingleOrDefault();
						if(activeDistrictsSet?.DateCreated > selectedNode.DateCreated) {
							commonServices.InteractiveService.ShowMessage(ImportanceLevel.Warning, "Нельзя активировать, так как дата создания выбранной версии меньше чем дата создания активной версии");
							return;
						}
						var selectedDistrictsSet = UoW.GetById<DistrictsSet>(selectedNode.Id);
						if(selectedDistrictsSet.Districts.All(x => x.CopyOf == null) 
							&& !commonServices.InteractiveService.Question("Для выбранной версии невозможно перенести все приоритеты работы водителей\nПродолжить?")) {
							return;
						}
						TabParent.AddSlaveTab(this, 
							new DistrictsSetActivationViewModel(
								EntityUoWBuilder.ForOpen(selectedNode.Id),
								QS.DomainModel.UoW.UnitOfWorkFactory.GetDefaultFactory,
								commonServices,
								new EmployeeRepository()
								)
						);
						
					}
				)
			);
		}
		
		private void CreateCloseNodeAction()
		{
			PopupActionsList.Add(
				new JournalAction(
					"Закрыть",
					selectedItems => canUpdate &&
						selectedItems.OfType<DistrictsSetJournalNode>().FirstOrDefault()?.Status == DistrictsSetStatus.Draft,
					selectedItems => true,
					selectedItems => {
						var selectedNodes = selectedItems.OfType<DistrictsSetJournalNode>();
						var selectedNode = selectedNodes.FirstOrDefault();
						if(selectedNode != null) {
							var districtsSet = UoW.GetById<DistrictsSet>(selectedNode.Id);
							if(districtsSet != null) {
								districtsSet.Status = DistrictsSetStatus.Closed;
								districtsSet.DateClosed = DateTime.Now;
								districtsSet.DateActivated = null;
								UoW.Save(districtsSet);
								UoW.Commit();
								Refresh();
							}
						}
					}
				)
			);
		}
		
		private void CreateToDraftNodeAction()
		{
			PopupActionsList.Add(
				new JournalAction(
					"В черновик",
					selectedItems => canUpdate &&
						selectedItems.OfType<DistrictsSetJournalNode>().FirstOrDefault()?.Status == DistrictsSetStatus.Closed,
					selectedItems => true,
					selectedItems => {
						var selectedNodes = selectedItems.OfType<DistrictsSetJournalNode>();
						var selectedNode = selectedNodes.FirstOrDefault();
						if(selectedNode != null) {
							var districtsSet = UoW.GetById<DistrictsSet>(selectedNode.Id);
							if(districtsSet != null) {
								districtsSet.Status = DistrictsSetStatus.Draft;
								districtsSet.DateClosed = null;
								districtsSet.DateActivated = null;
								UoW.Save(districtsSet);
								UoW.Commit();
								Refresh();
							}
						}
					}
				)
			);
		}
		
	}
}
