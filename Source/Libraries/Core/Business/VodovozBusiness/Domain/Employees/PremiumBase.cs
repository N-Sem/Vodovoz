﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using Gamma.Utilities;
using QS.DomainModel.Entity;
using QS.DomainModel.UoW;
using QS.Utilities;
using Vodovoz.Domain.Operations;

namespace Vodovoz.Domain.Employees
{
	public abstract class PremiumBase : PropertyChangedBase, IDomainObject, IValidatableObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		private DateTime date = DateTime.Today;

		[Display(Name = "Дата")]
		public virtual DateTime Date
		{
			get { return date; }
			set
			{
				SetField(ref date, value);
			}
		}

		decimal totalMoney;

		[Display(Name = "Всего денег")]
		public virtual decimal TotalMoney
		{
			get { return totalMoney; }
			set { SetField(ref totalMoney, value); }
		}

		private string premiumReasonString;

		[Display(Name = "Причина премии")]
		public virtual string PremiumReasonString
		{
			get { return premiumReasonString; }
			set { SetField(ref premiumReasonString, value); }
		}

		private Employee author;

		[Display(Name = "Автор премии")]
		public virtual Employee Author
		{
			get { return author; }
			set { SetField(ref author, value); }
		}

		IList<PremiumItem> items = new List<PremiumItem>();

		[Display(Name = "Строки")]
		public virtual IList<PremiumItem> Items
		{
			get { return items; }
			set
			{
				SetField(ref items, value);
				observableItems = null;
			}
		}

		GenericObservableList<PremiumItem> observableItems;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<PremiumItem> ObservableItems
		{
			get
			{
				if (observableItems == null)
					observableItems = new GenericObservableList<PremiumItem>(Items);
				return observableItems;
			}
		}

		#endregion

		#region Расчетные

		public virtual string Title
		{
			get { return String.Format("Премия №{0} от {1:d}", Id, Date); }
		}

		public virtual string Description
		{
			get
			{
				if (Items.Count == 0)
					return CurrencyWorks.GetShortCurrencyString(TotalMoney);
				string persons;
				if (Items.Count <= 3)
					persons = String.Join(", ", Items.Select(x => x.Employee.ShortName));
				else
					persons = NumberToTextRus.FormatCase(Items.Count, "{0} сотрудник", "{0} сотрудника", "{0} сотрудников");
				return String.Format("({0}) = {1}", persons,
					CurrencyWorks.GetShortCurrencyString(TotalMoney));
			}
		}

		#endregion

		#region Методы

		public virtual void AddItem(Employee employee)
		{
			var item = new PremiumItem()
			{
				Employee = employee,
				Premium = this
			};
			ObservableItems.Add(item);
		}

		public virtual void DivideAtAll()
		{
			if (Items.Count == 0)
				return;
			var part = Math.Round(TotalMoney / Items.Count, 2);
			foreach (var item in Items)
			{
				item.Money = part;
			}
		}

		public virtual void UpdateWageOperations(IUnitOfWork uow)
		{
			foreach (var item in Items)
			{
				if (item.WageOperation == null)
				{
					item.WageOperation = new WagesMovementOperations
					{
						OperationType = WagesType.PremiumWage,
						Employee = item.Employee,
						Money = item.Money,
						OperationTime = this.Date
					};
				}
				else
				{
					item.WageOperation.OperationType = WagesType.PremiumWage;
					item.WageOperation.Employee = item.Employee;
					item.WageOperation.Money = item.Money;
				}
				uow.Save(item.WageOperation);
			}
		}

		public virtual void Fill(decimal money, string reasonString, DateTime date, params Employee[] employees)
		{
			employees.ToList().ForEach(this.AddItem);
			this.TotalMoney = money;
			this.DivideAtAll();
			this.PremiumReasonString = reasonString;
			this.Date = date;
		}

		#endregion

		#region IValidatableObject implementation

		public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (Items.Count == 0)
				yield return new ValidationResult(String.Format("Отсутствуют сотрудники которым начислена премия."),
					new[] { nameof(Items) });

			var totalSum = Items.Sum(x => x.Money);
			if (totalSum != TotalMoney)
				yield return new ValidationResult(String.Format("Общая сумма премии {0:C}, отличается от суммы премий всех сотрудников {1:C}.",
					TotalMoney, totalSum),
					new[] { nameof(Items) });

			if (string.IsNullOrWhiteSpace(PremiumReasonString))
				yield return new ValidationResult(String.Format("Отсутствует причина начисления премии."),
					new[] { nameof(Items) });
		}

		#endregion

		public enum PremiumType
		{
			[Display(Name = "Премия сотрудников")]
			Premium,
			[Display(Name = "Автопремия для раскатных газелей")]
			RaskatGAZelle
		}
	}
}
