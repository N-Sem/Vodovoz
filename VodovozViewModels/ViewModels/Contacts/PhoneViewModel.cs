using QS.DomainModel.UoW;
using QS.Project.Journal.EntitySelector;
using QS.Services;
using QS.ViewModels;
using System;
using System.Collections.Generic;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Contacts;
using Vodovoz.EntityRepositories;
using Vodovoz.Services;

namespace Vodovoz.ViewModels.ViewModels.Contacts
{
	public class PhoneViewModel : WidgetViewModelBase
	{
		private Phone _phone;
		private bool _readOnly = false;
		private bool _canArchivateNumber;
		private readonly IPhoneTypeSettings _phoneTypeSettings;
		private readonly ICommonServices _commonServices;
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

		public bool PhoneIsArchive
		{
			get => Phone.IsArchive;
			set => Phone.IsArchive = value;
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
			IPhoneRepository phoneRepository,
			ICommonServices commonServices,
			IUnitOfWork uow,
			IPhoneTypeSettings phoneTypeSettings,
			bool readOnly)
		{
			_readOnly = readOnly;
			_phone = phone ?? throw new ArgumentNullException(nameof(phoneRepository));
			_phoneTypeSettings = phoneTypeSettings ?? throw new ArgumentNullException(nameof(phoneTypeSettings));
			_commonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));
			RoboAtsCounterpartyNameSelectorFactory = roboAtsCounterpartyNameSelectorFactory ?? throw new ArgumentNullException(nameof(roboAtsCounterpartyNameSelectorFactory));
			RoboAtsCounterpartyPatronymicSelectorFactory = roboAtsCounterpartyPatronymicSelectorFactory ?? throw new ArgumentNullException(nameof(roboAtsCounterpartyPatronymicSelectorFactory));
			PhoneTypes = phoneRepository.GetPhoneTypes(uow) ?? throw new ArgumentNullException(nameof(phoneRepository));

			var roboAtsCounterpartyNamePermissions = commonServices.CurrentPermissionService.ValidateEntityPermission(typeof(RoboAtsCounterpartyName));
			CanReadCounterpartyName = roboAtsCounterpartyNamePermissions.CanRead;
			CanEditCounterpartyName = roboAtsCounterpartyNamePermissions.CanUpdate;

			var roboAtsCounterpartyPatronymicPermissions = commonServices.CurrentPermissionService.ValidateEntityPermission(typeof(RoboAtsCounterpartyPatronymic));
			CanReadCounterpartyPatronymic = roboAtsCounterpartyPatronymicPermissions.CanRead;
			CanEditCounterpartyPatronymic = roboAtsCounterpartyPatronymicPermissions.CanUpdate;
			_canArchivateNumber = _commonServices.CurrentPermissionService.ValidateEntityPermission(typeof(Phone)).CanUpdate;
		}

		private void SetPhoneType(PhoneType phoneType)
		{
			if(phoneType.Id == _phoneTypeSettings.ArchiveId)
			{
				if(_canArchivateNumber && !_commonServices.InteractiveService.Question("Номер будет переведен в архив и пропадет в списке активных. Продолжить?"))
				{
					return;
				}
				PhoneIsArchive = true;
			}
			else
			{
				if(PhoneIsArchive)
				{
					PhoneIsArchive = false;
				}
			}
			Phone.PhoneType = phoneType;
		}
	}
}
