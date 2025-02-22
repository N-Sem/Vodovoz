﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gamma.Binding;
using Gamma.ColumnConfig;
using Gdk;
using GMap.NET;
using GMap.NET.GtkSharp;
using GMap.NET.MapProviders;
using Gtk;
using QS.Dialog.GtkUI;
using QS.Utilities;
using QS.Views.GtkUI;
using QSWidgetLib;
using Vodovoz.Additions.Logistic;
using Vodovoz.Dialogs.Logistic;
using Vodovoz.Domain.Logistic;
using Vodovoz.ViewModels.Logistic;
using Order = Vodovoz.Domain.Orders.Order;
using Vodovoz.Domain.Sale;
using Vodovoz.Domain.Employees;
using System.Drawing;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Project.Journal;
using Vodovoz.Domain.Logistic.Cars;
using Vodovoz.Domain.Orders;
using Vodovoz.Filters.ViewModels;
using Vodovoz.JournalViewModels;
using Gamma.Binding.Core.LevelTreeConfig;
using Vodovoz.ViewModels.Dialogs.Logistic;

namespace Vodovoz.Views.Logistic
{

	public partial class RouteListsOnDayView : TabViewBase<RouteListsOnDayViewModel>
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

		public RouteListsOnDayView(RouteListsOnDayViewModel viewModel) : base(viewModel)
		{
			this.Build();
			ConfigureDlg();
		}

		#region Поля
		private readonly GMapOverlay districtsOverlay = new GMapOverlay("districts");
		private readonly GMapOverlay driverDistrictsOverlay = new GMapOverlay("driverDistricts");
		private readonly GMapOverlay addressesOverlay = new GMapOverlay("addresses");
		private readonly GMapOverlay driverAddressesOverlay = new GMapOverlay("driverAddresses");
		private readonly GMapOverlay selectionOverlay = new GMapOverlay("selection");
		private readonly GMapOverlay routeOverlay = new GMapOverlay("route");
		private GMapPolygon brokenSelection;
		private List<GMapMarker> selectedMarkers = new List<GMapMarker>();
		Pixbuf[] pixbufMarkers;
		int addressesWithoutCoordinats, addressesWithoutRoutes, totalBottlesCountAtDay, bottlesWithoutRL;
		#endregion

