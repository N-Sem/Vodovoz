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
using Vodovoz.Parameters;

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
			// vboxPhones.ShowAll();
			_hBoxList.Add(hBox);

		}

		private HBox GetPhoneView(Phone newPhone)
		{
			var phoneView = new PhoneView(newPhone,
				ViewModel.RoboAtsCounterpartyNameSelectorFactory, 
				ViewModel.RoboAtsCounterpartyPatronymicSelectorFactory,
				ViewModel.PhoneRepository,
				ViewModel.CommonServices,
				ViewModel.UoW,
				new PhoneTypeSettings(new ParametersProvider()),
				ViewModel.ReadOnly);
			AddPhoneDeleteButton(phoneView.HBoxPhoneView, newPhone);

			return phoneView.HBoxPhoneView;
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
	}
}
