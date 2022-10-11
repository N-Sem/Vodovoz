﻿using System.ComponentModel.DataAnnotations;

namespace Vodovoz.Domain.Complaints
{
	public enum ComplaintStatuses
	{
		[Display(Name = "В работе")]
		InProcess,
		[Display(Name = "На проверке")]
		Checking,
		[Display(Name = "Закрыт")]
		Closed
	}

	public class ComplaintStatusesStringType : NHibernate.Type.EnumStringType
	{
		public ComplaintStatusesStringType() : base(typeof(ComplaintStatuses))
		{
		}
	}
}