		void ConfigureDlg()
		{
			ytreeviewGeographicGroup.ColumnsConfig = FluentColumnsConfig<GeographicGroupNode>
				.Create()
					.AddColumn("Выбрать").AddToggleRenderer(x => x.Selected).Editing()
					.AddColumn("Район города").AddTextRenderer(x => x.GeographicGroup.Name)
				.Finish();

			ytreeviewGeographicGroup.Binding.AddBinding(ViewModel, vm => vm.GeographicGroupNodes, w => w.ItemsDataSource).InitializeFromSource();
			ytreeviewGeographicGroup.HeadersVisible = false;

			if(progressOrders.Adjustment == null)
				progressOrders.Adjustment = new Adjustment(0, 0, 0, 1, 1, 0);

			//Configure map
			districtsOverlay.IsVisibile = false;
			gmapWidget.MapProvider = GMapProviders.GoogleMap;
			gmapWidget.Position = new PointLatLng(59.93900, 30.31646);
			gmapWidget.HeightRequest = 150;
			gmapWidget.HasFrame = true;
			gmapWidget.Overlays.Add(districtsOverlay);
			gmapWidget.Overlays.Add(driverDistrictsOverlay);
			gmapWidget.Overlays.Add(routeOverlay);
			gmapWidget.Overlays.Add(addressesOverlay);
			gmapWidget.Overlays.Add(driverAddressesOverlay);
			gmapWidget.Overlays.Add(selectionOverlay);
			gmapWidget.DisableAltForSelection = true;
			gmapWidget.OnSelectionChange += GmapWidget_OnSelectionChange;
			gmapWidget.ButtonPressEvent += GmapWidget_ButtonPressEvent;
			gmapWidget.ButtonReleaseEvent += GmapWidget_ButtonReleaseEvent;
			gmapWidget.MotionNotifyEvent += GmapWidget_MotionNotifyEvent;

			yenumcomboMapType.ItemsEnum = typeof(MapProviders);
			yenumcomboMapType.TooltipText = "Если карта отображается некорректно или не отображается вовсе - смените тип карты";
			yenumcomboMapType.EnumItemSelected += (sender, args) =>
				gmapWidget.MapProvider = MapProvidersHelper.GetPovider((MapProviders)args.SelectedItem);
			yenumcomboMapType.SelectedItem = MapProviders.GoogleMap;

			LoadDistrictsGeometry();

			var colorPink = new Gdk.Color(0xff, 0xc0, 0xc0);
			var colorWhite = new Gdk.Color(0xff, 0xff, 0xff);
			var colorLightRed = new Gdk.Color(0xff, 0x66, 0x66);

			ytreeRoutes.ColumnsConfig = FluentColumnsConfig<object>
				.Create()
					.AddColumn("Маркер").AddPixbufRenderer(x => GetRowMarker(x))
					.AddColumn("МЛ/Адрес").AddTextRenderer(x => ViewModel.GetRowTitle(x))
					.AddColumn("Адр./Время").AddTextRenderer(x => ViewModel.GetRowTime(x), useMarkup: true)
					.AddColumn("Смена").AddTextRenderer(x => ViewModel.GetRowDeliveryShift(x), useMarkup: true)
					.AddColumn("План").AddTextRenderer(x => ViewModel.GetRowPlanTime(x), useMarkup: true)
					.AddColumn("Бутылей").AddTextRenderer(x => ViewModel.GetRowBottles(x), useMarkup: true)
					.AddColumn("Бут. 6л").AddTextRenderer(x => ViewModel.GetRowBottlesSix(x))
					.AddColumn("Бут. менее 6л").AddTextRenderer(x => ViewModel.GetRowBottlesSmall(x))
					.AddColumn("Вес, кг").AddTextRenderer(x => ViewModel.GetRowWeight(x), useMarkup: true)
					.AddColumn("Объём, куб.м.").AddTextRenderer(x => ViewModel.GetRowVolume(x), useMarkup: true)
					.AddColumn("Погрузка").Tag(RouteColumnTag.OnloadTime)
						.AddTextRenderer(x => ViewModel.GetRowOnloadTime(x), useMarkup: true)
						.AddSetter((c, n) => c.Editable = n is RouteList)
						.EditedEvent(OnLoadTimeEdited)
					.AddColumn("Километраж").AddTextRenderer(x => ViewModel.GetRowDistance(x))
					.AddColumn("К клиенту").AddTextRenderer(x => ViewModel.GetRowEquipmentToClient(x))
					.AddColumn("От клиента").AddTextRenderer(x => ViewModel.GetRowEquipmentFromClient(x))
				.Finish();

			ytreeRoutes.HasTooltip = true;
			ytreeRoutes.QueryTooltip += YtreeRoutes_QueryTooltip;
			ytreeRoutes.Selection.Changed += YtreeRoutes_Selection_Changed;

			Pixbuf vodovozCarIcon = Pixbuf.LoadFromResource("Vodovoz.icons.buttons.vodovoz-logo.png");
			ytreeviewOnDayDrivers.ColumnsConfig = FluentColumnsConfig<AtWorkDriver>
				.Create()
					.AddColumn("Водитель").AddTextRenderer(x => x.Employee.ShortName)
					.AddColumn("Автомобиль").AddPixbufRenderer(x => x.Car != null && x.CarVersion.IsCompanyCar ? vodovozCarIcon : null)
						.AddTextRenderer(x => x.Car != null ? x.Car.RegistrationNumber : "нет")
					.AddColumn("База").AddComboRenderer(x => x.GeographicGroup).SetDisplayFunc(x => x.Name)
						.FillItems(ViewModel.GeographicGroupsExceptEast)
						.AddSetter(
							(c, n) => {
								c.Editable = n.Car != null;
								c.BackgroundGdk = n.GeographicGroup == null && n.Car != null
									? colorLightRed
									: colorWhite;
							}
						)
					.AddColumn("")
				.Finish();
			ytreeviewOnDayDrivers.Selection.Mode = SelectionMode.Multiple;
			ytreeviewOnDayDrivers.Selection.Changed += (sender, e) => ViewModel.SelectedDrivers = ytreeviewOnDayDrivers.GetSelectedObjects<AtWorkDriver>().ToArray();
			ytreeviewOnDayDrivers.Binding.AddBinding(ViewModel, vm => vm.ObservableDriversOnDay, w => w.ItemsDataSource).InitializeFromSource();

			ytreeviewAddressesTypes.ColumnsConfig = FluentColumnsConfig<OrderAddressTypeNode>.Create()
				.AddColumn("").AddToggleRenderer(x => x.Selected)
				.AddColumn("Тип адресов").AddTextRenderer(x => x.Title)
				.Finish();
			ytreeviewAddressesTypes.ItemsDataSource = ViewModel.OrderAddressTypes;

			ytreeviewShift.ColumnsConfig = FluentColumnsConfig<DeliveryShiftNode>.Create()
				.AddColumn("").AddToggleRenderer(x => x.Selected)
				.AddColumn("Смены").AddTextRenderer(x => x.Title)
				.Finish();
			ytreeviewShift.ItemsDataSource = ViewModel.DeliveryShiftNodes;


			buttonAddDriver.Clicked += (sender, e) => ViewModel.AddDriverCommand.Execute();

			buttonRemoveDriver.Binding.AddBinding(ViewModel, vm => vm.AreDriversSelected, w => w.Sensitive).InitializeFromSource();
			buttonRemoveDriver.Clicked += (sender, e) => ViewModel.RemoveDriverCommand.Execute(null);

			buttonDriverSelectAuto.Binding.AddBinding(ViewModel, vm => vm.AreDriversSelected, w => w.Sensitive).InitializeFromSource();

			ytreeviewOnDayForwarders.ColumnsConfig = FluentColumnsConfig<AtWorkForwarder>
				.Create()
					.AddColumn("Экспедитор").AddTextRenderer(x => x.Employee.ShortName)
				.Finish();

			ytreeviewOnDayForwarders.Selection.Mode = SelectionMode.Multiple;
			ytreeviewOnDayForwarders.Selection.Changed += (sender, e) => ViewModel.SelectedForwarder = ytreeviewOnDayForwarders.GetSelectedObjects<AtWorkForwarder>().FirstOrDefault();
			ytreeviewOnDayForwarders.Binding.AddBinding(ViewModel, vm => vm.ObservableForwardersOnDay, w => w.ItemsDataSource).InitializeFromSource();

			buttonAddForwarder.Clicked += (sender, e) => ViewModel.AddForwarderCommand.Execute();

			buttonRemoveForwarder.Binding.AddBinding(ViewModel, vm => vm.IsForwarderSelected, w => w.Sensitive).InitializeFromSource();
			buttonRemoveForwarder.Clicked += (sender, e) => ViewModel.RemoveForwarderCommand.Execute(ytreeviewOnDayForwarders.GetSelectedObjects<AtWorkForwarder>());

			yspinMaxTime.Binding.AddBinding(ViewModel.Optimizer, e => e.MaxTimeSeconds, w => w.ValueAsInt).InitializeFromSource();

			ydateForRoutes.Binding.AddBinding(ViewModel, vm => vm.DateForRouting, w => w.DateOrNull).InitializeFromSource();
			checkShowCompleted.Binding.AddBinding(ViewModel, vm => vm.ShowCompleted, w => w.Active).InitializeFromSource();
			ySpnMin19Btls.Binding.AddBinding(ViewModel, vm => vm.MinBottles19L, w => w.ValueAsInt).InitializeFromSource();
			ydateForRoutes.Binding.AddBinding(ViewModel, vm => vm.HasNoChanges, w => w.Sensitive).InitializeFromSource();
			checkShowCompleted.Binding.AddBinding(ViewModel, vm => vm.HasNoChanges, w => w.Sensitive).InitializeFromSource();
			checkShowOnlyDriverOrders.Binding.AddBinding(ViewModel, vm => vm.ShowOnlyDriverOrders, w => w.Active).InitializeFromSource();
			checkShowOnlyDriverOrders.Toggled += (sender, e) => GetRowFromYTreeRoutes();

			timeRngPicker.Binding.AddBinding(ViewModel, vm => vm.HasNoChanges, w => w.Sensitive).InitializeFromSource();
			ySpnMin19Btls.Binding.AddBinding(ViewModel, vm => vm.HasNoChanges, w => w.Sensitive).InitializeFromSource();

			timeRngPicker.Binding.AddBinding(ViewModel, vm => vm.DeliveryFromTime, w => w.TimeStart).InitializeFromSource();
			timeRngPicker.Binding.AddBinding(ViewModel, vm => vm.DeliveryToTime, w => w.TimeEnd).InitializeFromSource();
			timeRngPicker.TimePeriodChangedByUser += (sender, e) => FillItems();

			timeDrvShiftRngpicker.Binding.AddBinding(ViewModel, vm => vm.DriverStartTime, t => t.TimeStart).InitializeFromSource();
			timeDrvShiftRngpicker.Binding.AddBinding(ViewModel, vm => vm.DriverEndTime, t => t.TimeEnd).InitializeFromSource();
			timeDrvShiftRngpicker.Binding.AddBinding(ViewModel, vm => vm.HasNoChanges, w => w.Sensitive).InitializeFromSource();

			checkShowDistricts.Toggled += (sender, e) => districtsOverlay.IsVisibile = checkShowDistricts.Active;

			ViewModel.AutoroutingResultsSaved += (sender, e) => FillDialogAtDay();

			btnSave.Binding.AddBinding(ViewModel, e => e.IsAutoroutingModeActive, w => w.Visible).InitializeFromSource();
			btnSave.Clicked += (sender, e) => ViewModel.SaveCommand.Execute();

			btnCancel.Binding.AddBinding(ViewModel, e => e.IsAutoroutingModeActive, w => w.Visible).InitializeFromSource();
			btnCancel.Clicked += (sender, e) => {
				ViewModel.DisposeUoW();
				ViewModel.CreateUoW();
				ViewModel.HasNoChanges = true;
				ViewModel.IsAutoroutingModeActive = false;
				FillDialogAtDay();
			};
			btnRefresh.Clicked += (sender, e) => Refresh();
			ydateForRoutes.DateChanged += (sender, e) => Refresh();
			Refresh();
			buttonRemoveAddress.Clicked+= (sender, e) => {
				ViewModel.RemoveRLItemCommand.Execute(ytreeRoutes.GetSelectedObject<RouteListItem>());
				UpdateMarkersInDriverDistricts(ViewModel.DriverFromRouteList);
				ytreeRoutes.YTreeModel.EmitModelChanged();
				UpdateAddressesOnMap();
			};
			checkShowCompleted.Toggled += (sender, e) => FillDialogAtDay();
			buttonOpen.Clicked += (sender, e) => ViewModel.OpenOrderOrRouteListCommand.Execute(ytreeRoutes.GetSelectedObject());
			buttonMapHelp.Clicked += (sender, e) => new RouresAtDayInfoWnd().Show();
			buttonRebuildRoute.Clicked += (sender, e) => {
				ViewModel.RebuilOneRouteCommand.Execute(ytreeRoutes.GetSelectedObject());
				ytreeRoutes.YTreeModel.EmitModelChanged();
			};
			buttonWarnings.Clicked += (sender, e) => ViewModel.ShowWarningsCommand.Execute();
			ytreeviewOnDayDrivers.RowActivated += OnButtonDriverSelectAutoClicked;
			buttonFilter.Clicked += (sender, e) => FillItems();
			enumCmbDeliveryType.ItemsEnum = typeof(DeliveryScheduleFilterType);
			enumCmbDeliveryType.Binding.AddBinding(ViewModel, vm => vm.DeliveryScheduleType, w => w.SelectedItem).InitializeFromSource();
			enumCmbDeliveryType.ChangedByUser += (sender, e) => FillItems();
			
			ytextWorkDriversInfo.Binding.AddBinding(ViewModel, vm => vm.CanTake, w => w.Buffer.Text).InitializeFromSource(); 
			viewDeliverySummary.ColumnsConfig = FluentColumnsConfig<DeliverySummary>
				.Create()
				.AddColumn("Статус").AddTextRenderer(x => x.Name)
				.AddColumn("Адреса").AddTextRenderer(x=>x.AddressCount.ToString()).XAlign(0.5f)
				.AddColumn("Бутыли").AddTextRenderer(x=>x.Bottles.ToString("N0")).XAlign(0.5f)
				.Finish();

			viewDeliverySummary.Binding.AddBinding(ViewModel, vm => vm.ObservableDeliverySummary, w => w.ItemsDataSource).InitializeFromSource();
		}

