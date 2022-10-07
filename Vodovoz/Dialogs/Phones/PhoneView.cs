using Gamma.GtkWidgets;
using Gamma.Widgets;
using Gtk;
using QS.DomainModel.UoW;
using QS.Project.Journal.EntitySelector;
using QS.Services;
using QS.Widgets.GtkUI;
using QSWidgetLib;
using Vodovoz.Domain.Contacts;
using Vodovoz.Services;
using Vodovoz.ViewModels.ViewModels.Contacts;
using Vodovoz.ViewWidgets.Mango;

namespace Vodovoz.Dialogs.Phones
{
	public class PhoneView
	{
		private readonly HBox _hBox;
		private PhoneViewModel _viewModel;

		public PhoneViewModel ViewModel
		{
			get { return _viewModel; }
			set
			{
				_viewModel = value;
				ConfigureDlg();
			}
		}
		public HBox HBoxPhoneView
		{
			get { return _hBox; }
		}

		public PhoneView(Phone phone, 
			IEntityAutocompleteSelectorFactory RoboAtsCounterpartyNameSelectorFactory, 
			IEntityAutocompleteSelectorFactory RoboAtsCounterpartyPatronymicSelectorFactory,
			EntityRepositories.IPhoneRepository phoneRepository,
			ICommonServices commonServices,
			IUnitOfWork uow,
			IPhoneTypeSettings phoneTypeSettings,
			bool readOnly)
		{
			_hBox = new HBox();
			ViewModel = new PhoneViewModel(phone, 
				RoboAtsCounterpartyNameSelectorFactory, 
				RoboAtsCounterpartyPatronymicSelectorFactory, 
				phoneRepository, 
				commonServices, 
				uow, 
				phoneTypeSettings, 
				readOnly);
		}

		private void ConfigureDlg()
		{
			AddPhoneTypeCombo();
			AddLable("+7");
			AddPhoneNumberEntry();
			AddHandset();
			AddLable("доб.");
			AddPhoneAdditionalEntry();
			AddLable("коммент.:");
			AddPhoneCommentEntry();

			if(ViewModel.RoboAtsCounterpartyNameSelectorFactory != null)
			{
				AddRoboAtsLable("имя:");
				_hBox.PackStart(GetNameEntityEntry(), true, true, 0);
			}

			if(ViewModel.RoboAtsCounterpartyPatronymicSelectorFactory != null)
			{
				AddRoboAtsLable("отч.:");
				_hBox.PackStart(GetPatronymicEntityEntry(), true, true, 0);
			}

			_hBox.Data.Add("phone", ViewModel.Phone); // Для свзяки виджета и телефона
			_hBox.ShowAll();
		}

		private void AddPhoneTypeCombo()
		{
			var phoneDataCombo = new yListComboBox
			{
				WidthRequest = 100
			};
			phoneDataCombo.SetRenderTextFunc((PhoneType x) => x.Name);
			phoneDataCombo.ItemsList = ViewModel.PhoneTypes;
			phoneDataCombo.Binding.AddBinding(ViewModel, vm => vm.SelectedPhoneType, w => w.SelectedItem).InitializeFromSource();
			phoneDataCombo.Binding.AddFuncBinding(ViewModel, e => !e.ReadOnly, w => w.Sensitive).InitializeFromSource();
			_hBox.Add(phoneDataCombo);
			_hBox.SetChildPacking(phoneDataCombo, true, true, 0, PackType.Start);
		}

		private void AddPhoneCommentEntry()
		{
			var entryComment = new yEntry
			{
				MaxLength = 150
			};
			entryComment.Binding.AddBinding(ViewModel.Phone, e => e.Comment, w => w.Text).InitializeFromSource();
			entryComment.Binding.AddFuncBinding(ViewModel, e => !e.ReadOnly, w => w.IsEditable).InitializeFromSource();
			_hBox.Add(entryComment);
			_hBox.SetChildPacking(entryComment, true, true, 0, PackType.Start);
		}

