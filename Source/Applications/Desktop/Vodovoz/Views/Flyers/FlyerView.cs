﻿using Gamma.ColumnConfig;
using QS.Navigation;
using QS.Views.GtkUI;
using Vodovoz.Domain;
using Vodovoz.ViewModels.ViewModels.Flyers;

namespace Vodovoz.Views.Flyers
{
	public partial class FlyerView : TabViewBase<FlyerViewModel>
	{
		public FlyerView(FlyerViewModel viewModel) : base(viewModel)
		{
			Build();
			Configure();
		}

		private void Configure()
		{
			btnSave.Clicked += (sender, args) => ViewModel.SaveAndClose();
			btnCancel.Clicked += (sender, args) => ViewModel.Close(false, CloseSource.Cancel);
			btnActivate.Clicked += (sender, args) => ViewModel.ActivateFlyerCommand.Execute();
			btnDeactivate.Clicked += (sender, args) => ViewModel.DeactivateFlyerCommand.Execute();
			
			entryFlyerNomenclature.SetEntityAutocompleteSelectorFactory(ViewModel.FlyerAutocompleteSelectorFactory);
			entryFlyerNomenclature.Binding.AddBinding(ViewModel.Entity, e => e.FlyerNomenclature, w => w.Subject).InitializeFromSource();
			entryFlyerNomenclature.Binding.AddBinding(ViewModel, vm => vm.CanEditFlyerNomenclature, w => w.Sensitive).InitializeFromSource();
			chkIsForFirstOrder.Binding.AddBinding(ViewModel.Entity, v => v.IsForFirstOrder, w => w.Active).InitializeFromSource();

			hboxActivation.Binding.AddFuncBinding(ViewModel, vm => !vm.IsFlyerActivated, w => w.Visible).InitializeFromSource();
			startDatePicker.Binding.AddBinding(ViewModel, vm => vm.FlyerStartDate, w => w.DateOrNull).InitializeFromSource();
			startDatePicker.IsEditable = true;
			btnActivate.Binding.AddBinding(ViewModel, vm => vm.CanActivateFlyer, w => w.Sensitive).InitializeFromSource();

			hboxDeactivation.Binding.AddBinding(ViewModel, vm => vm.IsFlyerActivated, w => w.Visible).InitializeFromSource();
			endDatePicker.Binding.AddBinding(ViewModel, vm => vm.FlyerEndDate, w => w.DateOrNull).InitializeFromSource();
			endDatePicker.IsEditable = true;
			btnDeactivate.Binding.AddBinding(ViewModel, vm => vm.CanDeactivateFlyer, w => w.Sensitive).InitializeFromSource();

			ConfigureTreeView();
		}

		private void ConfigureTreeView()
		{
			treeViewFlyersActionTimes.ColumnsConfig = FluentColumnsConfig<FlyerActionTime>.Create()
				.AddColumn("Дата старта").AddTextRenderer(n => n.StartDate.ToShortDateString())
				.AddColumn("Дата окончания").AddTextRenderer(n => 
					n.EndDate.HasValue 
						? n.EndDate.Value.ToShortDateString() 
						: "")
				.AddColumn("")
				.Finish();
			
			treeViewFlyersActionTimes.ItemsDataSource = ViewModel.Entity.ObservableFlyerActionTimes;
		}
	}
}
