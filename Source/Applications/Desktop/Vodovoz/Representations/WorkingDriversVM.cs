﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gamma.ColumnConfig;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Dialect.Function;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QS.Utilities.Text;
using QSOrmProject.RepresentationModel;
using Vodovoz.Domain.Documents;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Logistic.Cars;
using Vodovoz.Domain.Orders;
using Vodovoz.Filters.ViewModels;
using Order = Vodovoz.Domain.Orders.Order;

namespace Vodovoz.ViewModel
{
	public class WorkingDriversVM : RepresentationModelEntityBase<RouteList, WorkingDriverVMNode>
	{
		#region IRepresentationModel implementation

		public override void UpdateNodes()
		{
			WorkingDriverVMNode resultAlias = null;
			Employee driverAlias = null;
			RouteList routeListAlias = null;
			Car carAlias = null;
			CarVersion carVersionAlias = null;

			Domain.Orders.Order orderAlias = null;
			OrderItem ordItemsAlias = null;
			Nomenclature nomenclatureAlias = null;

			var completedSubquery = QueryOver.Of<RouteListItem>()
				.Where(i => i.RouteList.Id == routeListAlias.Id)
				.Where(i => i.Status != RouteListItemStatus.EnRoute)
				.Select(Projections.RowCount());

			var addressesSubquery = QueryOver.Of<RouteListItem>()
				.Where(i => i.RouteList.Id == routeListAlias.Id)
				.Select(Projections.RowCount());

			var uncompletedBottlesSubquery = QueryOver.Of<RouteListItem>()  // Запрашивает количество ещё не доставленных бутылей.
				.Where(i => i.RouteList.Id == routeListAlias.Id)
				.Where(i => i.Status == RouteListItemStatus.EnRoute)
				   .JoinAlias(rli => rli.Order, () => orderAlias)
				   .JoinAlias(() => orderAlias.OrderItems, () => ordItemsAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
				   .JoinAlias(() => ordItemsAlias.Nomenclature, () => nomenclatureAlias)
			   	.Where(() => nomenclatureAlias.Category == NomenclatureCategory.water && nomenclatureAlias.TareVolume == TareVolume.Vol19L)
				.Select(Projections.Sum(() => ordItemsAlias.Count));

			var trackSubquery = QueryOver.Of<Track>()
				.Where(x => x.RouteList.Id == routeListAlias.Id)
				.Select(x => x.Id);

			var isCompanyHavingProjection = Projections.Conditional(
				Restrictions.Eq(Projections.Property(() => carVersionAlias.CarOwnType), CarOwnType.Company),
				Projections.Constant(true),
				Projections.Constant(false));

			#region Water19LReserve

			RouteListItem routeListItemAlias = null;
			RouteListItem transferedToAlias = null;
			OrderItem orderItemAlias = null;
			AdditionalLoadingDocumentItem additionalLoadingDocumentItemAlias = null;
			AdditionalLoadingDocument additionalLoadingDocumentAlias = null;

			var ownOrdersSubquery = QueryOver.Of<RouteListItem>(() => routeListItemAlias)
				.JoinAlias(() => routeListItemAlias.Order, () => orderAlias)
				.JoinEntityAlias(() => orderItemAlias, () => orderItemAlias.Order.Id == orderAlias.Id)
				.JoinAlias(() => orderItemAlias.Nomenclature, () => nomenclatureAlias)
				.Where(() => !orderAlias.IsFastDelivery && !routeListItemAlias.WasTransfered)
				.And(() => nomenclatureAlias.Category == NomenclatureCategory.water
				           && nomenclatureAlias.TareVolume == TareVolume.Vol19L)
				.And(() => routeListItemAlias.RouteList.Id == routeListAlias.Id)
				.Select(Projections.Sum(() => orderItemAlias.Count));

			var additionalBalanceSubquery = QueryOver.Of<AdditionalLoadingDocumentItem>(() => additionalLoadingDocumentItemAlias)
				.JoinAlias(() => additionalLoadingDocumentItemAlias.Nomenclature, () => nomenclatureAlias)
				.JoinAlias(() => additionalLoadingDocumentItemAlias.AdditionalLoadingDocument, () => additionalLoadingDocumentAlias)
				.Where(() => nomenclatureAlias.Category == NomenclatureCategory.water
				             && nomenclatureAlias.TareVolume == TareVolume.Vol19L)
				.And(() => routeListAlias.AdditionalLoadingDocument.Id == additionalLoadingDocumentAlias.Id)
				.Select(Projections.Sum(() => additionalLoadingDocumentItemAlias.Amount));

			var deliveredOrdersSubquery =QueryOver.Of<RouteListItem>(() => routeListItemAlias)
				.JoinAlias(() => routeListItemAlias.Order, () => orderAlias)
				.JoinEntityAlias(() => orderItemAlias, () => orderItemAlias.Order.Id == orderAlias.Id)
				.JoinAlias(() => orderItemAlias.Nomenclature, () => nomenclatureAlias)
				.Left.JoinAlias(() => routeListItemAlias.TransferedTo, () => transferedToAlias)
				.Where(() =>
					//не отменённые и не недовозы
					routeListItemAlias.Status != RouteListItemStatus.Canceled && routeListItemAlias.Status != RouteListItemStatus.Overdue
					// и не перенесённые к водителю; либо перенесённые с погрузкой; либо перенесённые и это экспресс-доставка (всегда без погрузки)
					&& (!routeListItemAlias.WasTransfered || routeListItemAlias.NeedToReload || orderAlias.IsFastDelivery)
					// и не перенесённые от водителя; либо перенесённые и не нужна погрузка и не экспресс-доставка (остатки по экспресс-доставке не переносятся)
					&& (routeListItemAlias.Status != RouteListItemStatus.Transfered || (!transferedToAlias.NeedToReload && !orderAlias.IsFastDelivery)))
				.And(() => nomenclatureAlias.Category == NomenclatureCategory.water &&
				           nomenclatureAlias.TareVolume == TareVolume.Vol19L)
				.And(() => routeListItemAlias.RouteList.Id == routeListAlias.Id)
				.Select(Projections.Sum(() => orderItemAlias.Count));

			var water19LReserveProjection =
				Projections.Conditional(Restrictions.Eq(Projections.Property(() => routeListAlias.AdditionalLoadingDocument), null),
					Projections.Constant(0m),
					Projections.SqlFunction(
						new SQLFunctionTemplate(NHibernateUtil.Decimal, "IFNULL(?1, 0) + IFNULL(?2, 0) - IFNULL(?3, 0)"), // 
						NHibernateUtil.Decimal,
						Projections.SubQuery(ownOrdersSubquery),
						Projections.SubQuery(additionalBalanceSubquery),
						Projections.SubQuery(deliveredOrdersSubquery)));

			#endregion

			var query = UoW.Session.QueryOver<RouteList>(() => routeListAlias);

			if(Filter.IsFastDeliveryOnly)
			{
				query.Where(() => routeListAlias.AdditionalLoadingDocument != null);
			}

			var result = query
				.JoinAlias(rl => rl.Driver, () => driverAlias)
				.JoinAlias(rl => rl.Car, () => carAlias)
				.JoinEntityAlias(() => carVersionAlias,
					() => carVersionAlias.Car.Id == carAlias.Id
						&& carVersionAlias.StartDate <= routeListAlias.Date &&
						(carVersionAlias.EndDate == null || carVersionAlias.EndDate >= routeListAlias.Date))
				.Where(rl => rl.Status == RouteListStatus.EnRoute)
				.Where(rl => rl.Driver != null)
				.Where(rl => rl.Car != null)
				.SelectList(list => list
					.Select(() => driverAlias.Id).WithAlias(() => resultAlias.Id)
					.Select(() => driverAlias.Name).WithAlias(() => resultAlias.Name)
					.Select(() => driverAlias.LastName).WithAlias(() => resultAlias.LastName)
					.Select(() => driverAlias.Patronymic).WithAlias(() => resultAlias.Patronymic)
					.Select(() => carAlias.RegistrationNumber).WithAlias(() => resultAlias.CarNumber)
					.Select(isCompanyHavingProjection).WithAlias(() => resultAlias.IsVodovozAuto)
					.Select(() => routeListAlias.Id).WithAlias(() => resultAlias.RouteListNumber)
					.SelectSubQuery(addressesSubquery).WithAlias(() => resultAlias.AddressesAll)
					.SelectSubQuery(completedSubquery).WithAlias(() => resultAlias.AddressesCompleted)
					.SelectSubQuery(trackSubquery).WithAlias(() => resultAlias.TrackId)
					.SelectSubQuery(uncompletedBottlesSubquery).WithAlias(() => resultAlias.BottlesLeft)
					.Select(water19LReserveProjection).WithAlias(() => resultAlias.Water19LReserve)
					)
				.TransformUsing(Transformers.AliasToBean<WorkingDriverVMNode>())
				.List<WorkingDriverVMNode>();

			var summaryResult = new List<WorkingDriverVMNode>();
			int rowNum = 0;
			foreach(var driver in result.GroupBy(x => x.Id).OrderBy(x => x.First().ShortName)) {
				var savedRow = driver.First();
				savedRow.RouteListsText = String.Join("; ", driver.Select(x => x.TrackId != null ? String.Format("<span foreground=\"green\"><b>{0}</b></span>", x.RouteListNumber) : x.RouteListNumber.ToString()));
				savedRow.RouteListsIds = driver.ToDictionary(x => x.RouteListNumber, x => x.TrackId);
				savedRow.AddressesAll = driver.Sum(x => x.AddressesAll);
				savedRow.AddressesCompleted = driver.Sum(x => x.AddressesCompleted);
				savedRow.Water19LReserve = driver.Sum(x => x.Water19LReserve);
				savedRow.RowNumber = ++rowNum;
				summaryResult.Add(savedRow);
			}

			SetItemsSource(summaryResult);
		}

		IColumnsConfig columnsConfig = FluentColumnsConfig<WorkingDriverVMNode>.Create()
			.AddColumn("№").AddNumericRenderer(node => node.RowNumber)
			.AddColumn("Имя").SetDataProperty(node => node.ShortName)
			.AddColumn("Машина").AddTextRenderer().AddSetter((c, node) => c.Markup = node.CarText)
			.AddColumn("МЛ").AddTextRenderer().AddSetter((c, node) => c.Markup = node.RouteListsText)
			.AddColumn("Выполнено").AddProgressRenderer(x => x.CompletedPercent)
			.AddSetter((c, n) => c.Text = n.CompletedText)
			.AddColumn("Остаток бут.").AddTextRenderer().AddSetter((c, node) => c.Markup = $"{node.BottlesLeft:N0}")
			.AddColumn("Остаток запаса").AddTextRenderer().AddSetter((c, node) => c.Markup = $"{node.Water19LReserve:N0}")
			.Finish();

		public override IColumnsConfig ColumnsConfig => columnsConfig;

		#endregion

		#region implemented abstract members of RepresentationModelBase

		protected override bool NeedUpdateFunc(RouteList updatedSubject) => true;

		#endregion

		public WorkingDriversVM() : this(UnitOfWorkFactory.CreateWithoutRoot(), new RouteListTrackFilterViewModel()) { }

		public WorkingDriversVM(IUnitOfWork uow, RouteListTrackFilterViewModel routeListTrackFilterViewModel) : base()
		{
			this.UoW = uow;
			Filter = routeListTrackFilterViewModel;
		}

		private RouteListTrackFilterViewModel _filter;
		public RouteListTrackFilterViewModel Filter
		{
			get => _filter;
			set
			{
				if(_filter != value)
				{
					_filter = value;
					_filter.OnFiltered += (sender, e) => UpdateNodes();
				}
			}
		}
	}

	public class WorkingDriverVMNode
	{
		public int Id { get; set; }

		public int RowNumber { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Patronymic { get; set; }
		public string CarNumber { get; set; }
		public string RouteListsText { get; set; }
		public bool IsVodovozAuto { get; set; }

		//RouteListId, TrackId
		public Dictionary<int, int?> RouteListsIds;

		public int AddressesCompleted { get; set; }
		public int AddressesAll { get; set; }

		public decimal BottlesLeft { get; set; } // @Дима

		public decimal Water19LReserve { get; set; }

		public int CompletedPercent {
			get {
				if(AddressesAll == 0)
					return 100;
				return (int)(((double)AddressesCompleted / AddressesAll) * 100);
			}
		}

		public string CompletedText => string.Format("{0}/{1}", AddressesCompleted, AddressesAll);

		public string CarText => IsVodovozAuto ? string.Format("<b>{0}</b>", CarNumber) : CarNumber;

		private int routeListNumber;

		public int? TrackId;

		public int RouteListNumber {
			get => routeListNumber;
			set {
				routeListNumber = value;
				this.RouteListsText = value.ToString();
			}
		}

		public string ShortName => PersonHelper.PersonNameWithInitials(LastName, Name, Patronymic);
	}
}
