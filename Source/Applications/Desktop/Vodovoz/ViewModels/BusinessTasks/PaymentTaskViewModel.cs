﻿using System;
using QS.Commands;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Project.Journal.EntitySelector;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.BusinessTasks;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.Employees;
using Vodovoz.Filters.ViewModels;
using Vodovoz.Journals.JournalViewModels;
using Vodovoz.Journals.JournalViewModels.Organizations;
using Vodovoz.FilterViewModels.Organization;
using Vodovoz.JournalViewModels;
using Vodovoz.TempAdapters;

namespace Vodovoz.ViewModels.BusinessTasks
{
	public class PaymentTaskViewModel : EntityTabViewModelBase<PaymentTask>
	{
		public IEntityAutocompleteSelectorFactory CounterpartySelectorFactory { get; private set; }
		public IEntityAutocompleteSelectorFactory EmployeeSelectorFactory { get; private set; }
		public IEntityAutocompleteSelectorFactory OrderSelectorFactory { get; private set; }
		public IEntityAutocompleteSelectorFactory SubdivisionSelectorFactory { get; private set; }

		public readonly IEmployeeRepository employeeRepository;

		public PaymentTaskViewModel(
			IEmployeeRepository employeeRepository,
			IEntityUoWBuilder uowBuilder,
			IUnitOfWorkFactory unitOfWorkFactory,
			ICommonServices commonServices) : base(uowBuilder, unitOfWorkFactory, commonServices)
		{
			if(uowBuilder.IsNewEntity) {
				TabName = "Новая задача";
				Entity.CreationDate = DateTime.Now;
				Entity.Source = Domain.BusinessTasks.TaskSource.Handmade;
				Entity.TaskCreator = employeeRepository.GetEmployeeForCurrentUser(UoW);
				Entity.EndActivePeriod = DateTime.Now;
			} else {
				TabName = Entity.Counterparty?.Name;
			}

			this.employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));

			Initialize();
			CreateCommands();
		}

		private void Initialize()
		{
			CounterpartySelectorFactory = new DefaultEntityAutocompleteSelectorFactory<Counterparty,
																						CounterpartyJournalViewModel,
																						CounterpartyJournalFilterViewModel>(CommonServices);

			EmployeeSelectorFactory = new EmployeeJournalFactory().CreateWorkingOfficeEmployeeAutocompleteSelectorFactory();

			OrderSelectorFactory = new DefaultEntityAutocompleteSelectorFactory<Order,
																				OrderJournalViewModel,
																				OrderJournalFilterViewModel>(CommonServices);
		}

		private void CreateCommands()
		{
			CreateSaveCommand();
			CreateCancelCommand();
		}

		public DelegateCommand SaveCommand { get; private set; }
		private void CreateSaveCommand()
		{
			SaveCommand = new DelegateCommand(
				() => Save(true),
				() => true
			);
		}

		public DelegateCommand CancelCommand { get; private set; }
		private void CreateCancelCommand()
		{
			CancelCommand = new DelegateCommand(
				() => Close(true, QS.Navigation.CloseSource.Cancel),
				() => true
			);
		}
	}
}