		private void AddPhoneAdditionalEntry()
		{
			var additionalDataEntry = new yEntry
			{
				WidthRequest = 40,
				MaxLength = 10
			};
			additionalDataEntry.Binding.AddBinding(ViewModel.Phone, e => e.Additional, w => w.Text).InitializeFromSource();
			additionalDataEntry.Binding.AddFuncBinding(ViewModel, e => !e.ReadOnly, w => w.IsEditable).InitializeFromSource();
			_hBox.Add(additionalDataEntry);
			_hBox.SetChildPacking(additionalDataEntry, false, false, 0, PackType.Start);
		}

		private void AddHandset()
		{
			var handset = new HandsetView(ViewModel.Phone.DigitsNumber);
			handset.Binding.AddBinding(ViewModel, vm => vm.PhoneIsArchive, w => w.Sensitive);
			_hBox.Add(handset);
			_hBox.SetChildPacking(handset, false, false, 0, PackType.Start);
		}

		private void AddPhoneNumberEntry()
		{
			var phoneDataEntry = new yValidatedEntry
			{
				ValidationMode = ValidationType.phone,
				Tag = ViewModel.Phone,
				WidthRequest = 110,
				WidthChars = 19
			};
			phoneDataEntry.Binding.AddBinding(ViewModel.Phone, e => e.Number, w => w.Text).InitializeFromSource();
			phoneDataEntry.Binding.AddFuncBinding(ViewModel, e => !e.ReadOnly, w => w.IsEditable).InitializeFromSource();
			_hBox.Add(phoneDataEntry);
			_hBox.SetChildPacking(phoneDataEntry, false, false, 0, PackType.Start);
		}

		private EntityViewModelEntry GetNameEntityEntry()
		{
			var entityviewmodelentryName = new EntityViewModelEntry
			{
				WidthRequest = 170
			};
			entityviewmodelentryName.Binding
				.AddBinding(ViewModel, vm => vm.CanEditCounterpartyName, w => w.CanEditReference)
				.AddBinding(ViewModel.Phone, e => e.RoboAtsCounterpartyName, w => w.Subject)
				.InitializeFromSource();
			entityviewmodelentryName.Binding
				.AddFuncBinding(ViewModel, vm => !vm.ReadOnly && vm.CanReadCounterpartyName, w => w.IsEditable).InitializeFromSource();
			entityviewmodelentryName.SetEntityAutocompleteSelectorFactory(ViewModel.RoboAtsCounterpartyNameSelectorFactory);

			return entityviewmodelentryName;
		}

		private EntityViewModelEntry GetPatronymicEntityEntry()
		{
			var entityviewmodelentryPatronymic = new EntityViewModelEntry
			{
				WidthRequest = 170
			};
			entityviewmodelentryPatronymic.Binding
				.AddBinding(ViewModel, vm => vm.CanEditCounterpartyPatronymic, w => w.CanEditReference)
				.AddBinding(ViewModel.Phone, e => e.RoboAtsCounterpartyPatronymic, w => w.Subject)
				.InitializeFromSource();
			entityviewmodelentryPatronymic.Binding
				.AddFuncBinding(ViewModel, vm => !vm.ReadOnly && vm.CanReadCounterpartyPatronymic, w => w.IsEditable)
				.InitializeFromSource();
			entityviewmodelentryPatronymic.SetEntityAutocompleteSelectorFactory(ViewModel.RoboAtsCounterpartyPatronymicSelectorFactory);

			return entityviewmodelentryPatronymic;
		}

		private void AddLable(string value)
		{
			var newLable = new Label(value);
			_hBox.Add(newLable);
			_hBox.SetChildPacking(newLable, false, false, 0, PackType.Start);
		}

		private void AddRoboAtsLable(string value)
		{
			var labelPatronymic = new Label(value);
			_hBox.PackStart(labelPatronymic, false, false, 0);
		}
	}
}
