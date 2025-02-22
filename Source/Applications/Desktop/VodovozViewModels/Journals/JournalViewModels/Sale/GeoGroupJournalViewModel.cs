﻿using NHibernate;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Services;
using System;
using Vodovoz.Domain.Sale;
using Vodovoz.Models;
using Vodovoz.ViewModels.Dialogs.Sales;
using Vodovoz.ViewModels.Journals.JournalFactories;
using Vodovoz.ViewModels.Journals.JournalNodes;

namespace Vodovoz.ViewModels.Journals.JournalViewModels.Sale
{
	public class GeoGroupJournalViewModel : SingleEntityJournalViewModelBase<GeoGroup, GeoGroupViewModel, GeoGroupJournalNode>
	{
		private readonly ISubdivisionJournalFactory _subdivisionJournalFactory;
		private readonly WarehouseJournalFactory _warehouseJournalFactory;
		private readonly GeoGroupVersionsModel _geoGroupVersionsModel;

		public GeoGroupJournalViewModel(
			IUnitOfWorkFactory unitOfWorkFactory,
			ICommonServices commonServices,
			ISubdivisionJournalFactory subdivisionJournalFactory,
			WarehouseJournalFactory warehouseJournalFactory,
			GeoGroupVersionsModel geoGroupVersionsModel,
			bool hideJournalForOpenDialog = false,
			bool hideJournalForCreateDialog = false
		) : base(unitOfWorkFactory, commonServices, hideJournalForOpenDialog, hideJournalForCreateDialog)
		{
			_subdivisionJournalFactory = subdivisionJournalFactory ?? throw new ArgumentNullException(nameof(subdivisionJournalFactory));
			_warehouseJournalFactory = warehouseJournalFactory ?? throw new ArgumentNullException(nameof(warehouseJournalFactory));
			_geoGroupVersionsModel = geoGroupVersionsModel ?? throw new ArgumentNullException(nameof(geoGroupVersionsModel));

			Title = "Части города";
		}

		public void DisableChangeEntityActions()
		{
			NodeActionsList.Clear();
			CreateDefaultSelectAction();
		}

		protected override Func<IUnitOfWork, IQueryOver<GeoGroup>> ItemsSourceQueryFunction => uow => {
			GeoGroup geoGroupAlias = null;
			GeoGroupJournalNode resultAlias = null;

			var query = uow.Session.QueryOver(() => geoGroupAlias);

			query.Where(GetSearchCriterion(
				() => geoGroupAlias.Name,
				() => geoGroupAlias.Id
			));

			return query
				.SelectList(list => list
				   .Select(x => x.Id).WithAlias(() => resultAlias.Id)
				   .Select(x => x.Name).WithAlias(() => resultAlias.Name)
				)
				.TransformUsing(Transformers.AliasToBean<GeoGroupJournalNode>());
		};

		protected override Func<GeoGroupViewModel> CreateDialogFunction => () =>
			new GeoGroupViewModel(
				EntityUoWBuilder.ForCreate(),
				UnitOfWorkFactory,
				_geoGroupVersionsModel,
				_subdivisionJournalFactory,
				_warehouseJournalFactory,
				commonServices
			);

		protected override Func<GeoGroupJournalNode, GeoGroupViewModel> OpenDialogFunction => node =>
			new GeoGroupViewModel(
				EntityUoWBuilder.ForOpen(node.Id),
				UnitOfWorkFactory,
				_geoGroupVersionsModel,
				_subdivisionJournalFactory,
				_warehouseJournalFactory,
				commonServices
			);
	}
}
