﻿using System;
using Gamma.ColumnConfig;
using Gtk;
using QS.Project.Journal.EntitySelector;
using QS.Views.GtkUI;
using QSProjectsLib;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Documents;
using Vodovoz.Filters.ViewModels;
using Vodovoz.JournalViewModels;
using Vodovoz.ViewModels.Warehouses;

namespace Vodovoz.Views.Warehouse
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class IncomingInvoiceView : TabViewBase<IncomingInvoiceViewModel>
	{
		public IncomingInvoiceView(IncomingInvoiceViewModel viewModel) : base(viewModel)
		{
			this.Build();
			ConfigureView();
		}

		private void ConfigureView()
		{
			#region Bindings
			ylabelSum.Binding.AddBinding(ViewModel, vm => vm.TotalSum, w => w.LabelProp).InitializeFromSource();
			
			ybtnAdd.Clicked += (sender, args) => ViewModel.AddItemCommand.Execute();
			buttonDelete.Clicked += (sender, e) => ViewModel.DeleteItemCommand.Execute(GetSelectedItem());
			ViewModel.DeleteItemCommand.CanExecuteChanged += (sender, e) => buttonDelete.Sensitive = ViewModel.DeleteItemCommand.CanExecute(GetSelectedItem());

			ybtnAddFromOrders.Clicked += (sender, e) => ViewModel.FillFromOrdersCommand.Execute();
			ViewModel.FillFromOrdersCommand.CanExecuteChanged += (sender, e) => ybtnAddFromOrders.Sensitive = ViewModel.FillFromOrdersCommand.CanExecute();
			ybtnAddFromOrders.Sensitive = ViewModel.FillFromOrdersCommand.CanExecute();
			
			treeItemsList.Selection.Changed += (sender, args) => { buttonDelete.Sensitive = treeItemsList.Selection.CountSelectedRows() > 0; };
			
			labelTimeStamp.Binding.AddBinding(ViewModel.Entity, e => e.DateString, w => w.LabelProp).InitializeFromSource();
			entryInvoiceNumber.Binding.AddBinding(ViewModel.Entity, e => e.InvoiceNumber, w => w.Text).InitializeFromSource();
			entryWaybillNumber.Binding.AddBinding(ViewModel.Entity, e => e.WaybillNumber, w => w.Text).InitializeFromSource();
			
			lstWarehouse.SetRenderTextFunc<Domain.Store.Warehouse>(w => w.Name);
			lstWarehouse.Binding.AddBinding(ViewModel, vm => vm.Warehouses, w => w.ItemsList).InitializeFromSource();
			lstWarehouse.Binding.AddBinding(ViewModel.Entity, e => e.Warehouse, w => w.SelectedItem).InitializeFromSource();
			
			entityVMEntryClient.SetEntityAutocompleteSelectorFactory(
				new DefaultEntityAutocompleteSelectorFactory<Counterparty, CounterpartyJournalViewModel, CounterpartyJournalFilterViewModel>(QS.Project.Services.ServicesConfig.CommonServices)
			);
			entityVMEntryClient.Binding.AddBinding(ViewModel.Entity, s => s.Contractor, w => w.Subject).InitializeFromSource();
			entityVMEntryClient.CanEditReference = !ViewModel.UserHasOnlyAccessToWarehouseAndComplaints;
			
			ytextviewComment.Binding.AddBinding(ViewModel.Entity, e => e.Comment, w => w.Buffer.Text).InitializeFromSource();
			
			btnPrint.Clicked += (sender, e) => ViewModel.PrintCommand.Execute();
			
			buttonSave.Clicked += (sender, e) => { ViewModel.SaveAndClose(); };
			buttonSave.Binding.AddBinding(ViewModel, vm => vm.CanCreateOrUpdate, w => w.Sensitive);
			
			buttonCancel.Clicked += (sender, e) => ViewModel.Close(true, QS.Navigation.CloseSource.Cancel);
			
			#endregion
			
			#region Таблица
			treeItemsList.ColumnsConfig =  FluentColumnsConfig<IncomingInvoiceItem>.Create ()
                .AddColumn ("№ п/п")
                .AddTextRenderer(i => (i.Document.Items.IndexOf(i)+ 1).ToString())
				.AddColumn ("Наименование").AddTextRenderer (i => i.Name)
				.AddColumn ("С/Н оборудования").AddTextRenderer (i => i.EquipmentString)
				.AddColumn ("% НДС").AddEnumRenderer (i => i.VAT).Editing ()
				.AddColumn ("Количество")
				.AddNumericRenderer (i => i.Amount).Editing ().WidthChars (10)
				.AddSetter((c, i) => c.Digits = (i.Nomenclature.Unit == null ? 1 :(uint)i.Nomenclature.Unit.Digits)) 
				.AddSetter ((c, i) => c.Editable = i.CanEditAmount)
				.Adjustment (new Adjustment (0, 0, 1000000, 1, 100, 0))
				.AddTextRenderer (i => i.Nomenclature.Unit == null ? String.Empty: i.Nomenclature.Unit.Name, false)
				.AddColumn("Цена закупки").AddNumericRenderer(i => i.PrimeCost).Digits(2).Editing()
				.Adjustment (new Adjustment (0, 0, 1000000, 1, 100, 0))
				.AddTextRenderer (i => CurrencyWorks.CurrencyShortName, false)
				.AddColumn ("Сумма").AddTextRenderer (i => CurrencyWorks.GetShortCurrencyString (i.Sum))
				.Finish ();
			
			treeItemsList.ItemsDataSource = ViewModel.Entity.ObservableItems;
			#endregion
			
		}
		
		private IncomingInvoiceItem GetSelectedItem()
		{
			return treeItemsList.GetSelectedObject() as IncomingInvoiceItem;
		}
	}
}
