using QS.Project.Journal.EntitySelector;
using QS.Services;
using QS.ViewModels;
using System.Collections.Generic;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Contacts;

namespace Vodovoz.ViewModels.ViewModels.Contacts
{
	public class PhoneViewModel : WidgetViewModelBase
	{
		private Phone _phone;
		private bool _readOnly = false;
		public bool CanReadCounterpartyName { get; }
		public bool CanEditCounterpartyName { get; }
		public bool CanReadCounterpartyPatronymic { get; }
		public bool CanEditCounterpartyPatronymic { get; }

		public IList<PhoneType> PhoneTypes;
		public IEntityAutocompleteSelectorFactory RoboAtsCounterpartyNameSelectorFactory { get; }
		public IEntityAutocompleteSelectorFactory RoboAtsCounterpartyPatronymicSelectorFactory { get; }
		public PhoneType SelectedPhoneType
		{
			get => Phone.PhoneType;
			set => SetPhoneType(value);
		}
		public Phone Phone
		{
			get => _phone;
			set => SetField(ref _phone, value);
		}
		public virtual bool ReadOnly
		{
			get => _readOnly;
			set => SetField(ref _readOnly, value);
		}

		public PhoneViewModel(Phone phone,
			IEntityAutocompleteSelectorFactory roboAtsCounterpartyNameSelectorFactory,
			IEntityAutocompleteSelectorFactory roboAtsCounterpartyPatronymicSelectorFactory,
			ICommonServices commonServices)
		{
			_phone = phone;
			RoboAtsCounterpartyNameSelectorFactory = roboAtsCounterpartyNameSelectorFactory;
			RoboAtsCounterpartyPatronymicSelectorFactory = roboAtsCounterpartyPatronymicSelectorFactory;

			var roboAtsCounterpartyNamePermissions = commonServices.CurrentPermissionService.ValidateEntityPermission(typeof(RoboAtsCounterpartyName));
			CanReadCounterpartyName = roboAtsCounterpartyNamePermissions.CanRead;
			CanEditCounterpartyName = roboAtsCounterpartyNamePermissions.CanUpdate;

			var roboAtsCounterpartyPatronymicPermissions = commonServices.CurrentPermissionService.ValidateEntityPermission(typeof(RoboAtsCounterpartyPatronymic));
			CanReadCounterpartyPatronymic = roboAtsCounterpartyPatronymicPermissions.CanRead;
			CanEditCounterpartyPatronymic = roboAtsCounterpartyPatronymicPermissions.CanUpdate;
		}

		private void SetPhoneType(PhoneType phoneType)
		{
			Phone.PhoneType = phoneType;
			if (phoneType.Id == 11)
			{

			}
		}
	}
}
