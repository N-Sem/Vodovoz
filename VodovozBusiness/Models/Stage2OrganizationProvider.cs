﻿using System;
using System.Collections.Generic;
using System.Linq;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.OrdersWithoutShipment;
using Vodovoz.Domain.Organizations;
using Vodovoz.Domain.Sale;
using Vodovoz.Services;

namespace Vodovoz.Models
{
	public class Stage2OrganizationProvider : IOrganizationProvider
	{
		private readonly IOrganizationParametersProvider _organizationParametersProvider;
		private readonly IOrderParametersProvider _orderParametersProvider;
		private readonly IGeographicGroupParametersProvider _geographicGroupParametersProvider;

		public Stage2OrganizationProvider(
			IOrganizationParametersProvider organizationParametersProvider,
			IOrderParametersProvider orderParametersProvider,
			IGeographicGroupParametersProvider geographicGroupParametersProvider)
		{
			_organizationParametersProvider = organizationParametersProvider
				?? throw new ArgumentNullException(nameof(organizationParametersProvider));
			_orderParametersProvider = orderParametersProvider
				?? throw new ArgumentNullException(nameof(orderParametersProvider));
			_geographicGroupParametersProvider = geographicGroupParametersProvider
				?? throw new ArgumentNullException(nameof(geographicGroupParametersProvider));
		}

		public Organization GetOrganizationForOrderWithoutShipment(IUnitOfWork uow, OrderWithoutShipmentForAdvancePayment order)
		{
			if(uow == null)
			{
				throw new ArgumentNullException(nameof(uow));
			}
			if(order == null)
			{
				throw new ArgumentNullException(nameof(order));
			}

			var organizationId = IsOnlineStoreOrderWithoutShipment(order)
				? _organizationParametersProvider.VodovozSouthOrganizationId
				: _organizationParametersProvider.VodovozOrganizationId;

			return uow.GetById<Organization>(organizationId);
		}

		/// <summary>
		/// Метод подбора организации.<br/>
		/// Если в заказе установлена наша организация - берем ее.<br/>
		/// Иначе, если у клиента прописана организация с которой он работает - возвращаем ее.<br/>
		/// Иначе подбираем организацию по параметрам: если заполнены параметры paymentFrom и paymentType,
		/// то они берутся для подбора организации вместо соответствующих полей заказа<br/>
		/// </summary>
		/// <param name="uow">Unit Of Work</param>
		/// <param name="order">Заказ, для которого подбираем организацию</param>
		/// <param name="paymentFrom">Источник оплаты, если не null берем его для подбора организации</param>
		/// <param name="paymentType">Тип оплаты, если не null берем его для подбора организации</param>
		/// <returns>Организация для заказа</returns>
		/// <exception cref="ArgumentNullException">Исключение при order = null</exception>
		public Organization GetOrganization(IUnitOfWork uow, Order order, PaymentFrom paymentFrom = null, PaymentType? paymentType = null)
		{
			if(order == null)
			{
				throw new ArgumentNullException(nameof(order));
			}

			if(order.OurOrganization != null)
			{
				return order.OurOrganization;
			}
			if(order.Client.WorksThroughOrganization != null)
			{
				return order.Client.WorksThroughOrganization;
			}

			var isSelfDelivery = order.SelfDelivery || order.DeliveryPoint == null;

			return GetOrganizationForOrderParameters(uow, paymentType ?? order.PaymentType, isSelfDelivery, order.CreateDate,
				order.OrderItems, paymentFrom ?? order.PaymentByCardFrom, order.DeliveryPoint?.District?.GeographicGroup);
		}

		private Organization GetOrganizationForOrderParameters(IUnitOfWork uow, PaymentType paymentType, bool isSelfDelivery,
			DateTime? orderCreateDate, IEnumerable<OrderItem> orderItems, PaymentFrom paymentFrom, GeographicGroup geographicGroup)
		{
			if(uow == null)
			{
				throw new ArgumentNullException(nameof(uow));
			}

			if(HasAnyOnlineStoreNomenclature(orderItems))
			{
				return GetOrganizationForOnlineStore(uow);
			}

			return isSelfDelivery
				? GetOrganizationForSelfDelivery(uow, paymentType, orderCreateDate, paymentFrom, geographicGroup)
				: GetOrganizationForOtherOptions(uow, paymentType, orderCreateDate, paymentFrom, geographicGroup);
		}

