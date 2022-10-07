﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Gamma.Utilities;
using QS.DomainModel.Entity;
using QS.DomainModel.Entity.EntityPermissions;
using QS.DomainModel.UoW;
using QS.HistoryLog;
using Vodovoz.Domain.Cash.CashTransfer;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Organizations;
using Vodovoz.Domain.Permissions;
using Vodovoz.EntityRepositories.Cash;
using Vodovoz.Tools.CallTasks;

namespace Vodovoz.Domain.Cash
{
	[Appellative (Gender = GrammaticalGender.Masculine,
		NominativePlural = "приходные одера",
		Nominative = "приходный ордер")]
	[EntityPermission]
	[HistoryTrace]
	public class Income : PropertyChangedBase, IDomainObject, IValidatableObject, ISubdivisionEntity
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

		private IncomeInvoiceDocumentType typeDocument;

		[Display(Name = "Тип документа")]
		public virtual IncomeInvoiceDocumentType TypeDocument {
			get { return typeDocument; }
			set { SetField(ref typeDocument, value, () => TypeDocument); }
		}

		private IncomeType typeOperation;

		[Display (Name = "Тип операции")]
		public virtual IncomeType TypeOperation {
			get { return typeOperation; }
			set { 
				if(SetField (ref typeOperation, value, () => TypeOperation))
				{
					switch(TypeOperation)
					{
					case IncomeType.Return:
						IncomeCategory = null;
						Customer = null;
						break;
					case IncomeType.Common:
						ExpenseCategory = null;
						Customer = null;
						break;
					case IncomeType.Payment:
						ExpenseCategory = null;
						break;
					}
				}
			}
		}

		Employee casher;

		[Display (Name = "Кассир")]
		public virtual Employee Casher {
			get { return casher; }
			set { SetField (ref casher, value, () => Casher); }
		}

		Employee employee;

		[Display (Name = "Сотрудник")]
		public virtual Employee Employee {
			get { return employee; }
			set { SetField (ref employee, value, () => Employee); }
		}

		Counterparty customer;

		[Display (Name = "Клиент")]
		public virtual Counterparty Customer {
			get { return customer; }
			set { SetField (ref customer, value, () => Customer); }
		}

		Order order;

		[Display(Name = "Заказ")]
		public virtual Order Order {
			get { return order; }
			set { SetField(ref order, value, () => Order); }
		}

		IncomeCategory incomeCategory;

		[Display (Name = "Статья дохода")]
		public virtual IncomeCategory IncomeCategory {
			get { return incomeCategory; }
			set { SetField (ref incomeCategory, value, () => IncomeCategory); }
		}

		ExpenseCategory expenseCategory;

