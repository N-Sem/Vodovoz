using NHibernate;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using Vodovoz.Domain.Permissions.Warehouses;
using Vodovoz.Domain.Store;
using Vodovoz.EntityRepositories.Subdivisions;
using Vodovoz.ViewModels.Journals.JournalNodes;
using Vodovoz.ViewModels.Warehouses;

namespace Vodovoz.ViewModels.Journals.JournalViewModels.Store
{
	public class WarehouseJournalViewModel : SingleEntityJournalViewModelBase<Warehouse, WarehouseViewModel, WarehouseJournalNode>
	{
		private readonly ISubdivisionRepository _subdivisionRepository;
		private readonly WarehouseJournalFilterViewModel _filterViewModel;
		private WarehousePermissionsType[] _warehousePermissions;
		
		public WarehouseJournalViewModel(
			IUnitOfWorkFactory unitOfWorkFactory,
			ICommonServices commonServices,
			ISubdivisionRepository subdivisionRepository,
			WarehouseJournalFilterViewModel filterViewModel = null)
				: base(unitOfWorkFactory, commonServices)
		{
			TabName = "Журнал складов";
			_subdivisionRepository = subdivisionRepository ?? throw new ArgumentNullException(nameof(subdivisionRepository));
			_filterViewModel = filterViewModel;
			_warehousePermissions = new[] { WarehousePermissionsType.WarehouseView };

			UpdateOnChanges(
				typeof(Warehouse)
			);
		}

		protected override void CreateNodeActions()
		{
			NodeActionsList.Clear();
			CreateDefaultSelectAction();
			CreateDefaultEditAction();
			CreateDefaultAddActions();
		}

		protected override Func<IUnitOfWork, IQueryOver<Warehouse>> ItemsSourceQueryFunction => (uow) => {
			Warehouse warehouseAlias = null;
			WarehouseJournalNode warehouseNodeAlias = null;

			var query = uow.Session.QueryOver<Warehouse>(() => warehouseAlias).WhereNot(w => w.IsArchive);
			var disjunction = new Disjunction();

			if(_filterViewModel?.ExcludeWarehousesIds != null)
			{
				query.WhereRestrictionOn(x => x.Id).Not.IsIn(_filterViewModel.ExcludeWarehousesIds);
			}
			
			if(_filterViewModel?.IncludeWarehouseIds != null)
			{
				query.WhereRestrictionOn(x => x.Id).IsInG(_filterViewModel.IncludeWarehouseIds);
			}

			var permission = new CurrentWarehousePermissions();
			foreach (var p in _warehousePermissions)
			{
				disjunction.Add<Warehouse>(
					w =>
						w.Id.IsIn(permission.WarehousePermissions.Where(x => x.WarehousePermissionType == p && x.PermissionValue == true)
						.Select(x => x.Warehouse.Id).ToArray()));
			}
			query.Where(disjunction);

			query.Where(GetSearchCriterion(
				() => warehouseAlias.Id,
				() => warehouseAlias.Name
			));
			var result = query.SelectList(list => list
					.Select(w => w.Id).WithAlias(() => warehouseNodeAlias.Id)
					.Select(w => w.Name).WithAlias(() => warehouseNodeAlias.Name))
				.OrderBy(w => w.Name).Asc
				.TransformUsing(Transformers.AliasToBean<WarehouseJournalNode>());
			return result;
		};

		protected override Func<WarehouseViewModel> CreateDialogFunction => () => new WarehouseViewModel(
			EntityUoWBuilder.ForCreate(),
			QS.DomainModel.UoW.UnitOfWorkFactory.GetDefaultFactory,
			commonServices,
			_subdivisionRepository
		);

		protected override Func<WarehouseJournalNode, WarehouseViewModel> OpenDialogFunction => node => new WarehouseViewModel(
			EntityUoWBuilder.ForOpen(node.Id),
			QS.DomainModel.UoW.UnitOfWorkFactory.GetDefaultFactory,
			commonServices,
			_subdivisionRepository
		);
	}


	public class WarehouseJournalFilterViewModel
	{
		public IEnumerable<int> IncludeWarehouseIds { get; set; }
		public int[] ExcludeWarehousesIds { get; set; }
	}
}
