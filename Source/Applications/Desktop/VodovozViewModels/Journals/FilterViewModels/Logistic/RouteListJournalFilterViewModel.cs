﻿using QS.Project.Filter;
using QS.Project.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Gamma.Utilities;
using QS.Commands;
using QS.Dialog;
using QS.Utilities.Enums;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Logistic.Cars;
using Vodovoz.Domain.Sale;
using Vodovoz.EntityRepositories;
using Vodovoz.ViewModels.Logistic;

namespace Vodovoz.ViewModels.Journals.FilterViewModels.Logistic
{
	public class RouteListJournalFilterViewModel : FilterViewModelBase<RouteListJournalFilterViewModel>
	{
		private bool _showDriversWithTerminal;
		private bool _hasAccessToDriverTerminal;
		private DeliveryShift _deliveryShift;
		private DateTime? _startDate;
		private DateTime? _endDate;
		private GeoGroup _geographicGroup;
		private List<AddressTypeNode> _addressTypeNodes = new List<AddressTypeNode>();
		private List<RouteListStatusNode> _statusNodes;
		private IList<CarOwnType> _restrictedCarOwnTypes;
		private IList<CarTypeOfUse> _restrictedCarTypesOfUse;
		private DelegateCommand _infoCommand;

		public RouteListJournalFilterViewModel()
		{
			_statusNodes = EnumHelper.GetValuesList<RouteListStatus>().Select(x => new RouteListStatusNode(x) { Selected = true }).ToList();

			foreach(var addressType in Enum.GetValues(typeof(AddressType)).Cast<AddressType>())
			{
				var newAddressTypeNode = new AddressTypeNode(addressType);
				newAddressTypeNode.PropertyChanged += OnStatusCheckChanged;
				_addressTypeNodes.Add(newAddressTypeNode);
			}

			GeographicGroups = UoW.GetAll<GeoGroup>().ToList();

			var currentUserSettings = new UserRepository().GetUserSettings(UoW, ServicesConfig.CommonServices.UserService.CurrentUserId);

			foreach(var addressTypeNode in AddressTypeNodes)
			{
				switch(addressTypeNode.AddressType)
				{
					case AddressType.Delivery:
						addressTypeNode.Selected = currentUserSettings.LogisticDeliveryOrders;
						break;
					case AddressType.Service:
						addressTypeNode.Selected = currentUserSettings.LogisticServiceOrders;
						break;
					case AddressType.ChainStore:
						addressTypeNode.Selected = currentUserSettings.LogisticChainStoreOrders;
						break;
				}
			}

			_restrictedCarOwnTypes = EnumHelper.GetValuesList<CarOwnType>();
			_restrictedCarTypesOfUse = EnumHelper.GetValuesList<CarTypeOfUse>();

			var cashier = ServicesConfig.CommonServices.CurrentPermissionService.ValidatePresetPermission("role_сashier");
			var logistician = ServicesConfig.CommonServices.CurrentPermissionService.ValidatePresetPermission("logistican");
			HasAccessToDriverTerminal = cashier || logistician;

			SubscribeOnCheckChanged();
		}

		public IList<GeoGroup> GeographicGroups { get; }

		public bool ShowDriversWithTerminal
		{
			get => _showDriversWithTerminal;
			set => UpdateFilterField(ref _showDriversWithTerminal, value);
		}

		public bool HasAccessToDriverTerminal
		{
			get => _hasAccessToDriverTerminal;
			set => UpdateFilterField(ref _hasAccessToDriverTerminal, value);
		}

		public DeliveryShift DeliveryShift
		{
			get => _deliveryShift;
			set => UpdateFilterField(ref _deliveryShift, value);
		}

		public DateTime? StartDate
		{
			get => _startDate;
			set => UpdateFilterField(ref _startDate, value);
		}

		public DateTime? EndDate
		{
			get => _endDate;
			set => UpdateFilterField(ref _endDate, value);
		}

		public IList<CarTypeOfUse> RestrictedCarTypesOfUse
		{
			get => _restrictedCarTypesOfUse;
			set => UpdateFilterField(ref _restrictedCarTypesOfUse, value);
		}

		public IList<CarOwnType> RestrictedCarOwnTypes
		{
			get => _restrictedCarOwnTypes;
			set => UpdateFilterField(ref _restrictedCarOwnTypes, value);
		}

		public GeoGroup GeographicGroup
		{
			get => _geographicGroup;
			set => UpdateFilterField(ref _geographicGroup, value);
		}

