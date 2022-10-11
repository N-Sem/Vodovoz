﻿using System;
using NLog;
using QS.DomainModel.UoW;
using QSOrmProject;
using QS.Tdi;
using Vodovoz.Domain.Accounting;
using Vodovoz.ViewModel;

namespace Vodovoz
{
	[System.ComponentModel.ToolboxItem (true)]
	public partial class AccountingView : QS.Dialog.Gtk.TdiTabBase, ITdiJournal
	{
		static Logger logger = LogManager.GetCurrentClassLogger ();

		public bool? UseSlider => null;

		public AccountingView ()
		{
			this.Build ();
			this.TabName = "Журнал операций по счету";
			accountingFilter.UoW = UnitOfWorkFactory.CreateWithoutRoot ();
			tableAccountingOperations.RepresentationModel = new AccountingVM (accountingFilter);
			tableAccountingOperations.RepresentationModel.UpdateNodes ();
			tableAccountingOperations.Selection.Changed += OnSelectionChanged;
			buttonDelete.Sensitive = false;
			accountingFilter.Visible = false;
		}

		void OnSelectionChanged (object sender, EventArgs e)
		{
			buttonDelete.Sensitive = (tableAccountingOperations.Selection.CountSelectedRows () > 0);
		}

		protected void OnButtonDeleteClicked (object sender, EventArgs e)
		{
			var node = (ViewModel.AccountingVMNode)tableAccountingOperations.GetSelectedNode ();
			Type operationType = node.Expense == 0 ? typeof(AccountIncome) : typeof(AccountExpense);
			if (OrmMain.DeleteObject (operationType, tableAccountingOperations.GetSelectedId ())) {
				tableAccountingOperations.RepresentationModel.UpdateNodes ();
			}
		}

		protected void OnCheckFilterToggled (object sender, EventArgs e)
		{
			accountingFilter.Visible = checkFilter.Active;
		}

		public override void Destroy()
		{
			accountingFilter.UoW?.Dispose();
			base.Destroy();
		}
	}
}