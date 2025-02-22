﻿using QS.Views;
using Vodovoz.ViewModels.ViewModels.Settings;

namespace Vodovoz.Views.Settings
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class GeneralSettingsView : ViewBase<GeneralSettingsViewModel>
	{
		public GeneralSettingsView(GeneralSettingsViewModel viewModel) : base(viewModel)
		{
			Build();
			Configure();
		}

		private void Configure()
		{
			btnSaveRouteListPrintedPhones.Clicked += (sender, args) => ViewModel.SaveRouteListPrintedFormPhonesCommand.Execute();
			btnSaveRouteListPrintedPhones.Binding.AddBinding(ViewModel, vm => vm.CanEditRouteListPrintedFormPhones, w => w.Sensitive)
				.InitializeFromSource();

			textviewRouteListPrintedFormPhones.Binding.AddSource(ViewModel)
				.AddBinding(vm => vm.RouteListPrintedFormPhones, w => w.Buffer.Text)
				.AddBinding(vm => vm.CanEditRouteListPrintedFormPhones, w => w.Sensitive)
				.InitializeFromSource();

			btnSaveCanAddForwardersToLargus.Clicked += (sender, args) => ViewModel.SaveCanAddForwardersToLargusCommand.Execute();
			btnSaveCanAddForwardersToLargus.Binding.AddBinding(ViewModel, vm => vm.CanEditCanAddForwardersToLargus, w => w.Sensitive)
				.InitializeFromSource();

			ycheckCanAddForwardersToLargus.Binding.AddSource(ViewModel)
				.AddBinding(vm => vm.CanAddForwardersToLargus, w => w.Active)
				.AddBinding(vm => vm.CanEditCanAddForwardersToLargus, w => w.Sensitive)
				.InitializeFromSource();

			roboatssettingsview1.ViewModel = ViewModel.RoboatsSettingsViewModel;
		}
	}
}
