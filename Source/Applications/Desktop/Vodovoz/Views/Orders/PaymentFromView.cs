﻿using QS.Navigation;
using QS.Views.GtkUI;
using Vodovoz.ViewModels.Orders;

namespace Vodovoz.Views.Orders
{
	public partial class PaymentFromView : TabViewBase<PaymentFromViewModel>
	{
		public PaymentFromView(PaymentFromViewModel viewModel) : base(viewModel)
		{
			Build();
			ConfigureView();
		}

		private void ConfigureView()
		{
			btnSave.Clicked += (sender, e) => ViewModel.SaveAndClose();
			btnCancel.Clicked += (sender, e) => ViewModel.Close(false, CloseSource.Cancel);

			btnSave.Binding
				.AddBinding(ViewModel, vm => vm.CanEdit, w => w.Sensitive)
				.InitializeFromSource();

			entryName.Binding
				.AddBinding(ViewModel.Entity, e => e.Name, w => w.Text)
				.AddBinding(ViewModel, vm => vm.CanEdit, w => w.Sensitive)
				.InitializeFromSource();

			lblOrganizationForAvangardPayments.Visible = ViewModel.CanShowOrganization;
			entryOrganizationForAvangardPayments.SetEntityAutocompleteSelectorFactory(ViewModel.OrganizationSelectorFactory);
			entryOrganizationForAvangardPayments.Binding
				.AddSource(ViewModel)
				.AddBinding(vm => vm.CanEdit, w => w.Sensitive)
				.AddBinding(vm => vm.CanShowOrganization, w => w.Visible)
				.AddBinding(ViewModel.Entity, e => e.OrganizationForAvangardPayments, w => w.Subject)
				.InitializeFromSource();
		}
	}
}
