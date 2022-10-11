﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Gamma.Utilities;
using QS.DomainModel.Entity;
using QS.DomainModel.Entity.EntityPermissions;
using QS.HistoryLog;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Organizations;
using Vodovoz.Domain.Permissions;

namespace Vodovoz.Domain.Cash
{
	[Appellative (Gender = GrammaticalGender.Masculine,
		NominativePlural = "авансовые отчеты",
		Nominative = "авансовый отчет")]
	[EntityPermission]
	[HistoryTrace]
	public class AdvanceReport : PropertyChangedBase, IDomainObject, IValidatableObject, ISubdivisionEntity
	{
		#region Свойства

		public virtual int Id { get; set; }

		private DateTime date;

		[Display (Name = "Дата")]
		public virtual DateTime Date {
			get { return date; }
			set { SetField (ref date, value, () => Date); }
		}

		private Subdivision relatedToSubdivision;

		[Display(Name = "Относится к подразделению")]
		public virtual Subdivision RelatedToSubdivision {
			get { return relatedToSubdivision; }
			set { SetField(ref relatedToSubdivision, value, () => RelatedToSubdivision); }
		}

		Employee casher;

		[Display (Name = "Кассир")]
		public virtual Employee Casher {
			get { return casher; }
			set { SetField (ref casher, value, () => Casher); }
		}

		Employee accountable;

		[Display (Name = "Подотчетное лицо")]
		public virtual Employee Accountable {
			get { return accountable; }
			set { SetField (ref accountable, value, () => Accountable); }
		}

		ExpenseCategory expenseCategory;

		[Display (Name = "Статья расхода")]
		public virtual ExpenseCategory ExpenseCategory {
			get { return expenseCategory; }
			set { SetField (ref expenseCategory, value, () => ExpenseCategory); }
		}

		Income changeReturn;

		[Display (Name = "Возврат сдачи")]
		public virtual Income ChangeReturn {
			get { return changeReturn; }
			set { SetField (ref changeReturn, value, () => ChangeReturn); }
		}

		string description;

		[Display (Name = "Основание")]
		public virtual string Description {
			get { return description; }
			set { SetField (ref description, value, () => Description); }
		}

		decimal money;

		[Display (Name = "Сумма")]
		public virtual decimal Money {
			get { return money; }
			set {
				SetField (ref money, value, () => Money); 
			}
		}

		Organization organisation;
		[Display(Name = "Организация")]
		public virtual Organization Organisation {
			get => organisation;
			set => SetField(ref organisation, value);
		}

		public virtual string Title => String.Format("Авансовый отчет №{0} от {1:d}", Id, Date);
		
		public virtual bool NeedValidateOrganisation { get; set; }

		#endregion

		public AdvanceReport() { }

		public virtual List<AdvanceClosing> CloseAdvances(out Expense surcharge, out Income returnChange, List<Expense> advances )
		{
			if (advances.Any (a => a.ExpenseCategory != ExpenseCategory))
				throw new InvalidOperationException ("Нельзя что бы авансовый отчет, закрывал авансы выданные по другим статьям.");

			surcharge = null; returnChange = null;

			decimal totalExpense = advances.Sum (a => a.UnclosedMoney);
			decimal balance = totalExpense - Money;
			List<AdvanceClosing> resultClosing = new List<AdvanceClosing> ();

			if(balance < 0)
			{
				surcharge = new Expense{
					Casher = Casher,
					Date = Date,
					Employee = Accountable,
					TypeOperation = ExpenseType.Advance,
					Organisation = Organisation,
					ExpenseCategory = ExpenseCategory,
					Money = Math.Abs (balance),
					Description = $"Доплата денежных средств сотруднику по авансовому отчету №{Id}",
					AdvanceClosed = true,
					RelatedToSubdivision = RelatedToSubdivision
				};
				resultClosing.Add (surcharge.AddAdvanceCloseItem(this, surcharge.Money));
			}
			else if(balance > 0)
			{
				returnChange = new Income{
					Casher = Casher,
					Date = Date,
					Employee = Accountable,
					ExpenseCategory = ExpenseCategory,
					TypeOperation = IncomeType.Return,
					Organisation = Organisation,
					Money = Math.Abs (balance),
					Description = $"Возврат в кассу денежных средств по авансовому отчету №{Id}",
					RelatedToSubdivision = RelatedToSubdivision
				};
				ChangeReturn = returnChange;
			}

			foreach(var adv in advances)
			{
				resultClosing.Add (adv.AddAdvanceCloseItem(this, adv.UnclosedMoney));
			}

			return resultClosing;
		}

		#region IValidatableObject implementation

		public virtual IEnumerable<ValidationResult> Validate (ValidationContext validationContext)
		{
			if (Accountable == null)
				yield return new ValidationResult ("Подотчетное лицо должно быть указано.",
					new[] { this.GetPropertyName (o => o.Accountable) });
			if (ExpenseCategory == null)
				yield return new ValidationResult ("Статья расхода должна быть указана.",
					new[] { this.GetPropertyName (o => o.ExpenseCategory) });

			if(Money <= 0)
				yield return new ValidationResult ("Сумма должна иметь значение отличное от 0.",
					new[] { this.GetPropertyName (o => o.Money) });

			if(String.IsNullOrWhiteSpace (Description))
				yield return new ValidationResult ("Основание должно быть заполнено.",
					new[] { this.GetPropertyName (o => o.Description) });

			if(RelatedToSubdivision == null) {
				yield return new ValidationResult("Должно быть выбрано подразделение",
					new[] { this.GetPropertyName(o => o.RelatedToSubdivision) });
			}
			
			if(Id == 0 && NeedValidateOrganisation && Organisation == null) {
				yield return new ValidationResult("Организация должна быть заполнена",
					new[] { nameof(Organisation) });
			}
		}

		#endregion
	}
}