		void GmapWidget_ButtonReleaseEvent(object o, ButtonReleaseEventArgs args)
		{
			if(dragSelectionPointId != -1) {
				gmapWidget.DisableAltForSelection = true;
				OnPoligonSelectionUpdated();
				dragSelectionPointId = -1;
			}
		}

		void GmapWidget_MotionNotifyEvent(object o, MotionNotifyEventArgs args)
		{
			if(dragSelectionPointId > -1) {
				brokenSelection.Points[dragSelectionPointId] = gmapWidget.FromLocalToLatLng((int)args.Event.X, (int)args.Event.Y);
				gmapWidget.UpdatePolygonLocalPosition(brokenSelection);
				gmapWidget.Refresh();
			}
		}

		void YtreeRoutes_QueryTooltip(object o, QueryTooltipArgs args)
		{
			ytreeRoutes.ConvertWidgetToBinWindowCoords(args.X, args.Y, out int binX, out int binY);

			if(ytreeRoutes.GetPathAtPos(binX, binY, out TreePath path, out TreeViewColumn col) && ytreeRoutes.Model.GetIter(out TreeIter iter, path)) {
				var loadtimeCol = ytreeRoutes.ColumnsConfig.GetColumnsByTag(RouteColumnTag.OnloadTime).Where(x => x == col).ToArray();
				if(loadtimeCol.Any() && ytreeRoutes.YTreeModel.NodeFromIter(iter) is RouteList node) {
					args.RetVal = true;
					args.Tooltip.Text = ViewModel.GenerateToolTip(node);
				}
			}
		}

