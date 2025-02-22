﻿using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Project.Journal.EntitySelector;
using QS.Project.Services;
using Vodovoz.Domain.Cash;
using Vodovoz.Domain.Employees;
using Vodovoz.EntityRepositories.Cash;
using Vodovoz.Parameters;
using Vodovoz.ViewModels.Journals.FilterViewModels;
using Vodovoz.ViewModels.Journals.FilterViewModels.Employees;
using Vodovoz.ViewModels.Journals.JournalViewModels.Cash;
using Vodovoz.ViewModels.TempAdapters;
using Vodovoz.ViewModels.ViewModels.Cash;
using VodovozInfrastructure.Interfaces;

namespace Vodovoz.TempAdapters
{
	public class ExpenseCategorySelectorFactory : IExpenseCategorySelectorFactory
	{
		private readonly IEnumerable<int> _excludedIds;

		public ExpenseCategorySelectorFactory()
		{
			using(var uow =
				UnitOfWorkFactory.CreateWithoutRoot($"Фабрика статьи расхода {nameof(ExpenseCategorySelectorFactory)}"))
			{
				_excludedIds = new CategoryRepository(new ParametersProvider()).ExpenseSelfDeliveryCategories(uow).Select(x => x.Id);
			}
		}

		public IEntityAutocompleteSelectorFactory CreateSimpleExpenseCategoryAutocompleteSelectorFactory()
		{
			var subdivisionJournalFactory = new SubdivisionJournalFactory();
			var expenseFactory = new ExpenseCategorySelectorFactory();

			return new SimpleEntitySelectorFactory<ExpenseCategory, ExpenseCategoryViewModel>(() =>
			{
				var expenseCategoryFilter = new ExpenseCategoryJournalFilterViewModel();
				expenseCategoryFilter.ExcludedIds = _excludedIds;
				expenseCategoryFilter.HidenByDefault = true;

				var employeeFilter = new EmployeeFilterViewModel
				{
					Status = EmployeeStatus.IsWorking
				};

				var employeeJournalFactory = new EmployeeJournalFactory(employeeFilter);
				
				var journal = new SimpleEntityJournalViewModel<ExpenseCategory, ExpenseCategoryViewModel>(
					x => x.Name,
					() => new ExpenseCategoryViewModel(
						EntityUoWBuilder.ForCreate(),
						UnitOfWorkFactory.GetDefaultFactory,
						ServicesConfig.CommonServices,
						employeeJournalFactory,
						subdivisionJournalFactory,
						expenseFactory
					),
					node => new ExpenseCategoryViewModel(
						EntityUoWBuilder.ForOpen(node.Id),
						UnitOfWorkFactory.GetDefaultFactory,
						ServicesConfig.CommonServices,
						employeeJournalFactory,
						subdivisionJournalFactory,
						expenseFactory
					),
					UnitOfWorkFactory.GetDefaultFactory,
					ServicesConfig.CommonServices
					);
				journal.SelectionMode = JournalSelectionMode.Single;
				journal.SetFilter(expenseCategoryFilter,
					filter =>
					{
						if(filter.ShowArchive)
						{
							return Restrictions.Not(Restrictions.In("Id", filter.ExcludedIds.ToArray()));
						}

						return Restrictions.Conjunction()
							.Add(Restrictions.Not(Restrictions.In("Id", filter.ExcludedIds.ToArray())))
							.Add(Restrictions.Eq(
								Projections.Property<ExpenseCategory>(c => c.IsArchive), filter.ShowArchive));
					});
				return journal;
			});
		}

		public IEntityAutocompleteSelectorFactory CreateDefaultExpenseCategoryAutocompleteSelectorFactory()
		{
			IFileChooserProvider chooserProvider = new FileChooser();
			var subdivisionJournalFactory = new SubdivisionJournalFactory();
			var expenseFactory = new ExpenseCategorySelectorFactory();

			return new EntityAutocompleteSelectorFactory<ExpenseCategoryJournalViewModel>(
				typeof(ExpenseCategory),
				() =>
				{
					var expenseCategoryFilter = new ExpenseCategoryJournalFilterViewModel();
					expenseCategoryFilter.ExcludedIds = _excludedIds;
					expenseCategoryFilter.HidenByDefault = true;

					var employeeFilter = new EmployeeFilterViewModel
					{
						Status = EmployeeStatus.IsWorking
					};
					
					var employeeJournalFactory = new EmployeeJournalFactory(employeeFilter);

					return new ExpenseCategoryJournalViewModel(
						expenseCategoryFilter,
						UnitOfWorkFactory.GetDefaultFactory,
						ServicesConfig.CommonServices,
						chooserProvider,
						employeeJournalFactory,
						subdivisionJournalFactory,
						expenseFactory)
					{
						SelectionMode = JournalSelectionMode.Single
					};
				});
		}
	}
}