		#region RouteListStatus

		public RouteListStatus[] DisplayableStatuses
		{
			get => StatusNodes.Select(rn => rn.RouteListStatus).ToArray();
			set
			{
				foreach(var status in value)
				{
					if(_statusNodes.All(sn => sn.RouteListStatus != status))
					{
						_statusNodes.Add(new RouteListStatusNode(status));
					}
				}

				_statusNodes.RemoveAll(rn => !value.Contains(rn.RouteListStatus));

				FirePropertyChanged();
			}
		}

		/// <summary>
		/// Статусы в фильтре предустановлены и не могут изменяться, если в поле есть хотя бы 1 статус
		/// </summary>
		public RouteListStatus[] RestrictedByStatuses
		{
			get
			{
				if(CanSelectStatuses)
				{
					return new RouteListStatus[] { };
				}
				else
				{
					return SelectedStatuses;
				}
			}
			set
			{
				if(value.Any())
				{
					DisplayableStatuses = value;
					SelectedStatuses = value;
					CanSelectStatuses = false;
				}
				else
				{
					CanSelectStatuses = true;
				}
			}
		}

		/// <summary>
		/// Статусы в фильтре предустановлены и не могут изменяться, если в поле есть хотя бы 1 статус
		/// </summary>
		public RouteListStatus[] SelectedStatuses
		{
			get
			{
				return StatusNodes.Where(rn => rn.Selected)
					.Select(rn => rn.RouteListStatus).ToArray();
			}
			set
			{
				foreach(var status in _statusNodes.Where(rn => value.Contains(rn.RouteListStatus)))
				{
					status.Selected = true;
				}
				FirePropertyChanged();
			}
		}

		public List<RouteListStatusNode> StatusNodes
		{
			get => _statusNodes;
			private set
			{
				UnsubscribeOnCheckChanged();
				UpdateFilterField(ref _statusNodes, value);
				SubscribeOnCheckChanged();
			}
		}

		#endregion

		/// <summary>
		/// Типы выездов
		/// </summary>
		public List<AddressTypeNode> AddressTypeNodes
		{
			get => _addressTypeNodes;
			set => _addressTypeNodes = value;
		}

		public bool WithDeliveryAddresses => AddressTypeNodes.Any(an => an.AddressType == AddressType.Delivery && an.Selected);

		public bool WithServiceAddresses => AddressTypeNodes.Any(an => an.AddressType == AddressType.Service && an.Selected);

		public bool WithChainStoreAddresses => AddressTypeNodes.Any(an => an.AddressType == AddressType.ChainStore && an.Selected);

		public bool CanSelectStatuses { get; private set; } = true;
		
		public decimal RouteListProfitabilityIndicator { get; set; }

		public void SelectAllRouteListStatuses()
		{
			_statusNodes.ForEach(x => x.Selected = true);
			Update();
		}

		public void DeselectAllRouteListStatuses()
		{
			_statusNodes.ForEach(x => x.Selected = false);
			Update();
		}
		
		public DelegateCommand InfoCommand => _infoCommand ?? (_infoCommand = new DelegateCommand(
			() => ServicesConfig.InteractiveService.ShowMessage(
				ImportanceLevel.Info,
				"Описание расцветки строк журнала МЛ:\n\n" +
				"Если МЛ отправлен В Путь пользователем без полной загрузки, то запись строки будет сделана оранжевым цветом\n" +
				$"Если Рентабельность рейса меньше {RouteListProfitabilityIndicator}% и МЛ не в статусе {RouteListStatus.New.GetEnumTitle()}," +
				" то строка будет выделена серым цветом\n" +
				"В остальных случаях используется стандартная расцветка")
		));

		private void SubscribeOnCheckChanged()
		{
			foreach(var statusNode in StatusNodes)
			{
				statusNode.PropertyChanged += OnStatusCheckChanged;
			}
		}

		private void UnsubscribeOnCheckChanged()
		{
			foreach(var statusNode in StatusNodes)
			{
				statusNode.PropertyChanged -= OnStatusCheckChanged;
			}
		}

		private void OnStatusCheckChanged(object sender, PropertyChangedEventArgs e)
		{
			Update();
		}

		public override void Dispose()
		{
			UnsubscribeOnCheckChanged();
			_addressTypeNodes.ForEach(x => x.PropertyChanged -= OnStatusCheckChanged);
			base.Dispose();
		}
	}
}
