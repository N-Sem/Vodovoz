﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vodovoz.Services
{
	public interface ICarEventSettings
	{
		int DontShowCarEventByReportId { get; }
		int CarEventStartNewPeriodDay { get;  }
		int CompensationFromInsuranceByCourtId { get; }
	}
}