		bool poligonSelection;
		int dragSelectionPointId = -1;

		void GmapWidget_ButtonPressEvent(object o, ButtonPressEventArgs args)
		{
			if(args.Event.Button == 1) {
				bool markerIsSelect = false;
				if(args.Event.State.HasFlag(ModifierType.LockMask)) {
					foreach(var marker in addressesOverlay.Markers) {
						if(marker.IsMouseOver) {
							var markerUnderMouse = selectedMarkers
													.Where(m => m.Tag is Order)
													.FirstOrDefault(x => (x.Tag as Order).Id == (marker.Tag as Order)?.Id);

							if(markerUnderMouse == null) {
								selectedMarkers.Add(marker);
								logger.Debug("Маркер с заказом №{0} добавлен в список выделенных", (marker.Tag as Order)?.Id);
							} else {
								selectedMarkers.Remove(markerUnderMouse);
								logger.Debug("Маркер с заказом №{0} исключен из списка выделенных", (marker.Tag as Order)?.Id);
							}
							markerIsSelect = true;
						}
					}
					UpdateSelectedInfo(selectedMarkers);
					UpdateAddressesOnMap();
					return;
				}
				if(!markerIsSelect) {
					selectedMarkers.Clear();
					logger.Debug("Список выделенных маркеров очищен");
				}
				UpdateAddressesOnMap();

				if(poligonSelection) {
					GRect rect = new GRect((long)args.Event.X - 5, (long)args.Event.Y - 5, 10, 10);
					rect.OffsetNegative(gmapWidget.RenderOffset);

					dragSelectionPointId = brokenSelection.LocalPoints.FindIndex(rect.Contains);
					if(dragSelectionPointId != -1) {
						gmapWidget.DisableAltForSelection = false;
						return;
					}
				}

				if(args.Event.State.HasFlag(ModifierType.ControlMask)) {
					if(!poligonSelection) {
						poligonSelection = true;
						logger.Debug("Старт выделения через полигон.");
						var startPoint = gmapWidget.FromLocalToLatLng((int)args.Event.X, (int)args.Event.Y);
						brokenSelection = new GMapPolygon(new List<PointLatLng> { startPoint }, "Выделение");
						gmapWidget.UpdatePolygonLocalPosition(brokenSelection);
						selectionOverlay.Polygons.Add(brokenSelection);
					} else {
						logger.Debug("Продолжили.");
						var newPoint = gmapWidget.FromLocalToLatLng((int)args.Event.X, (int)args.Event.Y);
						brokenSelection.Points.Add(newPoint);
						gmapWidget.UpdatePolygonLocalPosition(brokenSelection);
					}
					OnPoligonSelectionUpdated();
				} else {
					logger.Debug("Закончили.");
					poligonSelection = false;
					UpdateSelectedInfo(new List<GMapMarker>());
					selectionOverlay.Clear();
				}
			}

			if(args.Event.Button == 3 && addressesOverlay.Markers.FirstOrDefault(m => m.IsMouseOver)?.Tag is Order order) {
				Menu popupMenu = new Menu();
				var item = new MenuItem(string.Format("Открыть {0}", order));
				item.Activated += (sender, e) => {
					var dlg = new OrderDlg(order) {
						HasChanges = false
					};
					dlg.SetDlgToReadOnly();
					Tab.TabParent.AddSlaveTab(Tab, dlg);
				};
				popupMenu.Add(item);
				popupMenu.ShowAll();
				popupMenu.Popup();
			}
		}

		void OnPoligonSelectionUpdated()
		{
			var selected = addressesOverlay.Markers.Where(m => brokenSelection.IsInside(m.Position)).ToList();
			UpdateSelectedInfo(selected);
		}

		void YtreeRoutes_Selection_Changed(object sender, EventArgs e)
		{
			object row = GetRowFromYTreeRoutes();

			buttonRemoveAddress.Sensitive = row is RouteListItem && !checkShowCompleted.Active;
			buttonOpen.Sensitive = buttonRebuildRoute.Sensitive = (row is RouteListItem) || (row is RouteList);

			//Рисуем выделенный маршрут
			routeOverlay.Clear();
			if(row != null) {
				if(!(row is RouteList rl))
					rl = (row as RouteListItem).RouteList;

				MapDrawingHelper.DrawRoute(routeOverlay, rl, ViewModel.DistanceCalculator);

				//Если выбран адрес, центруем на него карту.
				if(row is RouteListItem rli)
					gmapWidget.Position = rli.Order.DeliveryPoint.GmapPoint;
			}
			logger.Info("Ok");
		}

		private object GetRowFromYTreeRoutes()
		{
			var row = ytreeRoutes.GetSelectedObject();

			checkShowOnlyDriverOrders.Sensitive = row is RouteList || row is RouteListItem;

			if(row is RouteList) {
				LoadDriverDistrictsGeometry((row as RouteList).Driver);
				ShowOrdersInDriverDistricts((row as RouteList).Driver);
			}

