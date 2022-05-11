﻿using NHibernate;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Services;
using QS.ViewModels;
using System;
using System.Linq;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Organizations;
using Vodovoz.Factories;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Commands;
using Vodovoz.ViewModels.Dialogs.Organizations;
using Vodovoz.ViewModels.Journals.JournalNodes;

namespace Vodovoz.ViewModels.Journals.JournalViewModels.Goods
{
	public class RoboatsWaterTypeJournalViewModel : SingleEntityJournalViewModelBase<RoboatsWaterType, RoboatsWaterTypeViewModel, RoboatsWaterTypeJournalNode>
	{
		private readonly RoboatsViewModelFactory _roboatsViewModelFactory;
		private readonly INomenclatureSelectorFactory _nomenclatureSelectorFactory;
		private readonly ICommonServices _commonServices;
		private OpenViewModelCommand _openViewModelCommand;

		public RoboatsWaterTypeJournalViewModel(
			IUnitOfWorkFactory unitOfWorkFactory,
			RoboatsViewModelFactory roboatsViewModelFactory,
			INomenclatureSelectorFactory nomenclatureSelectorFactory,
			ICommonServices commonServices
		) : base(unitOfWorkFactory, commonServices)
		{
			_roboatsViewModelFactory = roboatsViewModelFactory ?? throw new ArgumentNullException(nameof(roboatsViewModelFactory));
			_nomenclatureSelectorFactory = nomenclatureSelectorFactory ?? throw new ArgumentNullException(nameof(nomenclatureSelectorFactory));
			_commonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));

			TabName = "Вода для Roboats";

			UpdateOnChanges(
				typeof(RoboatsWaterType)
			);
		}

		public void SetForRoboatsCatalogExport(OpenViewModelCommand openViewModelCommand)
		{
			_openViewModelCommand = openViewModelCommand;
			UpdateActions();
		}

		private void UpdateActions()
		{
			if(_openViewModelCommand != null)
			{
				NodeActionsList.Clear();
				CreateActivateAction();
				CreateAddAction();
				CreateOpenAction();
				CreateDefaultDeleteAction();

				return;
			}

			CreateNodeActions();
		}

		private void CreateActivateAction()
		{
			var selectAction = new JournalAction("Выбрать",
				(selected) => selected.Any(),
				(selected) => SelectionMode != JournalSelectionMode.None,
				(selected) => OnItemsSelected(selected)
			);
			RowActivatedAction = selectAction;
		}

		private void CreateAddAction()
		{
			if(!EntityConfigs.Any())
			{
				return;
			}

			var entityConfig = EntityConfigs.First().Value;
			var action = new JournalAction("Добавить",
				(selected) => entityConfig.PermissionResult.CanCreate,
				(selected) => true,
				(selected) => {
					var docConfig = entityConfig.EntityDocumentConfigurations.First();
					var viewModel = docConfig.GetCreateEntityDlgConfigs().First().OpenEntityDialogFunction() as ViewModelBase;
					if(_openViewModelCommand.CanExecute(viewModel))
					{
						_openViewModelCommand.Execute(viewModel);
					}
				},
				"Insert"
				);
			NodeActionsList.Add(action);
		}

		private void CreateOpenAction()
		{
			if(!EntityConfigs.Any())
			{
				return;
			}

			var entityConfig = EntityConfigs.First().Value;

			string actionName = entityConfig.PermissionResult.CanRead && !entityConfig.PermissionResult.CanUpdate ? "Открыть" : "Изменить";
			bool canOpen = entityConfig.PermissionResult.CanRead || entityConfig.PermissionResult.CanUpdate;

			var action = new JournalAction(actionName,
				(selected) => canOpen && selected.Any(),
				(selected) => true,
				(selected) => {
					var selectedNode = selected.FirstOrDefault() as RoboatsWaterTypeJournalNode;
					var docConfig = entityConfig.EntityDocumentConfigurations.First();
					var viewModel = docConfig.GetOpenEntityDlgFunction().Invoke(selectedNode) as ViewModelBase;
					if(_openViewModelCommand.CanExecute(viewModel))
					{
						_openViewModelCommand.Execute(viewModel);
					}
				},
				"Return"
				);
			NodeActionsList.Add(action);
		}

		protected override Func<IUnitOfWork, IQueryOver<RoboatsWaterType>> ItemsSourceQueryFunction => (uow) => {
			RoboatsWaterType roboatsWaterTypeAlias = null;
			Nomenclature nomenclatureAlias = null;
			RoboatsWaterTypeJournalNode resultAlias = null;

			var itemsQuery = uow.Session.QueryOver(() => roboatsWaterTypeAlias)
				.Left.JoinAlias(() => roboatsWaterTypeAlias.Nomenclature, () => nomenclatureAlias);

			itemsQuery.Where(
				GetSearchCriterion(
					() => nomenclatureAlias.Name,
					() => roboatsWaterTypeAlias.Id
				)
			);

			itemsQuery
				.SelectList(list => list
					.Select(() => roboatsWaterTypeAlias.Id).WithAlias(() => resultAlias.Id)
					.Select(() => nomenclatureAlias.Name).WithAlias(() => resultAlias.Nomenclature)
					.Select(() => roboatsWaterTypeAlias.RoboatsAudiofile).WithAlias(() => resultAlias.RoboatsAudioFileName)
				)
				.TransformUsing(Transformers.AliasToBean<RoboatsWaterTypeJournalNode>());

			return itemsQuery;
		};

		protected override Func<RoboatsWaterTypeViewModel> CreateDialogFunction => () =>
			new RoboatsWaterTypeViewModel(EntityUoWBuilder.ForCreate(), _nomenclatureSelectorFactory, _roboatsViewModelFactory, _commonServices);

		protected override Func<RoboatsWaterTypeJournalNode, RoboatsWaterTypeViewModel> OpenDialogFunction => (node) =>
			new RoboatsWaterTypeViewModel(EntityUoWBuilder.ForOpen(node.Id), _nomenclatureSelectorFactory, _roboatsViewModelFactory, _commonServices);
	}
}
