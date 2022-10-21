﻿using System;
using System.Collections.Generic;
using QS.DomainModel.UoW;
using QS.Dialog;
using QS.Report;
using QSReport;
using QS.Dialog.GtkUI;

namespace Vodovoz.ReportsParameters
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class OrderIncorrectPrices : SingleUoWWidgetBase, IParametersWidget
	{
		public OrderIncorrectPrices()
		{
			this.Build();
			dateperiodpicker.StartDate = DateTime.Now.Date;
			dateperiodpicker.EndDate = DateTime.Now.Date;
		}

		#region IParametersWidget implementation

		public string Title {
			get {
				return "Отчет по некорректным ценам";
			}
		}

		public event EventHandler<LoadReportEventArgs> LoadReport;

		#endregion

		private ReportInfo GetReportInfo()
		{
			string dateFrom = "";
			string dateTo = "";
			if(dateperiodpicker.Sensitive) {
				dateFrom = String.Format("{0:yyyy-MM-dd}", dateperiodpicker.StartDate);
				dateTo = String.Format("{0:yyyy-MM-dd}", dateperiodpicker.EndDate);
			}
			var parameters = new Dictionary<string, object>();
			parameters.Add("dateFrom", dateFrom);
			parameters.Add("dateTo", dateTo);

			return new ReportInfo {
				Identifier = "Orders.OrdersIncorrectPrices",
				UseUserVariables = true,
				Parameters = parameters
			};
		}

		void OnUpdate(bool hide = false)
		{
			if(LoadReport != null) {
				LoadReport(this, new LoadReportEventArgs(GetReportInfo(), hide));
			}
		}

		protected void OnButtonCreateRepotClicked(object sender, EventArgs e)
		{
			OnUpdate(true);
		}

		protected void OnCheckbutton1Toggled(object sender, EventArgs e)
		{
			dateperiodpicker.Sensitive = !checkbutton1.Active;
		}
	}
}