		private Organization GetOrganizationForSelfDelivery(IUnitOfWork uow, PaymentType paymentType, DateTime? orderCreateDate,
			PaymentFrom paymentFrom, GeographicGroup geographicGroup)
		{
			int organizationId;
			switch(paymentType)
			{
				case PaymentType.barter:
				case PaymentType.cashless:
				case PaymentType.ContractDoc:
					organizationId = _organizationParametersProvider.VodovozOrganizationId;
					break;
				case PaymentType.cash:
					organizationId = _organizationParametersProvider.VodovozNorthOrganizationId;
					break;
				case PaymentType.Terminal:
					organizationId = _organizationParametersProvider.VodovozNorthOrganizationId;
					break;
				case PaymentType.ByCard:
					organizationId = GetOrganizationIdForByCard(paymentFrom, geographicGroup, orderCreateDate);
					break;
				default:
					throw new NotSupportedException(
						$"Невозможно подобрать организацию, так как тип оплаты {paymentType} не поддерживается.");
			}

			return uow.GetById<Organization>(organizationId);
		}

		private bool HasAnyOnlineStoreNomenclature(IEnumerable<OrderItem> orderItems)
		{
			return orderItems != null
				&& orderItems.Any(x =>
					x.Nomenclature.OnlineStore != null &&
					x.Nomenclature.OnlineStore.Id != _orderParametersProvider.OldInternalOnlineStoreId);
		}

		private Organization GetOrganizationForOnlineStore(IUnitOfWork uow)
		{
			return uow.GetById<Organization>(_organizationParametersProvider.VodovozSouthOrganizationId);
		}

		private Organization GetOrganizationForOtherOptions(IUnitOfWork uow, PaymentType paymentType, DateTime? orderCreateDate,
			PaymentFrom paymentFrom, GeographicGroup geographicGroup)
		{
			int organizationId;
			switch(paymentType)
			{
				case PaymentType.barter:
				case PaymentType.cashless:
				case PaymentType.ContractDoc:
					organizationId = _organizationParametersProvider.VodovozOrganizationId;
					break;
				case PaymentType.cash:
					organizationId = _organizationParametersProvider.VodovozNorthOrganizationId;
					break;
				case PaymentType.Terminal:
					organizationId = _organizationParametersProvider.VodovozNorthOrganizationId;
					break;
				case PaymentType.ByCard:
					organizationId = GetOrganizationIdForByCard(paymentFrom, geographicGroup, orderCreateDate);
					break;
				default:
					throw new NotSupportedException($"Тип оплаты {paymentType} не поддерживается, невозможно подобрать организацию.");
			}

			return uow.GetById<Organization>(organizationId);
		}

		private bool IsOnlineStoreOrderWithoutShipment(OrderWithoutShipmentForAdvancePayment order)
		{
			return order.OrderWithoutDeliveryForAdvancePaymentItems.Any(x =>
				x.Nomenclature.OnlineStore != null && x.Nomenclature.OnlineStore.Id != _orderParametersProvider.OldInternalOnlineStoreId);
		}

		private int GetOrganizationIdForByCard(PaymentFrom paymentFrom, GeographicGroup geographicGroup, DateTime? orderCreateDate)
		{
			if(paymentFrom == null || paymentFrom.Id == _orderParametersProvider.GetPaymentByCardFromFastPaymentServiceId)
			{
				return _organizationParametersProvider.VodovozNorthOrganizationId;
			}
			if(paymentFrom.Id == _orderParametersProvider.GetPaymentByCardFromMarketplaceId)
			{
				return _organizationParametersProvider.VodovozOrganizationId;
			}
			if(paymentFrom.Id == _orderParametersProvider.PaymentByCardFromSmsId)
			{
				if(geographicGroup == null || orderCreateDate == null)
				{
					return _organizationParametersProvider.VodovozNorthOrganizationId;
				}
				if(geographicGroup.Id == _geographicGroupParametersProvider.NorthGeographicGroupId
					&& orderCreateDate.Value.TimeOfDay < _organizationParametersProvider.LatestCreateTimeForSouthOrganizationInByCardOrder)
				{
					return _organizationParametersProvider.VodovozSouthOrganizationId;
				}
				return _organizationParametersProvider.VodovozNorthOrganizationId;
			}
			return _orderParametersProvider.PaymentsByCardFromForNorthOrganization.Contains(paymentFrom.Id)
				? _organizationParametersProvider.VodovozNorthOrganizationId
				: _organizationParametersProvider.VodovozSouthOrganizationId;
		}
	}
}
