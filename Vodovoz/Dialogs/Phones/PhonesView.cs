using System.Collections.Generic;
using Gamma.Widgets;
using Gtk;
using System.Linq;
using Gamma.GtkWidgets;
using QS.Widgets.GtkUI;
using QSWidgetLib;
using Vodovoz.Domain.Contacts;
using Vodovoz.ViewWidgets.Mango;
using Vodovoz.ViewModels.ViewModels.Contacts;
using System;

namespace Vodovoz.Dialogs.Phones
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PhonesView : Gtk.Bin
	{
		private IList<HBox> _hBoxList;
		private PhonesViewModel _viewModel;

		public PhonesViewModel ViewModel
		{
			get { return _viewModel; }
			set
			{
				_viewModel = value;
				ConfigureDlg();
			}
		}

		public PhonesView()
		{
			this.Build();
		}

		private void ConfigureDlg()
		{
			_viewModel.PhonesListReplaced += ConfigureDlg;

			if(_viewModel.PhonesList == null)
			{
				return;
			}

			buttonAddPhone.Clicked += (sender, e) => _viewModel.AddItemCommand.Execute();
			buttonAddPhone.Binding.AddFuncBinding(_viewModel, e => !e.ReadOnly, w => w.Sensitive).InitializeFromSource();

			_viewModel.PhonesList.PropertyChanged += (sender, e) => Redraw();
			Redraw();
		}

		private void Redraw()
		{
			buttonAddPhone.Visible = !ViewModel.ReadOnly;

			foreach(var child in vboxPhones.Children.ToList())
			{
				child.Destroy();
				vboxPhones.Remove(child);
			}

			foreach(Phone phone in _viewModel.PhonesList)
			{
				DrawNewRow(phone);
			}
		}

		private void DrawNewRow(Phone newPhone)
		{
			if(_hBoxList?.FirstOrDefault() == null)
			{
				_hBoxList = new List<HBox>();
			}

			HBox hBox = GetPhoneView(newPhone);

			vboxPhones.Add(hBox);
			vboxPhones.ShowAll();
			_hBoxList.Add(hBox);

		}

		private HBox GetPhoneView(Phone newPhone)
		{
			var hBox = new HBox();
			AddPhoneTypeCombo(hBox, newPhone);
			AddLable(hBox, "+7");
			AddPhoneNumberEntry(hBox, newPhone);
			AddHandset(hBox, newPhone);
			AddLable(hBox, "доб.");
			AddPhoneAdditionalEntry(hBox, newPhone);
			AddLable(hBox, "коммент.:");
			AddPhoneCommentEntry(hBox, newPhone);

			if(ViewModel.RoboAtsCounterpartyNameSelectorFactory != null)
			{
				AddRoboAtsLable(hBox, "имя:");
				hBox.PackStart(GetNameEntityEntry(newPhone), true, true, 0);
			}

			if(ViewModel.RoboAtsCounterpartyPatronymicSelectorFactory != null)
			{
				AddRoboAtsLable(hBox, "отч.:");
				hBox.PackStart(GetPatronymicEntityEntry(newPhone), true, true, 0);
			}

			AddPhoneDeleteButton(hBox, newPhone);
			hBox.Data.Add("phone", newPhone); // Для свзяки виджета и телефона
			hBox.ShowAll();

			return hBox;
		}

		private void AddPhoneTypeCombo(HBox hBox, Phone phone)
		{
			var phoneDataCombo = new yListComboBox
			{
				WidthRequest = 100,
				ItemsList = _viewModel.PhoneTypes
			};
			phoneDataCombo.SetRenderTextFunc((PhoneType x) => x.Name);
			phoneDataCombo.Binding.AddBinding(phone, e => e.PhoneType, w => w.SelectedItem).InitializeFromSource();
			phoneDataCombo.Binding.AddFuncBinding(_viewModel, e => !e.ReadOnly, w => w.Sensitive).InitializeFromSource();
			hBox.Add(phoneDataCombo);
			hBox.SetChildPacking(phoneDataCombo, true, true, 0, PackType.Start);
		}

		private void AddPhoneCommentEntry(HBox hBox, Phone phone)
		{
			var entryComment = new yEntry
			{
				MaxLength = 150
			};
			entryComment.Binding.AddBinding(phone, e => e.Comment, w => w.Text).InitializeFromSource();
			entryComment.Binding.AddFuncBinding(_viewModel, e => !e.ReadOnly, w => w.IsEditable).InitializeFromSource();
			hBox.Add(entryComment);
			hBox.SetChildPacking(entryComment, true, true, 0, PackType.Start);
		}

		private void AddPhoneAdditionalEntry(HBox hBox, Phone phone)
		{
			var additionalDataEntry = new yEntry
			{
				WidthRequest = 40,
				MaxLength = 10
			};
			additionalDataEntry.Binding.AddBinding(phone, e => e.Additional, w => w.Text).InitializeFromSource();
			additionalDataEntry.Binding.AddFuncBinding(_viewModel, e => !e.ReadOnly, w => w.IsEditable).InitializeFromSource();
			hBox.Add(additionalDataEntry);
			hBox.SetChildPacking(additionalDataEntry, false, false, 0, PackType.Start);
		}

		private void AddHandset(HBox hBox, Phone phone)
		{
			var handset = new HandsetView(phone.DigitsNumber);
			hBox.Add(handset);
			hBox.SetChildPacking(handset, false, false, 0, PackType.Start);
		}

		private void AddPhoneNumberEntry(HBox hBox, Phone phone)
		{
			var phoneDataEntry = new yValidatedEntry
			{
				ValidationMode = ValidationType.phone,
				Tag = phone,
				WidthRequest = 110,
				WidthChars = 19
			};
			phoneDataEntry.Binding.AddBinding(phone, e => e.Number, w => w.Text).InitializeFromSource();
			phoneDataEntry.Binding.AddFuncBinding(_viewModel, e => !e.ReadOnly, w => w.IsEditable).InitializeFromSource();
			hBox.Add(phoneDataEntry);
			hBox.SetChildPacking(phoneDataEntry, false, false, 0, PackType.Start);
		}

		private EntityViewModelEntry GetNameEntityEntry(Phone phone)
		{
			var entityviewmodelentryName = new EntityViewModelEntry
			{
				WidthRequest = 170
			};
			entityviewmodelentryName.Binding
				.AddBinding(_viewModel, vm => vm.CanEditCounterpartyName, w => w.CanEditReference)
				.AddBinding(phone, e => e.RoboAtsCounterpartyName, w => w.Subject)
				.InitializeFromSource();
			entityviewmodelentryName.Binding
				.AddFuncBinding(_viewModel, vm => !vm.ReadOnly && vm.CanReadCounterpartyName, w => w.IsEditable).InitializeFromSource();
			entityviewmodelentryName.SetEntityAutocompleteSelectorFactory(ViewModel.RoboAtsCounterpartyNameSelectorFactory);

			return entityviewmodelentryName;
		}

		private EntityViewModelEntry GetPatronymicEntityEntry(Phone phone)
		{
			var entityviewmodelentryPatronymic = new EntityViewModelEntry
			{
				WidthRequest = 170
			};
			entityviewmodelentryPatronymic.Binding
				.AddBinding(_viewModel, vm => vm.CanEditCounterpartyPatronymic, w => w.CanEditReference)
				.AddBinding(phone, e => e.RoboAtsCounterpartyPatronymic, w => w.Subject)
				.InitializeFromSource();
			entityviewmodelentryPatronymic.Binding
				.AddFuncBinding(_viewModel, vm => !vm.ReadOnly && vm.CanReadCounterpartyPatronymic, w => w.IsEditable)
				.InitializeFromSource();
			entityviewmodelentryPatronymic.SetEntityAutocompleteSelectorFactory(ViewModel.RoboAtsCounterpartyPatronymicSelectorFactory);

			return entityviewmodelentryPatronymic;
		}

		private void AddPhoneDeleteButton(HBox hBox, Phone newPhone)
		{
			var deleteButton = new yButton();
			deleteButton.Image = new Image
			{
				Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-delete", IconSize.Menu)
			};
			deleteButton.Clicked += (sender, e) => _viewModel.DeleteItemCommand.Execute(hBox.Data["phone"] as Phone);
			deleteButton.Binding.AddFuncBinding(_viewModel, e => !e.ReadOnly, w => w.Sensitive).InitializeFromSource();
			hBox.Add(deleteButton);
			hBox.SetChildPacking(deleteButton, false, false, 0, PackType.Start);
		}

		private void AddLable(HBox hBox, string value)
		{
			var newLable = new Label(value);
			hBox.Add(newLable);
			hBox.SetChildPacking(newLable, false, false, 0, PackType.Start);
		}

		private void AddRoboAtsLable(HBox hBox, string value)
		{
			var labelPatronymic = new Label(value);
			hBox.PackStart(labelPatronymic, false, false, 0);
		}
	}
}
