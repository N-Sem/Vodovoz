﻿using System;
using System.Collections.Generic;
using QS.Dialog;
using QS.Dialog.GtkUI;
using QS.DomainModel.UoW;
using QS.Report;
using QSReport;

namespace Vodovoz.ReportsParameters.Bottles
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class BottlesMovementSummaryReport : SingleUoWWidgetBase, IParametersWidget
	{
		public BottlesMovementSummaryReport()
		{
			this.Build();
		}

		#region IParametersWidget implementation

		public event EventHandler<LoadReportEventArgs> LoadReport;

		public string Title => "Отчёт по движению бутылей";

		#endregion

		void OnUpdate(bool hide = false)
		{
			LoadReport?.Invoke(this, new LoadReportEventArgs(GetReportInfo(), hide));
		}

		ReportInfo GetReportInfo()
		{
			return new ReportInfo {
				Identifier = "Bottles.BottlesMovementSummaryReport",
				Parameters = new Dictionary<string, object>
				{
					{ "start_date", dateperiodpicker.StartDateOrNull },
					{ "end_date", dateperiodpicker.EndDateOrNull }
				}
			};
		}

		protected void OnButtonCreateReportClicked(object sender, EventArgs e)
		{
			if(dateperiodpicker.StartDateOrNull == null) {
				MessageDialogHelper.RunErrorDialog("Необходимо выбрать период.");
				return;
			}
			OnUpdate(true);
		}
	}
}