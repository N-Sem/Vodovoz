﻿using QS.Views.GtkUI;
using Vodovoz.Domain.Goods;
using Vodovoz.FilterViewModels.Goods;
using Gamma.Utilities;
using Vodovoz.ViewModels.Journals.FilterViewModels.Goods;

namespace Vodovoz.Filters.GtkViews
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class NomenclaturesFilterView : FilterViewBase<NomenclatureFilterViewModel>
	{
		public NomenclaturesFilterView(NomenclatureFilterViewModel filterViewModel) : base(filterViewModel)
		{
			this.Build();
			Configure();
			InitializeRestrictions();
		}

		void Configure()
		{
			lstCategory.ItemsList = ViewModel.AvailableCategories;
			lstCategory.SetRenderTextFunc<NomenclatureCategory>(x => x.GetEnumTitle());
			lstCategory.Binding.AddBinding(ViewModel, s => s.RestrictCategory, w => w.SelectedItem).InitializeFromSource();

			lstSaleCategory.ItemsList = ViewModel.AvailableSalesCategories;
			lstSaleCategory.SetRenderTextFunc<SaleCategory>(x => x.GetEnumTitle());
			lstSaleCategory.Binding.AddBinding(ViewModel, s => s.RestrictSaleCategory, w => w.SelectedItem).InitializeFromSource();
			lstSaleCategory.Binding.AddBinding(ViewModel, s => s.IsSaleCategoryApplicable, w => w.Visible).InitializeFromSource();

			chkShowDilers.Binding.AddBinding(ViewModel, s => s.RestrictDilers, w => w.Active).InitializeFromSource();
			chkShowDilers.Binding.AddBinding(ViewModel, s => s.AreDilersApplicable, w => w.Visible).InitializeFromSource();

			chkOnlyDisposableTare.Binding.AddBinding(ViewModel, s => s.RestrictDisposbleTare, w => w.Active).InitializeFromSource();
			chkOnlyDisposableTare.Binding.AddBinding(ViewModel, s => s.IsDispossableTareApplicable, w => w.Visible).InitializeFromSource();

			ViewModel.RestrictArchive = false;
			chkShowArchive.Binding.AddBinding(ViewModel, vm => vm.RestrictArchive, w => w.Active).InitializeFromSource();
		}

		void InitializeRestrictions()
		{
			lstCategory.Sensitive = ViewModel.CanChangeCategory;
			lstSaleCategory.Sensitive = ViewModel.CanChangeSaleCategory;
			chkShowDilers.Sensitive = ViewModel.CanChangeShowDilers;
			chkOnlyDisposableTare.Sensitive = ViewModel.CanChangeShowDisposableTare;
			chkShowArchive.Sensitive = ViewModel.CanChangeShowArchive;
		}
	}
}
