﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gamma.ColumnConfig;
using Gamma.Utilities;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using QS.DomainModel.Entity;
using QS.DomainModel.UoW;
using QS.RepresentationModel.GtkUI;
using QS.Utilities.Text;
using QSProjectsLib;
using Vodovoz.Core.Journal;
using Vodovoz.Dialogs.Cash;
using Vodovoz.Domain.Cash;
using Vodovoz.Domain.Employees;
using Vodovoz.EntityRepositories.Cash;
using Vodovoz.SidePanel;
using Vodovoz.SidePanel.InfoProviders;
using Vodovoz.ViewModels.Infrastructure.InfoProviders;

namespace Vodovoz.Representations
{
	public class CashMultipleDocumentVM : MultipleEntityModelBase<CashDocumentVMNode>, ICashInfoProvider
	{
		private readonly ICashRepository _cashRepository;
		private CashDocumentVMNode resultAlias = null;

		public CashDocumentsFilter Filter {
			get => RepresentationFilter as CashDocumentsFilter;
			set => RepresentationFilter = value as IRepresentationFilter;
		}

		public CashMultipleDocumentVM(
			CashDocumentsFilter filter,
			ICashRepository cashRepository)
		{
			_cashRepository = cashRepository ?? throw new ArgumentNullException(nameof(cashRepository));
			UoW = UnitOfWorkFactory.CreateWithoutRoot();
			Filter = filter;
			Filter.UoW = UoW;
			JournalFilter = Filter;

			RegisterIncome();
			RegisterExpense();
			RegisterAdvanceReport();

			UpdateOnChanges(
				typeof(Income),
				typeof(Expense),
				typeof(AdvanceReport)
			);

			AfterSourceFillFunction = OrderFunc;

			Filter.InitSubdivisionsAccess(new Type[] { typeof(Income), typeof(Expense), typeof(AdvanceReport) });

			TreeViewConfig = FluentColumnsConfig<CashDocumentVMNode>.Create()
				.AddColumn("№ РКО/ПКО").AddTextRenderer(node => node.DocumentId.ToString())
				.AddColumn("Тип документа").AddTextRenderer(node => node.DisplayName)
				.AddColumn("Дата").AddTextRenderer(node => node.DateString)
				.AddColumn("Сотрудник").AddTextRenderer(node => node.EmployeeString)
				.AddColumn("Статья").AddTextRenderer(node => node.Category)
				.AddColumn("Сумма").AddTextRenderer(node => CurrencyWorks.GetShortCurrencyString(node.Money))
				.AddColumn("Кассир").AddTextRenderer(node => node.CasherString)
				.AddColumn("Основание").AddTextRenderer(node => node.Description)
			.Finish();
		}

		#region IInfoProvider

		public event EventHandler<CurrentObjectChangedArgs> CurrentObjectChanged;
		public PanelViewType[] InfoWidgets => new[] {PanelViewType.CashInfoPanelView};
		public CashDocumentsFilter CashFilter => Filter;

		#endregion

		List<CashDocumentVMNode> OrderFunc(List<CashDocumentVMNode> arg) => arg.OrderByDescending(x => x.Date).ToList();

		private string GetTotalSumInfo()
		{
			var total = ItemsList.Cast<CashDocumentVMNode>().Sum(node => node.MoneySigned);
			return CurrencyWorks.GetShortCurrencyString(total);
		}

		public override string GetSummaryInfo()
		{
			CurrentObjectChanged?.Invoke(this, new CurrentObjectChangedArgs(CashFilter));
			return $"Сумма выбранных документов: {GetTotalSumInfo()}. {base.GetSummaryInfo()}";
		}