			if(row is RouteListItem) {
				LoadDriverDistrictsGeometry((row as RouteListItem).RouteList.Driver);
				ShowOrdersInDriverDistricts((row as RouteListItem).RouteList.Driver);
			}

			return row;
		}

		void GmapWidget_OnSelectionChange(RectLatLng Selection, bool ZoomToFit)
		{
			if(poligonSelection)
				return;
			var selected = addressesOverlay.Markers.Where(m => Selection.Contains(m.Position)).ToList();
			UpdateSelectedInfo(selected);
		}

		void UpdateSelectedInfo(List<GMapMarker> selected)
		{
			var orders = selected.Select(x => x.Tag).OfType<Order>();
			if(!orders.Any()) {
				labelSelected.Markup = "Адресов\nне выбрано";
				menuAddToRL.Sensitive = false;
				return;
			}
			var selectedBottle = orders.Sum(o => o.Total19LBottlesToDeliver);
			var selectedKilos = orders.Sum(o => o.TotalWeight);
			var selectedCbm = orders.Sum(o => o.TotalVolume);
			labelSelected.Markup = string.Format(
				"{0} адр.; {1} бут.;\n{2} кг; {3} м<sup>3</sup>",
				orders.Count(),
				selectedBottle,
				selectedKilos,
				selectedCbm
			);
			menuAddToRL.Sensitive = ViewModel.RoutesOnDay.Any() && !checkShowCompleted.Active;
		}

		Pixbuf GetRowMarker(object row)
		{
			return PointMarker.GetIconPixbuf(
				ViewModel.GetAddressMarker(
					ViewModel.GetMarkerIndex(
						row,
						pixbufMarkers.Length
					)
				).ToString(),
				ViewModel.GetMarkerShape(row)
			);
		}

		void FillFullOrdersInfo()
		{
			ytextFullOrdersInfo.Buffer.Text = ViewModel.GetOrdersInfo();
		}

		void FillDialogAtDay()
		{
			addressesOverlay.Clear();
			TurnOffCheckShowOnlyDriverOrders();
			
			
			logger.Info("Загружаем заказы на {0:d}...", ViewModel.DateForRouting);
			ViewModel.InitializeData();
			UpdateRoutesPixBuf();
			UpdateRoutesButton();
			UpdateAddressesOnMap();

			var levels = LevelConfigFactory.FirstLevel<RouteList, RouteListItem>(x => x.Addresses).LastLevel(c => c.RouteList).EndConfig();
			ytreeRoutes.YTreeModel = new LevelTreeModel<RouteList>(ViewModel.RoutesOnDay, levels);
		}

		void UpdateAddressesOnMap()
		{
			logger.Info("Обновляем адреса на карте...");
			addressesWithoutCoordinats = 0;
			addressesWithoutRoutes = 0;
			totalBottlesCountAtDay = 0;
			bottlesWithoutRL = 0;
			addressesOverlay.Clear();

			//добавляем маркеры складов
			foreach(var b in ViewModel.GeographicGroupsExceptEast)
			{
				addressesOverlay.Markers.Add(FillBaseMarker(b));
			}

			var ordersOnDay = ViewModel.OrdersOnDay;
			var ordersRouteLists = ViewModel.OrderRepository.GetAllRouteListsForOrders(ViewModel.UoW, ordersOnDay);
			//добавляем маркеры адресов заказов
			foreach(var order in ordersOnDay) {
				totalBottlesCountAtDay += order.Total19LBottlesToDeliver;

				IEnumerable<int> orderRls;
				if(!ordersRouteLists.TryGetValue(order.Id, out orderRls)) {
					orderRls = new List<int>();
				}

				var route = ViewModel.RoutesOnDay.FirstOrDefault(rl => rl.Addresses.Any(a => a.Order.Id == order.Id));

				if(!orderRls.Any()) {
					addressesWithoutRoutes++;
					bottlesWithoutRL += order.Total19LBottlesToDeliver;
				}

				if(order.DeliveryPoint.Latitude.HasValue && order.DeliveryPoint.Longitude.HasValue)
				{
					bool overdueOrder = false;

					List<UndeliveryOrderNode> undeliveryOrderNodes = new List<UndeliveryOrderNode>();

					if(ViewModel.UndeliveredOrdersOnDay != null)
					{
						undeliveryOrderNodes = ViewModel.UndeliveredOrdersOnDay
							.Where(x => x.GuiltySide == GuiltyTypes.Driver || x.GuiltySide == GuiltyTypes.Department)
							.ToList();
					}

					if(undeliveryOrderNodes.Any(x => x.NewOrderId == order.Id))
					{
						overdueOrder = true;
					}

					FillTypeAndShapeMarker(order, route, orderRls, out PointMarkerShape shape, out PointMarkerType type, overdueOrder);

					if(selectedMarkers.FirstOrDefault(m => (m.Tag as Order)?.Id == order.Id) != null)
						type = PointMarkerType.white;

					var addressMarker = FillAddressMarker(order, type, shape, addressesOverlay, route);

					addressesOverlay.Markers.Add(addressMarker);
				}
				else
					addressesWithoutCoordinats++;
			}
			
			UpdateOrdersInfo();
			logger.Info("Ок.");
		}

		private void FillTypeAndShapeMarker(Order order, RouteList route, IEnumerable<int> orderRlsIds, out PointMarkerShape shape, out PointMarkerType type, bool overdueOrder = false)
		{
			shape = ViewModel.GetMarkerShapeFromBottleQuantity(order.Total19LBottlesToDeliver, overdueOrder);
			type = PointMarkerType.black;

			if(!orderRlsIds.Any()) {
				if((order.DeliverySchedule.To - order.DeliverySchedule.From).TotalHours <= 1)
					type = PointMarkerType.black_and_red;
				else {
					double from = order.DeliverySchedule.From.TotalMinutes;
					double to = order.DeliverySchedule.To.TotalMinutes;
					if(from >= 1080 && to <= 1439)//>= 18:00, <= 23:59
						type = PointMarkerType.grey_stripes;
					else if(from >= 0) {
						if(to <= 720)//<= 12:00
							type = PointMarkerType.red_stripes;
						else if(to <= 900)//<=15:00
							type = PointMarkerType.yellow_stripes;
						else if(to <= 1080)//<= 18:00
							type = PointMarkerType.green_stripes;
					}
				}
			}

			if (route != null)
				type = ViewModel.GetAddressMarker(ViewModel.RoutesOnDay.IndexOf(route));
		}

