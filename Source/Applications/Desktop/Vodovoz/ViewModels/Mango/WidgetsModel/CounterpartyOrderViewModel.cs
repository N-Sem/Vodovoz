﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using QS.DomainModel.NotifyChange;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Domain;
using QS.Project.Services;
using QS.Utilities.Extensions;
using QS.ViewModels;
using Vodovoz.Core.DataService;
using Vodovoz.Dialogs;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.CallTasks;
using Vodovoz.EntityRepositories.Employees;
using Vodovoz.EntityRepositories.Goods;
using Vodovoz.EntityRepositories.Logistic;
using Vodovoz.EntityRepositories.Orders;
using Vodovoz.Infrastructure.Mango;
using Vodovoz.Parameters;
using Vodovoz.Services;
using Vodovoz.TempAdapters;
using Vodovoz.Tools;
using Vodovoz.Tools.CallTasks;
using Vodovoz.ViewModels.Complaints;
using Vodovoz.ViewModels.Journals.JournalViewModels.Orders;

namespace Vodovoz.ViewModels.Mango
{
	public class CounterpartyOrderViewModel : ViewModelBase
	{
		#region Свойства
		public Counterparty Client { get; private set; }
		private ITdiCompatibilityNavigation tdiNavigation;
		private MangoManager MangoManager { get; set; }
		private readonly IOrderParametersProvider _orderParametersProvider;

		private readonly IDeliveryRulesParametersProvider _deliveryRulesParametersProvider;
		private readonly IRouteListRepository _routedListRepository;
		private readonly IEmployeeJournalFactory _employeeJournalFactory;
		private readonly ICounterpartyJournalFactory _counterpartyJournalFactory;
		private readonly INomenclatureRepository _nomenclatureRepository;
		private readonly IParametersProvider _parametersProvider;
		private readonly IEmployeeRepository _employeeRepository = new EmployeeRepository();
		private readonly IOrderRepository _orderRepository = new OrderRepository();
		private readonly IRouteListItemRepository _routeListItemRepository = new RouteListItemRepository();

		private IUnitOfWork UoW;

		public List<Order> LatestOrder { get; private set; }
		public Order Order { get; set; }

		public Action RefreshOrders { get; private set; }
		#endregion

		#region Конструкторы

		public CounterpartyOrderViewModel(Counterparty client,
			IUnitOfWorkFactory unitOfWorkFactory,
			ITdiCompatibilityNavigation tdinavigation,
			IRouteListRepository routedListRepository,
			MangoManager mangoManager,
			IOrderParametersProvider orderParametersProvider,
			IEmployeeJournalFactory employeeJournalFactory,
			ICounterpartyJournalFactory counterpartyJournalFactory,
			INomenclatureRepository nomenclatureRepository,
			IParametersProvider parametersProvider,
			IDeliveryRulesParametersProvider deliveryRulesParametersProvider,
			int count = 5)
		{
			Client = client;
			tdiNavigation = tdinavigation;
			_routedListRepository = routedListRepository;
			MangoManager = mangoManager;
			_orderParametersProvider = orderParametersProvider ?? throw new ArgumentNullException(nameof(orderParametersProvider));
			_employeeJournalFactory = employeeJournalFactory ?? throw new ArgumentNullException(nameof(employeeJournalFactory));
			_counterpartyJournalFactory = counterpartyJournalFactory ?? throw new ArgumentNullException(nameof(counterpartyJournalFactory));
			_nomenclatureRepository = nomenclatureRepository ?? throw new ArgumentNullException(nameof(nomenclatureRepository));
			_parametersProvider = parametersProvider ?? throw new ArgumentNullException(nameof(parametersProvider));
			_deliveryRulesParametersProvider = deliveryRulesParametersProvider ?? throw new ArgumentNullException(nameof(deliveryRulesParametersProvider));
			UoW = unitOfWorkFactory.CreateWithoutRoot();
			LatestOrder = _orderRepository.GetLatestOrdersForCounterparty(UoW, client, count).ToList();

			RefreshOrders = _RefreshOrders;
			NotifyConfiguration.Instance.BatchSubscribe(_RefreshCounterparty)
				.IfEntity<Counterparty>()
				.AndWhere(c => c.Id == client.Id)
				.Or.IfEntity<DeliveryPoint>()
				.AndWhere(d => client.DeliveryPoints.Any(cd => cd.Id == d.Id));

		}
		#endregion

		#region Функции

		#region privates
		private void _RefreshCounterparty(EntityChangeEvent[] entity)
		{
			Client = UoW.GetById<Counterparty>(Client.Id);
		}
		private void _RefreshOrders()
		{
			LatestOrder = _orderRepository.GetLatestOrdersForCounterparty(UoW, Client, 5).ToList();
			OnPropertyChanged(nameof(LatestOrder));
		}
		#endregion
		