		private void RegisterIncome()
		{
			var incomeConfig = RegisterEntity<Income>();
			//функция получения данных
			incomeConfig.AddDataFunction(() => {
				IList<CashDocumentVMNode> incomeResultList = new List<CashDocumentVMNode>();

				Income incomeAlias = null;
				Employee employeeAlias = null;
				Employee casherAlias = null;
				IncomeCategory incomeCategoryAlias = null;
				ExpenseCategory expenseCategoryAlias = null;

				var incomeDocTypes = new CashDocumentType[] { CashDocumentType.Income, CashDocumentType.IncomeSelfDelivery };

				if(Filter.RestrictExpenseCategory == null && (Filter.RestrictDocumentType == null || incomeDocTypes.Contains(Filter.RestrictDocumentType.Value))) {
					var income = UoW.Session.QueryOver<Income>(() => incomeAlias);
					income = Filter.FilterBySubdivisionsAccess<Income>(income);

					if(Filter.RestrictDocumentType != null) {
						IncomeInvoiceDocumentType documentType = IncomeInvoiceDocumentType.IncomeInvoice;
						if(Filter.RestrictDocumentType.Value == CashDocumentType.IncomeSelfDelivery) {
							documentType = IncomeInvoiceDocumentType.IncomeInvoiceSelfDelivery;
						}
						income.Where(i => i.TypeDocument == documentType);
					}
					if(Filter.RestrictExpenseCategory != null)
						income.Where(i => i.ExpenseCategory == Filter.RestrictExpenseCategory);
					if(Filter.RestrictIncomeCategory != null)
						income.Where(i => i.IncomeCategory == Filter.RestrictIncomeCategory);
					if(Filter.RestrictStartDate.HasValue)
						income.Where(o => o.Date >= Filter.RestrictStartDate.Value);
					if(Filter.RestrictEndDate.HasValue)
						income.Where(o => o.Date < Filter.RestrictEndDate.Value.AddDays(1));
					if(Filter.RestrictEmployee != null)
						income.Where(o => o.Employee == Filter.RestrictEmployee);

					incomeResultList = income
						.JoinQueryOver(() => incomeAlias.Employee, () => employeeAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
						.JoinQueryOver(() => incomeAlias.Casher, () => casherAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
						.JoinQueryOver(() => incomeAlias.IncomeCategory, () => incomeCategoryAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
						.JoinQueryOver(() => incomeAlias.ExpenseCategory, () => expenseCategoryAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
						.SelectList(list => list
							.Select(() => incomeAlias.Id).WithAlias(() => resultAlias.DocumentId)
							.Select(() => incomeAlias.Date).WithAlias(() => resultAlias.Date)
							.Select(() => incomeAlias.Money).WithAlias(() => resultAlias.Money)
							.Select(() => incomeAlias.Description).WithAlias(() => resultAlias.Description)
							.Select(() => employeeAlias.Name).WithAlias(() => resultAlias.EmployeeName)
							.Select(() => employeeAlias.LastName).WithAlias(() => resultAlias.EmployeeSurname)
							.Select(() => employeeAlias.Patronymic).WithAlias(() => resultAlias.EmployeePatronymic)
							.Select(() => casherAlias.Name).WithAlias(() => resultAlias.CasherName)
							.Select(() => casherAlias.LastName).WithAlias(() => resultAlias.CasherSurname)
							.Select(() => casherAlias.Patronymic).WithAlias(() => resultAlias.CasherPatronymic)
							.Select(Projections.SqlFunction("COALESCE", NHibernateUtil.String
							   , Projections.Property(() => incomeCategoryAlias.Name)
							   , Projections.Property(() => expenseCategoryAlias.Name)
							)).WithAlias(() => resultAlias.Category)
							.Select(() => incomeAlias.TypeDocument).WithAlias(() => resultAlias.IncomeDocumentType)
						)
						.TransformUsing(Transformers.AliasToBean<CashDocumentVMNode<Income>>())
						.List<CashDocumentVMNode>();

					foreach(var item in incomeResultList) {
						if(item.IncomeDocumentType == IncomeInvoiceDocumentType.IncomeInvoiceSelfDelivery) {
							item.DocTypeEnum = CashDocumentType.IncomeSelfDelivery;
						} else {
							item.DocTypeEnum = CashDocumentType.Income;
						}
					}

					return incomeResultList;
				}

				return new List<CashDocumentVMNode>();
			});

			incomeConfig.AddDocumentConfiguration<CashIncomeDlg>(
				//функция идентификации документа 
				(CashDocumentVMNode node) => {
					return node.DocTypeEnum == CashDocumentType.Income
					&& node.IncomeDocumentType == IncomeInvoiceDocumentType.IncomeInvoice;
				},
				//заголовок действия для создания нового документа
				CashDocumentType.Income.GetEnumTitle(),
				//функция диалога создания документа
				() => new CashIncomeDlg(),
				//функция диалога открытия документа
				(CashDocumentVMNode node) => new CashIncomeDlg(node.DocumentId)
			);

			incomeConfig.AddDocumentConfigurationWithoutCreation<TransferIncomeDlg>(
				//функция идентификации документа 
				(CashDocumentVMNode node) => {
					return node.DocTypeEnum == CashDocumentType.Income
					&& node.IncomeDocumentType == IncomeInvoiceDocumentType.IncomeTransferDocument;
				},
				//функция диалога открытия документа
				(CashDocumentVMNode node) => new TransferIncomeDlg(node.DocumentId)
			);

			incomeConfig.AddDocumentConfiguration<CashIncomeSelfDeliveryDlg>(
				(CashDocumentVMNode node) => {
					return node.DocTypeEnum == CashDocumentType.IncomeSelfDelivery && node.IncomeDocumentType == IncomeInvoiceDocumentType.IncomeInvoiceSelfDelivery;
				},
				CashDocumentType.IncomeSelfDelivery.GetEnumTitle(),
				() => new CashIncomeSelfDeliveryDlg(),
				(CashDocumentVMNode node) => new CashIncomeSelfDeliveryDlg(node.DocumentId)
			);

			//завершение конфигурации
			incomeConfig.FinishConfiguration();
		}

		private void RegisterExpense()
		{
			var expenseConfig = RegisterEntity<Expense>();
			//функция получения данных
			expenseConfig.AddDataFunction(() => {
				IList<CashDocumentVMNode> expenseResultList = new List<CashDocumentVMNode>();

				Expense expenseAlias = null;
				Employee employeeAlias = null;
				Employee casherAlias = null;
				ExpenseCategory expenseCategoryAlias = null;

				var expenseDocTypes = new CashDocumentType[] { CashDocumentType.Expense, CashDocumentType.ExpenseSelfDelivery };
				if(Filter.RestrictIncomeCategory == null && (Filter.RestrictDocumentType == null || expenseDocTypes.Contains(Filter.RestrictDocumentType.Value))) {

					var expense = UoW.Session.QueryOver<Expense>(() => expenseAlias);
					expense = Filter.FilterBySubdivisionsAccess<Expense>(expense);

					if(Filter.RestrictDocumentType != null) {
						ExpenseInvoiceDocumentType documentType = ExpenseInvoiceDocumentType.ExpenseInvoice;
						if(Filter.RestrictDocumentType.Value == CashDocumentType.ExpenseSelfDelivery) {
							documentType = ExpenseInvoiceDocumentType.ExpenseInvoiceSelfDelivery;
						}
						expense.Where(i => i.TypeDocument == documentType);
					}

					if(Filter.RestrictExpenseCategory != null)
						expense.Where(i => i.ExpenseCategory == Filter.RestrictExpenseCategory);
					if(Filter.RestrictStartDate.HasValue)
						expense.Where(o => o.Date >= Filter.RestrictStartDate.Value);
					if(Filter.RestrictEndDate.HasValue)
						expense.Where(o => o.Date < Filter.RestrictEndDate.Value.AddDays(1));
					if(Filter.RestrictEmployee != null)
						expense.Where(o => o.Employee == Filter.RestrictEmployee);

					expenseResultList = expense
						.JoinQueryOver(() => expenseAlias.Employee, () => employeeAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
						.JoinQueryOver(() => expenseAlias.Casher, () => casherAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
						.JoinQueryOver(() => expenseAlias.ExpenseCategory, () => expenseCategoryAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
						.SelectList(list => list
						   .Select(() => expenseAlias.Id).WithAlias(() => resultAlias.DocumentId)
						   .Select(() => expenseAlias.Date).WithAlias(() => resultAlias.Date)
						   .Select(() => expenseAlias.Money).WithAlias(() => resultAlias.Money)
						   .Select(() => expenseAlias.Description).WithAlias(() => resultAlias.Description)
						   .Select(() => employeeAlias.Name).WithAlias(() => resultAlias.EmployeeName)
						   .Select(() => employeeAlias.LastName).WithAlias(() => resultAlias.EmployeeSurname)
						   .Select(() => employeeAlias.Patronymic).WithAlias(() => resultAlias.EmployeePatronymic)
						   .Select(() => casherAlias.Name).WithAlias(() => resultAlias.CasherName)
						   .Select(() => casherAlias.LastName).WithAlias(() => resultAlias.CasherSurname)
						   .Select(() => casherAlias.Patronymic).WithAlias(() => resultAlias.CasherPatronymic)
						   .Select(() => expenseCategoryAlias.Name).WithAlias(() => resultAlias.Category)
						   .Select(() => expenseAlias.TypeDocument).WithAlias(() => resultAlias.ExpenseDocumentType)
						)
						.TransformUsing(Transformers.AliasToBean<CashDocumentVMNode<Expense>>())
						.List<CashDocumentVMNode>();

					foreach(var item in expenseResultList) {
						if(item.ExpenseDocumentType == ExpenseInvoiceDocumentType.ExpenseInvoiceSelfDelivery) {
							item.DocTypeEnum = CashDocumentType.ExpenseSelfDelivery;
						} else {
							item.DocTypeEnum = CashDocumentType.Expense;
						}
					}
				}

				return expenseResultList;
			});

			expenseConfig.AddDocumentConfiguration<CashExpenseDlg>(
				//функция идентификации документа 
				(CashDocumentVMNode node) => {
					return node.DocTypeEnum == CashDocumentType.Expense
					&& node.ExpenseDocumentType == ExpenseInvoiceDocumentType.ExpenseInvoice;
				},
				//заголовок действия для создания нового документа
				CashDocumentType.Expense.GetEnumTitle(),
				//функция диалога создания документа
				() => new CashExpenseDlg(),
				//функция диалога открытия документа
				(CashDocumentVMNode node) => new CashExpenseDlg(node.DocumentId)
			);

			expenseConfig.AddDocumentConfigurationWithoutCreation<TransferExpenseDlg>(
				//функция идентификации документа 
				(CashDocumentVMNode node) => {
					return node.DocTypeEnum == CashDocumentType.Expense
					&& node.ExpenseDocumentType == ExpenseInvoiceDocumentType.ExpenseTransferDocument;
				},
				//функция диалога открытия документа
				(CashDocumentVMNode node) => new TransferExpenseDlg(node.DocumentId)
			);

			expenseConfig.AddDocumentConfiguration<CashExpenseSelfDeliveryDlg>(
				(CashDocumentVMNode node) => {
					return node.DocTypeEnum == CashDocumentType.ExpenseSelfDelivery && node.ExpenseDocumentType == ExpenseInvoiceDocumentType.ExpenseInvoiceSelfDelivery;
				},
				CashDocumentType.ExpenseSelfDelivery.GetEnumTitle(),
				() => new CashExpenseSelfDeliveryDlg(),
				(CashDocumentVMNode node) => new CashExpenseSelfDeliveryDlg(node.DocumentId)
			);

			//завершение конфигурации
			expenseConfig.FinishConfiguration();
		}

		private void RegisterAdvanceReport()
		{
			var advanceReportConfig = RegisterEntity<AdvanceReport>();
			//функция получения данных
			advanceReportConfig.AddDataFunction(() => {
				IList<CashDocumentVMNode> advanceReportResultList = new List<CashDocumentVMNode>();

				AdvanceReport advanceReportAlias = null;
				Employee employeeAlias = null;
				Employee casherAlias = null;
				ExpenseCategory expenseCategoryAlias = null;

				if(Filter.RestrictIncomeCategory == null && Filter.RestrictExpenseCategory == null && (Filter.RestrictDocumentType == null || Filter.RestrictDocumentType == CashDocumentType.AdvanceReport)) {
					var advanceReport = UoW.Session.QueryOver<AdvanceReport>(() => advanceReportAlias);
					advanceReport = Filter.FilterBySubdivisionsAccess<AdvanceReport>(advanceReport);

					if(Filter.RestrictExpenseCategory != null)
						advanceReport.Where(i => i.ExpenseCategory == Filter.RestrictExpenseCategory);
					if(Filter.RestrictStartDate.HasValue)
						advanceReport.Where(o => o.Date >= Filter.RestrictStartDate.Value);
					if(Filter.RestrictEndDate.HasValue)
						advanceReport.Where(o => o.Date < Filter.RestrictEndDate.Value.AddDays(1));
					if(Filter.RestrictEmployee != null)
						advanceReport.Where(o => o.Accountable == Filter.RestrictEmployee);

					advanceReportResultList = advanceReport
						.JoinQueryOver(() => advanceReportAlias.Accountable, () => employeeAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
						.JoinQueryOver(() => advanceReportAlias.Casher, () => casherAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
						.JoinQueryOver(() => advanceReportAlias.ExpenseCategory, () => expenseCategoryAlias, NHibernate.SqlCommand.JoinType.LeftOuterJoin)
						.SelectList(list => list
						   .Select(() => advanceReportAlias.Id).WithAlias(() => resultAlias.DocumentId)
						   .Select(() => advanceReportAlias.Date).WithAlias(() => resultAlias.Date)
						   .Select(() => advanceReportAlias.Money).WithAlias(() => resultAlias.Money)
						   .Select(() => advanceReportAlias.Description).WithAlias(() => resultAlias.Description)
						   .Select(() => employeeAlias.Name).WithAlias(() => resultAlias.EmployeeName)
						   .Select(() => employeeAlias.LastName).WithAlias(() => resultAlias.EmployeeSurname)
						   .Select(() => employeeAlias.Patronymic).WithAlias(() => resultAlias.EmployeePatronymic)
						   .Select(() => casherAlias.Name).WithAlias(() => resultAlias.CasherName)
						   .Select(() => casherAlias.LastName).WithAlias(() => resultAlias.CasherSurname)
						   .Select(() => casherAlias.Patronymic).WithAlias(() => resultAlias.CasherPatronymic)
						   .Select(() => expenseCategoryAlias.Name).WithAlias(() => resultAlias.Category)
						)
						.TransformUsing(Transformers.AliasToBean<CashDocumentVMNode<AdvanceReport>>())
						.List<CashDocumentVMNode>();

					foreach(var item in advanceReportResultList) {
						item.DocTypeEnum = CashDocumentType.AdvanceReport;
					}
				}

				return advanceReportResultList;
			});

			advanceReportConfig.AddDocumentConfiguration<AdvanceReportDlg>(
				//функция идентификации документа 
				(CashDocumentVMNode node) => {
					return node.DocTypeEnum == CashDocumentType.AdvanceReport;
				},
				//заголовок действия для создания нового документа
				CashDocumentType.AdvanceReport.GetEnumTitle(),
				//функция диалога создания документа
				() => new AdvanceReportDlg(),
				//функция диалога открытия документа
				(CashDocumentVMNode node) => new AdvanceReportDlg(node.DocumentId)
			);

			//завершение конфигурации
			advanceReportConfig.FinishConfiguration();
		}

		public override IEnumerable<IJournalPopupItem> PopupItems
		{
			get
			{
				var result = new List<IJournalPopupItem>();
				result.Add(JournalPopupItemFactory.CreateNewAlwaysVisible("Повторить РКО",
					(selectedItems) => {
						var selectedNodes = selectedItems.Cast<CashDocumentVMNode>();
						var selectedNode = selectedNodes.FirstOrDefault();
						
						if(selectedNode != null)
						{
							var dlg = new CashExpenseDlg();
							var doc = UoW.GetById<Expense>(selectedNode.DocumentId);
							dlg.CopyExpenseFrom(doc);
							
							MainClass.MainWin.TdiMain.OpenTab(
								() => dlg
							);
						}
					},
					(selectedItems) => selectedItems.Any(x => (x as CashDocumentVMNode).DocTypeEnum == CashDocumentType.Expense &&
					                   (x as CashDocumentVMNode).ExpenseDocumentType == ExpenseInvoiceDocumentType.ExpenseInvoice)
				));
				
				return result;
			}
		}
	}

	public class CashDocumentVMNode<TEntity> : CashDocumentVMNode
		where TEntity : class, IDomainObject
	{
		public CashDocumentVMNode() => EntityType = typeof(TEntity);
	}

	public class CashDocumentVMNode : MultipleEntityVMNodeBase
	{
		#region MultipleDocumentJournalVMNodeBase implementation

		public override Type EntityType { get; set; }

		[UseForSearch]
		[SearchHighlight]
		public override int DocumentId { get; set; }

		[UseForSearch]
		public override string DisplayName => DocTypeEnum.GetEnumTitle();

		#endregion

		public CashDocumentType DocTypeEnum { get; set; }

		public DateTime Date { get; set; }

		[UseForSearch]
		public string DateString => Date.ToShortDateString();

		public string EmployeeSurname { get; set; }
		public string EmployeeName { get; set; }
		public string EmployeePatronymic { get; set; }

		[UseForSearch]
		public string EmployeeString => PersonHelper.PersonNameWithInitials(EmployeeSurname, EmployeeName, EmployeePatronymic);

		public string CasherSurname { get; set; }
		public string CasherName { get; set; }
		public string CasherPatronymic { get; set; }

		public string CasherString => PersonHelper.PersonNameWithInitials(CasherSurname, CasherName, CasherPatronymic);

		public IncomeInvoiceDocumentType IncomeDocumentType { get; set; }
		public ExpenseInvoiceDocumentType ExpenseDocumentType { get; set; }

		[UseForSearch]
		public string Category { get; set; }

		[UseForSearch]
		public string Description { get; set; }
		[UseForSearch]
		public decimal Money { get; set; }
		[UseForSearch]
		public decimal MoneySigned {
			get {
				switch(DocTypeEnum) {
					case CashDocumentType.Expense:
					case CashDocumentType.ExpenseSelfDelivery:
						return -Money;
					case CashDocumentType.AdvanceReport:
						return 0;
					default:
						return Money;
				}
			}
		}
	}
}