		private PointMarker FillBaseMarker(GeoGroup geoGroup)
		{
			var geoGroupVersion = geoGroup.GetActualVersionOrNull();
			if(geoGroupVersion == null)
			{
				throw new InvalidOperationException($"Не установлена активная версия данных в части города {geoGroup.Name}");
			}

			var addressMarker = new PointMarker(
				new PointLatLng(
					(double)geoGroupVersion.BaseLatitude,
					(double)geoGroupVersion.BaseLongitude
				),
				PointMarkerType.vodonos,
				PointMarkerShape.custom
			) {
				Tag = geoGroup
			};
			return addressMarker;
		}

		private PointMarker FillAddressMarker(Order order, PointMarkerType type, PointMarkerShape shape, GMapOverlay overlay, RouteList route)
		{
			var addressMarker = new PointMarker(new PointLatLng((double)order.DeliveryPoint.Latitude, (double)order.DeliveryPoint.Longitude), type, shape) {
				Tag = order
			};

			string ttText = order.DeliveryPoint.ShortAddress;
			if(order.Total19LBottlesToDeliver > 0)
				ttText += string.Format("\nБутылей 19л: {0}", order.Total19LBottlesToDeliver);
			if(order.Total6LBottlesToDeliver > 0)
				ttText += string.Format("\nБутылей 6л: {0}", order.Total6LBottlesToDeliver);
			if(order.Total600mlBottlesToDeliver > 0)
				ttText += string.Format("\nБутылей 0,6л: {0}", order.Total600mlBottlesToDeliver);

			ttText += string.Format($"\nЗабор бутылей: {order.BottlesReturn}");

			ttText += string.Format("\nВремя доставки: {0}\nРайон: {1}",
				order.DeliverySchedule?.Name ?? "Не назначено",
				ViewModel.LogisticanDistricts?.FirstOrDefault(x => x.DistrictBorder.Contains(order.DeliveryPoint.NetTopologyPoint))?.DistrictName);

			addressMarker.ToolTipText = ttText;

			var identicalPoint = overlay.Markers.Count(g => g.Position.Lat == (double)order.DeliveryPoint.Latitude && g.Position.Lng == (double)order.DeliveryPoint.Longitude);
			var pointShift = 5;
			if(identicalPoint >= 1) {
				addressMarker.Offset = new System.Drawing.Point(identicalPoint * pointShift, identicalPoint * pointShift);
			}

			if(route != null)
				addressMarker.ToolTipText += string.Format(" Везёт: {0}", route.Driver.ShortName);

			return addressMarker;
		}

		private void Refresh()
		{
			FillDialogAtDay();
			FillFullOrdersInfo();
		}

		void UpdateOrdersInfo()
		{
			textOrdersInfo.Buffer.Text = ViewModel.GetOrdersInfo(addressesWithoutCoordinats, addressesWithoutRoutes, totalBottlesCountAtDay, bottlesWithoutRL);

			if(progressOrders.Adjustment != null) {
				progressOrders.Adjustment.Upper = ViewModel.OrdersOnDay.Count;
				progressOrders.Adjustment.Value = ViewModel.OrdersOnDay.Count - addressesWithoutRoutes;
			}
			if(!ViewModel.OrdersOnDay.Any())
				progressOrders.Text = string.Empty;
			else if(addressesWithoutRoutes == 0)
				progressOrders.Text = "Готово.";
			else
				progressOrders.Text = NumberToTextRus.FormatCase(addressesWithoutRoutes, "Остался {0} заказ", "Осталось {0} заказа", "Осталось {0} заказов");
		}

		void UpdateRoutesPixBuf()
		{
			if(pixbufMarkers != null && ViewModel.RoutesOnDay.Count == pixbufMarkers.Length)
				return;
			pixbufMarkers = new Pixbuf[ViewModel.RoutesOnDay.Count];
			for(int i = 0; i < ViewModel.RoutesOnDay.Count; i++) {
				PointMarkerShape shape = ViewModel.GetMarkerShapeFromBottleQuantity(ViewModel.RoutesOnDay[i].TotalFullBottlesToClient);
				pixbufMarkers[i] = PointMarker.GetIconPixbuf(ViewModel.GetAddressMarker(i).ToString(), shape);
			}
		}

		void RoutesWasUpdated()
		{
			ViewModel.HasNoChanges = false;
			ytreeRoutes.YTreeModel.EmitModelChanged();
		}

		void UpdateRoutesButton()
		{
			var menu = new Menu();
			foreach(var route in ViewModel.RoutesOnDay) {
				var carrierInfo = string.Format("№{0} - {1}", route.Id, route.Driver.ShortName);
				if(route.GeographicGroups.Any())
					carrierInfo = string.Concat(carrierInfo, " (", route.GeographicGroups.First().Name, ')');
				carrierInfo = string.Concat(
					carrierInfo,
					string.Format("; {0} кг; {1} куб.м.", route.Car?.CarModel?.MaxWeight, route.Car?.CarModel?.MaxVolume)
				);
				var item = new MenuItemId<RouteList>(carrierInfo) {
					ID = route
				};
				item.Activated += AddToRLItem_Activated;
				menu.Append(item);
			}
			menu.ShowAll();
			menuAddToRL.Menu = menu;
		}

