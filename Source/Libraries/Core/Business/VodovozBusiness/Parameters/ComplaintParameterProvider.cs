﻿using System;
using Vodovoz.Services;

namespace Vodovoz.Parameters
{
	public class ComplaintParametersProvider : IComplaintParametersProvider
	{
		private readonly IParametersProvider _parametersProvider;

		public ComplaintParametersProvider(IParametersProvider parametersProvider)
		{
			this._parametersProvider = parametersProvider ?? throw new ArgumentNullException(nameof(parametersProvider));
		}

		public int SubdivisionResponsibleId => _parametersProvider.GetIntValue("subdivision_responsible_id");

		public int EmployeeResponsibleId => _parametersProvider.GetIntValue("employee_responsible_id");
	}
}
