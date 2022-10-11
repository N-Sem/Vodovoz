﻿using System;
using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;
using Vodovoz.Domain.Employees;

namespace Vodovoz
{
	[Appellative (Gender = GrammaticalGender.Masculine,
		NominativePlural = "графики работы сотрудников",
		Nominative = "график работы сотрудников")]
	public class EmployeeWorkChart : PropertyChangedBase, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		private Employee employee;

		[Display(Name = "Сотрудник")]
		public virtual Employee Employee
		{
			get { return employee; }
			set { SetField(ref employee, value, () => Employee); }
		}

		private DateTime date;

		[Display(Name = "Дата")]
		public virtual DateTime Date
		{
			get { return date; }
			set { SetField(ref date, value, () => Date); }
		}

		private TimeSpan startTime;

		[Display(Name = "Время начала")]
		public virtual TimeSpan StartTime
		{
			get { return startTime; }
			set { SetField(ref startTime, value, () => StartTime); }
		}

		private TimeSpan endTime;

		[Display(Name = "Время окончания")]
		public virtual TimeSpan EndTime
		{
			get { return endTime; }
			set { SetField(ref endTime, value, () => EndTime); }
		}

		public virtual int Month
		{
			get {
				return Date.Month;
			}
		}

		public virtual int Day
		{
			get {
				return Date.Day;
			}
		}
		#endregion

		public EmployeeWorkChart()
		{
		}
	}
}