		/// <summary>
		/// Используется только для отслеживания возвратных возвратных денег с типом операции Return
		/// </summary>
		[Display (Name = "Статья расхода")]
		public virtual ExpenseCategory ExpenseCategory {
			get { return expenseCategory; }
			set { SetField (ref expenseCategory, value, () => ExpenseCategory); }
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

		RouteList routeListClosing;

		public virtual RouteList RouteListClosing
		{
			get => routeListClosing;
			set => SetField(ref routeListClosing, value, () => RouteListClosing);
		}

		private IncomeCashTransferedItem transferedBy;
		[Display(Name = "Перемещен")]
		public virtual IncomeCashTransferedItem TransferedBy {
			get => transferedBy;
			set => SetField(ref transferedBy, value, () => TransferedBy);
		}

		private CashTransferDocumentBase cashTransferDocument;
		[Display(Name = "Документ перемещения")]
		public virtual CashTransferDocumentBase CashTransferDocument {
			get => cashTransferDocument;
			set => SetField(ref cashTransferDocument, value, () => CashTransferDocument);
		}

		string cashierReviewComment;
		[Display(Name = "Комментарий по закрытию кассы")]
		public virtual string CashierReviewComment {
			get => cashierReviewComment;
			set => SetField(ref cashierReviewComment, value, () => CashierReviewComment);
		}

		private Organization organisation;
		[Display(Name = "Организация")]
		public virtual Organization Organisation {
			get => organisation;
			set => SetField(ref organisation, value);
		}

		#endregion

		public virtual string Title => String.Format("Приходный ордер №{0} от {1:d}", Id, Date);

		#region RunTimeOnly

		public virtual List<Expense> AdvanceForClosing { get; protected set;}

		public virtual bool NoFullCloseMode { get; set;}

		#endregion

		public Income() { }

		#region Функции

		public virtual void AcceptSelfDeliveryPaid(CallTaskWorker callTaskWorker)
		{
			if(Id == 0) {
				Order.AcceptSelfDeliveryIncomeCash(Money, callTaskWorker);
			} else {
				Order.AcceptSelfDeliveryIncomeCash(Money, callTaskWorker, Id);
			}
		}

		public virtual void PrepareCloseAdvance(List<Expense> advances)
		{
			if (TypeOperation != IncomeType.Return)
				throw new InvalidOperationException("Метод PrepareCloseAdvance() можно вызываться только для возврата аванса.");

			AdvanceForClosing = advances;
		}

		public virtual void CloseAdvances(IUnitOfWork uow)
		{
			if (TypeOperation != IncomeType.Return) {
				throw new InvalidOperationException ("Приходный ордер должен иметь тип '"+Gamma.Utilities.AttributeUtil.GetEnumTitle(IncomeType.Return)+"'");
			}

			if(NoFullCloseMode)
			{
				var advance = AdvanceForClosing.First();
				advance.AddAdvanceCloseItem(this, Money);
				uow.Save(advance);
			}
			else
			{
				foreach(var advance in AdvanceForClosing)
				{
					advance.AddAdvanceCloseItem(this, advance.Money - advance.ClosedMoney);
					uow.Save(advance);
				}
			}
		}

		public virtual void FillFromOrder(IUnitOfWork uow, ICashRepository cashRepository)
		{
			if(Id == 0) {
				var existsIncome = cashRepository.GetIncomePaidSumForOrder(uow, Order.Id);
				decimal orderCash = 0m;
				if(Order.PaymentType == PaymentType.cash) {
					orderCash = Order.OrderSum;
				}
				var result = orderCash - existsIncome;
				Money = result < 0 ? 0 : result;

				Description = $"Самовывоз №{Order.Id} от {Order.DeliveryDate}";
			}
		}

		#endregion

		#region IValidatableObject implementation

		public virtual IEnumerable<ValidationResult> Validate (ValidationContext validationContext)
		{
			if(validationContext.Items.ContainsKey("IsSelfDelivery") && (bool)validationContext.Items["IsSelfDelivery"]) {
				if(TypeDocument != IncomeInvoiceDocumentType.IncomeInvoiceSelfDelivery) {
					yield return new ValidationResult($"Тип документа должен быть { IncomeInvoiceDocumentType.IncomeInvoiceSelfDelivery.GetEnumTitle() }.",
					new[] { this.GetPropertyName(o => o.TypeDocument) });
				}
				if(TypeOperation != IncomeType.Payment) {
					yield return new ValidationResult($"Тип операции должен быть { IncomeType.Payment.GetEnumTitle() }.",
					new[] { this.GetPropertyName(o => o.TypeOperation) });
				}
				if(IncomeCategory == null || IncomeCategory.IncomeDocumentType != IncomeInvoiceDocumentType.IncomeInvoiceSelfDelivery) {
					yield return new ValidationResult("Должна быть выбрана статья дохода для самовывоза.",
					new[] { this.GetPropertyName(o => o.IncomeCategory) });
				}
				if(Order == null) {
					yield return new ValidationResult("Должен быть выбран заказ.",
					new[] { this.GetPropertyName(o => o.Order) });
				} else {
					if(Order.PaymentType != PaymentType.cash) {
						yield return new ValidationResult("Должен быть выбран наличный заказ");
					}
					if(!Order.SelfDelivery) {
						yield return new ValidationResult("Должен быть выбран заказ с самовывозом");
					}
					if(Order.OrderPositiveSum < Money) {
						yield return new ValidationResult("Сумма к оплате не может быть больше чем сумма в заказе");
					}
				}
			} else {
				if(TypeOperation == IncomeType.Return) {
					if(Employee == null)
						yield return new ValidationResult("Подотчетное лицо должно быть указано.",
							new[] { this.GetPropertyName(o => o.Employee) });

					if(ExpenseCategory == null)
						yield return new ValidationResult("Статья по которой брались деньги должна быть указана.",
							new[] { this.GetPropertyName(o => o.ExpenseCategory) });

					if(Id == 0) {
						if (Organisation == null) {
							yield return new ValidationResult("Организация должна быть заполнена",
								new[] { nameof(Organisation) });
						}
						if(AdvanceForClosing == null || AdvanceForClosing.Count == 0) {
							yield return new ValidationResult("Не указаны авансы которые должны быть закрыты этим возвратом в кассу.",
								new[] { this.GetPropertyName(o => o.AdvanceForClosing) });
						} else {
							if(NoFullCloseMode) {
								var advance = AdvanceForClosing.First();
								if(Money > advance.UnclosedMoney)
									yield return new ValidationResult("Сумма возврата не должна превышать сумму которую брал человек за вычетом уже возвращенных средств.",
										new[] { this.GetPropertyName(o => o.AdvanceForClosing) });
							} else {
								decimal closedSum = AdvanceForClosing.Sum(x => x.UnclosedMoney);
								if(closedSum != Money)
									throw new InvalidOperationException("Сумма закрытых авансов должна соответствовать сумме возврата.");
							}
						}
					}
				}
				if(TypeOperation != IncomeType.Return) {
					if(IncomeCategory == null)
						yield return new ValidationResult("Статья дохода должна быть указана.",
							new[] { this.GetPropertyName(o => o.IncomeCategory) });
				}
				if(TypeOperation == IncomeType.Payment) {
					if(Customer == null)
						yield return new ValidationResult("Клиент должен быть указан.",
							new[] { this.GetPropertyName(o => o.Customer) });
				}
			}

			if(RelatedToSubdivision == null) {
				yield return new ValidationResult("Должно быть выбрано подразделение",
					new[] { this.GetPropertyName(o => o.RelatedToSubdivision) });
			}

			if(Money <= 0)
				yield return new ValidationResult("Сумма должна больше нуля",
					new[] { this.GetPropertyName(o => o.Money) });
			if(String.IsNullOrWhiteSpace(Description))
				yield return new ValidationResult("Основание должно быть заполнено.",
					new[] { this.GetPropertyName(o => o.Description) });
		}

		#endregion
	}

	public enum IncomeType
	{
		[Display (Name = "Прочий приход")]
		Common,
		[Display (Name = "Оплата покупателя")]
		Payment,
		[Display (Name = "Приход от водителя")]
		DriverReport,
		[Display (Name = "Возврат от подотчетного лица")]
		Return,
	}

	public class IncomeTypeStringType : NHibernate.Type.EnumStringType
	{
		public IncomeTypeStringType () : base (typeof(IncomeType))
		{
		}
	}

	public enum IncomeInvoiceDocumentType
	{
		[Display(Name = "Приходный ордер")]
		IncomeInvoice,
		[Display(Name = "Приходный ордер для документа перемещения ДС")]
		IncomeTransferDocument,
		[Display(Name = "Приходный ордер для самовывоза")]
		IncomeInvoiceSelfDelivery,
	}

	public class IncomeInvoiceDocumentTypeStringType : NHibernate.Type.EnumStringType
	{
		public IncomeInvoiceDocumentTypeStringType() : base(typeof(IncomeInvoiceDocumentType))
		{
		}
	}

}

