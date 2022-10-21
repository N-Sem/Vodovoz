﻿using System;
using System.Collections.Generic;
using QS.Dialog.GtkUI;
using QS.DomainModel.UoW;
using QS.Report;
using QSReport;
using Vodovoz.Domain.Client;

namespace Vodovoz.Reports
{
	public partial class Revision : SingleUoWWidgetBase, IParametersWidget
	{
		public Revision()
		{
			this.Build();
			UoW = UnitOfWorkFactory.CreateWithoutRoot ();
			referenceCounterparty.RepresentationModel = new ViewModel.CounterpartyVM(UoW);
		}	

		#region IParametersWidget implementation

		public string Title
		{
			get
			{
				return "Акт сверки";
			}
		}

		public event EventHandler<LoadReportEventArgs> LoadReport;

		#endregion

		void OnUpdate(bool hide = false)
		{
			if (LoadReport != null)
			{
				LoadReport(this, new LoadReportEventArgs(GetReportInfo(), hide));
			}
		}

		protected void OnButtonRunClicked(object sender, EventArgs e)
		{
			OnUpdate(true);
		}

		private ReportInfo GetReportInfo()
		{			
			return new ReportInfo
			{
				Identifier = "Client.Revision",
				Parameters = new Dictionary<string, object>
				{
					{ "StartDate", dateperiodpicker1.StartDateOrNull.Value },
					{ "EndDate", dateperiodpicker1.EndDateOrNull.Value },
					{ "CounterpartyID", (referenceCounterparty.Subject as Counterparty).Id}
				}
			};
		}			

		protected void OnDateperiodpicker1PeriodChanged(object sender, EventArgs e)
		{
			ValidateParameters();
		}

		private void ValidateParameters()
		{
			var datePeriodSelected = dateperiodpicker1.EndDateOrNull != null && dateperiodpicker1.StartDateOrNull != null;
			var counterpartySelected = referenceCounterparty.Subject != null;
			buttonRun.Sensitive = datePeriodSelected && counterpartySelected;
		}

		protected void OnReferenceCounterpartyChanged (object sender, EventArgs e)
		{
			ValidateParameters();
		}
	}
}