		void AddToRLItem_Activated(object sender, EventArgs e)
		{
			bool ordersAdded = false;
			try {
				ordersAdded = ViewModel.AddOrdersToRouteList(GetSelectedOrders(), ((MenuItemId<RouteList>)sender).ID);
			} catch(Exception ex) {
				MessageDialogHelper.RunErrorDialog(
					"Возникла ошибка при добавлении адресов, возможно из-за одновременного добавления одного адреса несколькими пользователями.\n" +
					"Данные для формирования будут автоматически обновлены для продолжения работы.\n" +
					"Повторите попытку добавления адресов.\n" +
					$"Текст ошибки: {ex.Message}", "Ошибка при добавлении адресов"
				);
				Refresh();
				return;
			}
			if(ordersAdded) {
				UpdateAddressesOnMap();
				UpdateMarkersInDriverDistricts(((MenuItemId<RouteList>)sender).ID.Driver);
				ytreeRoutes.YTreeModel.EmitModelChanged();
				TurnOffCheckShowOnlyDriverOrders();
			}
		}

		private void TurnOffCheckShowOnlyDriverOrders()
		{
			if(checkShowOnlyDriverOrders.Active) {
				checkShowOnlyDriverOrders.Active = false;
				driverDistrictsOverlay.IsVisibile = false;
				driverAddressesOverlay.IsVisibile = false;
				addressesOverlay.IsVisibile = true;
			}
			routeOverlay.Clear();
		}

		private IList<Order> GetSelectedOrders()
		{
			List<Order> orders = new List<Order>();
			//Добавление заказов из кликов по маркеру
			orders.AddRange(selectedMarkers.Select(m => m.Tag).OfType<Order>().ToList());
			//Добавление заказов из квадратного выделения
			orders.AddRange(addressesOverlay.Markers
				.Where(m => gmapWidget.SelectedArea.Contains(m.Position))
				.Select(x => x.Tag).OfType<Order>().ToList());
			//Добавление закзаов через непрямоугольную область
			GMapOverlay overlay = gmapWidget.Overlays.FirstOrDefault(o => o.Id.Contains(selectionOverlay.Id));
			GMapPolygon polygons = overlay?.Polygons.FirstOrDefault(p => p.Name.ToLower().Contains("выделение"));
			if(polygons != null) {
				var temp = addressesOverlay.Markers
					.Where(m => polygons.IsInside(m.Position))
					.Select(x => x.Tag).OfType<Order>().ToList();
				orders.AddRange(temp);
			}

			return orders;
		}

		protected void FillItems()
		{
			if(ViewModel.DateForRouting != default(DateTime))
				FillDialogAtDay();
		}

		void LoadDistrictsGeometry()
		{
			logger.Info("Загружаем районы...");
			districtsOverlay.Clear();
			ViewModel.LogisticanDistricts = ViewModel.ScheduleRestrictionRepository.GetDistrictsWithBorder(ViewModel.UoW);
			foreach(var district in ViewModel.LogisticanDistricts) {
				var poligon = new GMapPolygon(
					district.DistrictBorder.Coordinates.Select(p => new PointLatLng(p.X, p.Y)).ToList(),
					district.DistrictName
				);
				districtsOverlay.Polygons.Add(poligon);
			}
			logger.Info("Ок.");
		}

		void LoadDriverDistrictsGeometry(Employee driver)
		{
			if(driver != ViewModel.DriverFromRouteList) {
				driverDistrictsOverlay.Clear();

				var driverDistricts = driver.DriverDistrictPrioritySets
					.SingleOrDefault(x => x.IsActive)
					?.DriverDistrictPriorities.Select(x => x.District)
					.ToList();

				if(driverDistricts == null || !driverDistricts.Any()) {
					return;
				}
				
				foreach(var district in driverDistricts) {
					var poligon = new GMapPolygon(
						district.DistrictBorder.Coordinates.Select(p => new PointLatLng(p.X, p.Y)).ToList(),
						district.DistrictName
					);
					switch(driverDistricts.IndexOf(district) + 1) {
						case 1:
							poligon.Fill = new SolidBrush(System.Drawing.Color.FromArgb(155, System.Drawing.Color.LightGreen));
							break;
						case 2:
							poligon.Fill = new SolidBrush(System.Drawing.Color.FromArgb(155, System.Drawing.Color.Yellow));
							break;
						case 3:
							poligon.Fill = new SolidBrush(System.Drawing.Color.FromArgb(155, System.Drawing.Color.Orange));
							break;
					}
					driverDistrictsOverlay.Polygons.Add(poligon);
				}
			}
		}

		private void ShowOrdersInDriverDistricts(Employee driver)
		{
			if(driver != ViewModel.DriverFromRouteList) {
				UpdateMarkersInDriverDistricts(driver);
				ViewModel.DriverFromRouteList = driver;
			}

			if(ViewModel.ShowOnlyDriverOrders) {
				driverDistrictsOverlay.IsVisibile = true;
				driverAddressesOverlay.IsVisibile = true;
				addressesOverlay.IsVisibile = false;
			} else {
				driverDistrictsOverlay.IsVisibile = false;
				driverAddressesOverlay.IsVisibile = false;
				addressesOverlay.IsVisibile = true;
			}
		}