		public void OpenMoreInformationAboutCounterparty()
		{
			var page = tdiNavigation.OpenTdiTab<CounterpartyDlg, int>(null, Client.Id, OpenPageOptions.IgnoreHash);
			var tab = page.TdiTab as CounterpartyDlg;
		}
		public void OpenMoreInformationAboutOrder(int id)
		{
			var page = tdiNavigation.OpenTdiTab<OrderDlg, int>(null, id, OpenPageOptions.IgnoreHash);
		}

		public void RepeatOrder(Order order)
		{
			if(order != null)
				tdiNavigation.OpenTdiTab<OrderDlg, Order, bool>(null, order, true, OpenPageOptions.IgnoreHash);
		}

		public void OpenRoutedList(Order order)
		{
			if(order.OrderStatus == OrderStatus.NewOrder ||
				order.OrderStatus == OrderStatus.Accepted ||
				order.OrderStatus == OrderStatus.OnLoading
			) {
				tdiNavigation.OpenTdiTab<RouteListCreateDlg>(null);
			} else if(order.OrderStatus == OrderStatus.OnTheWay ||
			          order.OrderStatus == OrderStatus.InTravelList ||
			          order.OrderStatus == OrderStatus.Closed
			) {
				RouteList routeList = _routedListRepository.GetActualRouteListByOrder(UoW, order);
				if(routeList != null)
					tdiNavigation.OpenTdiTab<RouteListKeepingDlg, RouteList>(null, routeList);
				
			} else if (order.OrderStatus == OrderStatus.Shipped) {
				RouteList routeList = _routedListRepository.GetActualRouteListByOrder(UoW, order);
				if(routeList != null)
					tdiNavigation.OpenTdiTab<RouteListClosingDlg,RouteList>(null, routeList);
			}
		}

		public void OpenUndelivery(Order order)
		{
			var page = tdiNavigation.OpenTdiTab<UndeliveredOrdersJournalViewModel>(null);
			var dlg = page.TdiTab as UndeliveredOrdersJournalViewModel;
			var filter = dlg.UndeliveredOrdersFilterViewModel;
			filter.HidenByDefault = true;
			filter.RestrictOldOrder = order;
			filter.RestrictOldOrderStartDate = order.DeliveryDate;
			filter.RestrictOldOrderEndDate = order.DeliveryDate;
		}

		public void CancelOrder(Order order)
		{
			CallTaskWorker callTaskWorker = new CallTaskWorker(
							CallTaskSingletonFactory.GetInstance(),
							new CallTaskRepository(),
							_orderRepository,
							_employeeRepository,
							new BaseParametersProvider(_parametersProvider),
							ServicesConfig.CommonServices.UserService,
							ErrorReporter.Instance);

			if(order.OrderStatus == OrderStatus.InTravelList)
			{

				ValidationContext validationContext = new ValidationContext(order, null, new Dictionary<object, object> {
					{ "NewStatus", OrderStatus.Canceled },
				});
				validationContext.ServiceContainer.AddService(_orderParametersProvider);
				validationContext.ServiceContainer.AddService(_deliveryRulesParametersProvider);
				if(!ServicesConfig.ValidationService.Validate(order, validationContext))
				{
					return;
				}

				ITdiPage page = tdiNavigation.OpenTdiTab<UndeliveryOnOrderCloseDlg, Order, IUnitOfWork>(null, order, UoW);
				page.PageClosed += (sender, e) => {
					order.SetUndeliveredStatus(UoW, new BaseParametersProvider(_parametersProvider), callTaskWorker);

					var routeListItem = _routeListItemRepository.GetRouteListItemForOrder(UoW, order);
					if(routeListItem != null && routeListItem.Status != RouteListItemStatus.Canceled) {
						routeListItem.RouteList.SetAddressStatusWithoutOrderChange(routeListItem.Id, RouteListItemStatus.Canceled);
						routeListItem.StatusLastUpdate = DateTime.Now;
						routeListItem.SetOrderActualCountsToZeroOnCanceled();
						UoW.Save(routeListItem.RouteList);
						UoW.Save(routeListItem);
					}

					UoW.Commit();
				};
			} else {
				order.ChangeStatusAndCreateTasks(OrderStatus.Canceled, callTaskWorker);
				UoW.Save(order);
				UoW.Commit();
			}
		}

		public void CreateComplaint(Order order)
		{
			if (order != null)
			{
				var employeeSelectorFactory = _employeeJournalFactory.CreateEmployeeAutocompleteSelectorFactory();

				var counterpartySelectorFactory = _counterpartyJournalFactory.CreateCounterpartyAutocompleteSelectorFactory();

				var parameters = new Dictionary<string, object> {
					{"order", order},
					{"uowBuilder", EntityUoWBuilder.ForCreate()},
					{ "unitOfWorkFactory",UnitOfWorkFactory.GetDefaultFactory },
					{"employeeSelectorFactory", employeeSelectorFactory},
					{"counterpartySelectorFactory", counterpartySelectorFactory},
					{"phone", "+7" +this.MangoManager.CurrentCall.Phone.Number }
				};
				tdiNavigation.OpenTdiTabOnTdiNamedArgs<CreateComplaintViewModel>(null, parameters);
			}
		}
		#endregion
	}
}
