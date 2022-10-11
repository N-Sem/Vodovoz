﻿using System.Collections.Generic;
using Gamma.Widgets;
using Gtk;
using System.Linq;
using Gamma.GtkWidgets;
using QS.Widgets.GtkUI;
using QSWidgetLib;
using Vodovoz.Domain.Contacts;
using Vodovoz.ViewModels.ViewModels;
using Vodovoz.ViewWidgets.Mango;
using Vodovoz.ViewModels.ViewModels.Contacts;

namespace Vodovoz.Dialogs.Phones
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class PhonesView : Gtk.Bin
	{
		private PhonesViewModel viewModel;

		private IList<HBox> hBoxList;

		public PhonesViewModel ViewModel {
			get { return viewModel; }
			set {
				viewModel = value;
				ConfigureDlg();
			}
		}

		public PhonesView()
		{
			this.Build();
		}

		private void ConfigureDlg()
		{
			viewModel.PhonesListReplaced += ConfigureDlg;

			if(viewModel.PhonesList == null)
				return;

			buttonAddPhone.Clicked += (sender, e) => viewModel.AddItemCommand.Execute();
			buttonAddPhone.Binding.AddFuncBinding(viewModel, e => !e.ReadOnly, w => w.Sensitive).InitializeFromSource();

			viewModel.PhonesList.PropertyChanged += (sender, e) => Redraw();
			Redraw();
		}


		private void DrawNewRow(Phone newPhone)
		{
			if(hBoxList?.FirstOrDefault() == null) 
				hBoxList = new List<HBox>();

			HBox hBox = new HBox();

			var phoneDataCombo = new yListComboBox();
			phoneDataCombo.WidthRequest = 100;
			phoneDataCombo.SetRenderTextFunc((PhoneType x) => x.Name);
			phoneDataCombo.ItemsList = viewModel.PhoneTypes;
			phoneDataCombo.Binding.AddBinding(newPhone, e => e.PhoneType, w => w.SelectedItem).InitializeFromSource();
			phoneDataCombo.Binding.AddFuncBinding(viewModel, e => !e.ReadOnly, w => w.Sensitive).InitializeFromSource();
			hBox.Add(phoneDataCombo);
			hBox.SetChildPacking(phoneDataCombo, true, true, 0, PackType.Start);

			Label textPhoneLabel = new Label("+7");
			hBox.Add(textPhoneLabel);
			hBox.SetChildPacking(textPhoneLabel, false, false, 0, PackType.Start);

			var phoneDataEntry = new yValidatedEntry();
			phoneDataEntry.ValidationMode = ValidationType.phone;
			phoneDataEntry.Tag = newPhone;
			phoneDataEntry.WidthRequest = 110;
			phoneDataEntry.WidthChars = 19;
			phoneDataEntry.Binding.AddBinding(newPhone, e => e.Number, w => w.Text).InitializeFromSource();
			phoneDataEntry.Binding.AddFuncBinding(viewModel, e => !e.ReadOnly, w => w.IsEditable).InitializeFromSource();
			hBox.Add(phoneDataEntry);
			hBox.SetChildPacking(phoneDataEntry, false, false, 0, PackType.Start);

			HandsetView handset = new HandsetView(newPhone.DigitsNumber);
			hBox.Add(handset);
			hBox.SetChildPacking(handset, false, false, 0, PackType.Start);

			var textAdditionalLabel = new Gtk.Label("доб.");
			hBox.Add(textAdditionalLabel);
			hBox.SetChildPacking(textAdditionalLabel, false, false, 0, PackType.Start);

			var additionalDataEntry = new yEntry();
			additionalDataEntry.WidthRequest = 40;
			additionalDataEntry.MaxLength = 10;
			additionalDataEntry.Binding.AddBinding(newPhone, e => e.Additional, w => w.Text).InitializeFromSource();
			additionalDataEntry.Binding.AddFuncBinding(viewModel, e => !e.ReadOnly, w => w.IsEditable).InitializeFromSource();
			hBox.Add(additionalDataEntry);
			hBox.SetChildPacking(additionalDataEntry, false, false, 0, PackType.Start);

			var labelComment = new Label("коммент.:");
			hBox.Add(labelComment);
			hBox.SetChildPacking(labelComment, false, false, 0, PackType.Start);

			var entryComment = new yEntry();
			entryComment.MaxLength = 150;
			entryComment.Binding.AddBinding(newPhone, e => e.Comment, w => w.Text).InitializeFromSource();
			entryComment.Binding.AddFuncBinding(viewModel, e => !e.ReadOnly, w => w.IsEditable).InitializeFromSource();
			hBox.Add(entryComment);
			hBox.SetChildPacking(entryComment, true, true, 0, PackType.Start);

			if(ViewModel.RoboAtsCounterpartyNameSelectorFactory != null)
			{
				var labelName = new Label("имя:");
				hBox.PackStart(labelName, false, false, 0);

				var entityviewmodelentryName = new EntityViewModelEntry();
				entityviewmodelentryName.Binding.AddBinding(viewModel, vm => vm.CanEditCounterpartyName, w => w.CanEditReference)
					.InitializeFromSource();
				entityviewmodelentryName.Binding.AddBinding(newPhone, e => e.RoboAtsCounterpartyName, w => w.Subject)
					.InitializeFromSource();
				entityviewmodelentryName.Binding
					.AddFuncBinding(viewModel, vm => !vm.ReadOnly && vm.CanReadCounterpartyName, w => w.IsEditable).InitializeFromSource();
				entityviewmodelentryName.SetEntityAutocompleteSelectorFactory(ViewModel.RoboAtsCounterpartyNameSelectorFactory);
				entityviewmodelentryName.WidthRequest = 170;
				hBox.PackStart(entityviewmodelentryName, true, true, 0);
			}

			if(ViewModel.RoboAtsCounterpartyPatronymicSelectorFactory != null)
			{

				var labelPatronymic = new Label("отч.:");
				hBox.PackStart(labelPatronymic,false,false,0);

				var entityviewmodelentryPatronymic = new EntityViewModelEntry();
				entityviewmodelentryPatronymic.Binding
					.AddBinding(viewModel, vm => vm.CanEditCounterpartyPatronymic, w => w.CanEditReference).InitializeFromSource();
				entityviewmodelentryPatronymic.Binding.AddBinding(newPhone, e => e.RoboAtsCounterpartyPatronymic, w => w.Subject)
					.InitializeFromSource();
				entityviewmodelentryPatronymic.Binding
					.AddFuncBinding(viewModel, vm => !vm.ReadOnly && vm.CanReadCounterpartyPatronymic, w => w.IsEditable)
					.InitializeFromSource();
				entityviewmodelentryPatronymic.SetEntityAutocompleteSelectorFactory(ViewModel.RoboAtsCounterpartyPatronymicSelectorFactory);
				entityviewmodelentryPatronymic.WidthRequest = 170;
				hBox.PackStart(entityviewmodelentryPatronymic, true,true,0);
			}

			yButton deleteButton = new yButton();
			Image image = new Image();
			image.Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-delete", IconSize.Menu);
			deleteButton.Image = image;
			deleteButton.Clicked += (sender, e) => viewModel.DeleteItemCommand.Execute(hBox.Data["phone"] as Phone);
			deleteButton.Binding.AddFuncBinding(viewModel, e => !e.ReadOnly, w => w.Sensitive).InitializeFromSource();
			hBox.Add(deleteButton);
			hBox.SetChildPacking(deleteButton, false, false, 0, PackType.Start);

			hBox.Data.Add("phone", newPhone); //Для свзяки виджета и телефона
			hBox.ShowAll();

			vboxPhones.Add(hBox);
			vboxPhones.ShowAll();
			hBoxList.Add(hBox);

		}

		private void Redraw()
		{
			buttonAddPhone.Visible = !ViewModel.ReadOnly;

			foreach(var child in vboxPhones.Children.ToList())
			{
				child.Destroy();
				vboxPhones.Remove(child);
			}

			foreach(Phone phone in viewModel.PhonesList) 
			{
				DrawNewRow(phone);
			}
		}
	}
}