		private void UpdateMarkersInDriverDistricts(Employee driver)
		{
			driverAddressesOverlay.Clear();

			foreach(var b in ViewModel.GeographicGroupsExceptEast)
			{
				driverAddressesOverlay.Markers.Add(FillBaseMarker(b));
			}

			var driverAddresses = ViewModel.RoutesOnDay
				.Where(r => r.Driver.Id == driver.Id)
				.SelectMany(x => x.Addresses)
				.ToList();
			
			// добавляем маркеры заказов из маршрутников водителя
			if(driverAddresses.Any()) {
				foreach(var address in driverAddresses) {
					var addressMarker = FillAddressMarker(address.Order,
						ViewModel.GetAddressMarker(ViewModel.RoutesOnDay.IndexOf(address.RouteList)),
						ViewModel.GetMarkerShapeFromBottleQuantity(address.Order.Total19LBottlesToDeliver),
						driverAddressesOverlay,
						address.RouteList);

					driverAddressesOverlay.Markers.Add(addressMarker);
				}
			}
			
			var driverDistricts = driver.DriverDistrictPrioritySets
				.SingleOrDefault(x => x.IsActive)
				?.DriverDistrictPriorities.Select(x => x.District)
				.ToList();

			if(driverDistricts == null || !driverDistricts.Any()) {
				return;
			}

			var ordersOnDay = ViewModel.OrdersOnDay.Select(x => x)
				.Where(x => x.OrderAddressType != OrderAddressType.Service).ToList();
			var ordersRouteLists = ViewModel.OrderRepository.GetAllRouteListsForOrders(ViewModel.UoW, ordersOnDay);

			//добавляем маркеры нераспределенных заказов из районов водителя
			foreach(var order in ordersOnDay) {
				var route = ViewModel.RoutesOnDay.FirstOrDefault(rl => rl.Addresses.Any(a => a.Order.Id == order.Id));
			
				if(order.DeliveryPoint.Latitude.HasValue && order.DeliveryPoint.Longitude.HasValue) {
					if(!ordersRouteLists.TryGetValue(order.Id, out var orderRls)) {
						orderRls = new List<int>();
					}
			
					if(driverDistricts.Contains(order.DeliveryPoint.District) && route == null) {
						FillTypeAndShapeMarker(order, null, orderRls, out PointMarkerShape shape, out PointMarkerType type);
						var addressMarker = FillAddressMarker(order, type, shape, driverAddressesOverlay, null);
						driverAddressesOverlay.Markers.Add(addressMarker);
					}
				}
			}
		}

		bool creatingInProgress;
		protected void OnButtonAutoCreateClicked(object sender, EventArgs e)
		{
			if(ViewModel.DateForRouting < DateTime.Today.AddDays(-1) && !ViewModel.CanСreateRoutelistInPastPeriod)
			{
				ViewModel.CommonServices.InteractiveService.ShowMessage(
					ImportanceLevel.Warning,
					"Нельзя создавать маршруты на дату ранее вчерашнего дня!");
				return;
			}
			
			UpdateWarningButton();

			if(creatingInProgress) {
				buttonAutoCreate.Label = "Создать маршруты";
				return;
			}

			creatingInProgress = true;
			buttonAutoCreate.Label = "Остановить";

			if(ViewModel.CreateRoutesAutomatically(txt => textOrdersInfo.Buffer.Text = txt)) {
				UpdateRoutesPixBuf();
				UpdateRoutesButton();
				UpdateAddressesOnMap();
				RoutesWasUpdated();
				ViewModel.IsAutoroutingModeActive = true;
			}
			UpdateWarningButton();
			creatingInProgress = false;
			buttonAutoCreate.Label = "Создать маршруты";
		}

		protected void OnButtonDriverSelectAutoClicked(object sender, EventArgs e)
		{
			var driver = ytreeviewOnDayDrivers.GetSelectedObjects<AtWorkDriver>().FirstOrDefault();
			
			if(driver == null)
			{
				ViewModel.CommonServices.InteractiveService.ShowMessage(ImportanceLevel.Warning, "Не выбран водитель!");
				return;
			}
			
			var filter = new CarJournalFilterViewModel(ViewModel.CarModelJournalFactory);
			filter.SetAndRefilterAtOnce(
				x => x.Archive = false,
				x => x.RestrictedCarOwnTypes = new List<CarOwnType> { CarOwnType.Company }
			);
			var journal = new CarJournalViewModel(filter, UnitOfWorkFactory.GetDefaultFactory, ViewModel.CommonServices);
			journal.SelectionMode = JournalSelectionMode.Single;
			journal.OnEntitySelectedResult += (o, args) =>
			{
				var car = ViewModel.UoW.GetById<Car>(args.SelectedNodes.First().Id);
				ViewModel.SelectCarForDriver(driver, car);
			};
			ViewModel.TabParent.AddSlaveTab(ViewModel, journal);
		}

		void OnLoadTimeEdited(object o, EditedArgs args)
		{
			var routeList = (RouteList)ytreeRoutes.YTreeModel.NodeAtPath(new TreePath(args.Path));
			bool NeedRecalculate = false;

			if(string.IsNullOrWhiteSpace(args.NewText)) {
				NeedRecalculate = routeList.OnloadTimeFixed;
				routeList.OnloadTimeFixed = false;
			} else if(TimeSpan.TryParse(args.NewText, out TimeSpan fixedTime)) {
				if(fixedTime != routeList.OnLoadTimeStart)
					NeedRecalculate = true;
				routeList.OnloadTimeFixed = true;
				routeList.OnLoadTimeStart = fixedTime;
				routeList.OnLoadTimeEnd = fixedTime.Add(TimeSpan.FromMinutes(routeList.TimeOnLoadMinuts));
			}

			if(NeedRecalculate)
				ViewModel.RecalculateOnLoadTime();
		}

		private void UpdateWarningButton()
		{
			buttonWarnings.Visible = ViewModel.Optimizer.WarningMessages.Any();
			buttonWarnings.Label = ViewModel.Optimizer.WarningMessages.Count.ToString();
		}

		protected void OnFilterWidgetEvent(object o, WidgetEventArgs args)
		{
			if(args.Event.Type == EventType.KeyPress) {
				EventKey eventKey = args.Args.OfType<EventKey>().FirstOrDefault();
				if(eventKey != null && (eventKey.Key == Gdk.Key.Return || eventKey.Key == Gdk.Key.KP_Enter)) {
					FillItems();
				}
			}
		}

		public override void Destroy()
		{
			ViewModel.Dispose();
			base.Destroy();
		}
	}
}
