﻿using System;
using System.Collections.Generic;
using System.Linq;
using Gamma.GtkWidgets;
using Gtk;
using NLog;
using QS.Dialog;
using QS.Dialog.GtkUI;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Project.Services;
using QS.Validation;
using Vodovoz.DocTemplates;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Organizations;
using Vodovoz.EntityRepositories.Counterparties;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModel;

namespace Vodovoz.Dialogs.Employees
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class M2ProxyDlg : QS.Dialog.Gtk.EntityDialogBase<M2ProxyDocument>, IEditableDialog
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

		private readonly IDocTemplateRepository _docTemplateRepository = new DocTemplateRepository();

		private List<OrderEquipment> equipmentList;
		public bool IsEditable { get; set; } = true;

		public M2ProxyDlg()
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<M2ProxyDocument>();
			TabName = "Новая доверенность М-2";
			ConfigureDlg();
		}

		public M2ProxyDlg(M2ProxyDocument sub) : this(sub.Id) { }

		public M2ProxyDlg(int id)
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<M2ProxyDocument>(id);
			TabName = "Изменение доверенности М-2";
			ConfigureDlg();
		}

		public M2ProxyDlg(Order order)
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<M2ProxyDocument>();
			TabName = "Новая доверенность М-2";
			Entity.Order = order;

			ConfigureDlg();
		}

		void ConfigureDlg()
		{
			if(Entity.EmployeeDocument == null && Entity.Employee != null)
				GetDocument();

			ylabelNumber.Binding.AddBinding(Entity, x => x.Title, x => x.LabelProp).InitializeFromSource();
			var orderFactory = new OrderSelectorFactory();
			evmeOrder.SetEntityAutocompleteSelectorFactory(orderFactory.CreateOrderAutocompleteSelectorFactory());
			evmeOrder.Binding.AddBinding(Entity, x => x.Order, x => x.Subject).InitializeFromSource();
			evmeOrder.Changed += (sender, e) => {
				FillForOrder();
			};
			evmeOrder.CanEditReference = ServicesConfig.CommonServices.CurrentPermissionService.ValidatePresetPermission("can_delete");

			var orgFactory = new OrganizationJournalFactory();
			evmeOrganization.SetEntityAutocompleteSelectorFactory(orgFactory.CreateOrganizationAutocompleteSelectorFactory());
			evmeOrganization.Binding.AddBinding(Entity, x => x.Organization, x => x.Subject).InitializeFromSource();
			evmeOrganization.Changed += (sender, e) => UpdateStates();

			FillForOrder();

			yDPDatesRange.Binding.AddBinding(Entity, x => x.Date, x => x.StartDate).InitializeFromSource();
			yDPDatesRange.Binding.AddBinding(Entity, x => x.ExpirationDate, x => x.EndDate).InitializeFromSource();

			var employeeFactory = new EmployeeJournalFactory();
			evmeEmployee.SetEntityAutocompleteSelectorFactory(employeeFactory.CreateWorkingEmployeeAutocompleteSelectorFactory());
			evmeEmployee.Binding.AddBinding(Entity, x => x.Employee, x => x.Subject).InitializeFromSource();

			var supplierFactory = new CounterpartyJournalFactory();
			evmeSupplier.SetEntityAutocompleteSelectorFactory(supplierFactory.CreateCounterpartyAutocompleteSelectorFactory());
			evmeSupplier.Binding.AddBinding(Entity, x => x.Supplier, x => x.Subject).InitializeFromSource();

			yETicketNr.Binding.AddBinding(Entity, x => x.TicketNumber, w => w.Text).InitializeFromSource();

			yDTicketDate.Binding.AddBinding(Entity, x => x.TicketDate, x => x.DateOrNull).InitializeFromSource();

			RefreshParserRootObject();

			templatewidget.CanRevertCommon = ServicesConfig.CommonServices.CurrentPermissionService.ValidatePresetPermission("can_set_common_additionalagreement");
			templatewidget.Binding.AddBinding(Entity, e => e.DocumentTemplate, w => w.Template).InitializeFromSource();
			templatewidget.Binding.AddBinding(Entity, e => e.ChangedTemplateFile, w => w.ChangedDoc).InitializeFromSource();

			templatewidget.BeforeOpen += Templatewidget_BeforeOpen;

			yTWEquipment.ColumnsConfig = ColumnsConfigFactory.Create<OrderEquipment>()
				.AddColumn("Наименование").SetDataProperty(node => node.FullNameString)
				.AddColumn("Направление").SetDataProperty(node => node.DirectionString)
				.AddColumn("Кол-во").AddNumericRenderer(node => node.Count).WidthChars(10)
				.Adjustment(new Adjustment(0, 0, 1000000, 1, 100, 0))
				.AddColumn("")
				.Finish();

			UpdateStates();
		}

		void GetDocument()
		{
			var doc = Entity.Employee.GetMainDocuments();
			if(doc.Any())
				Entity.EmployeeDocument = doc[0];
		}

		void FillForOrder()
		{
			Order order = Entity.Order;
			if(order != null) {
				equipmentList = Entity.Order.ObservableOrderEquipments.Where(eq => eq.Direction == Domain.Orders.Direction.PickUp).ToList<OrderEquipment>();
				Entity.Date = order.DeliveryDate != null ? order.DeliveryDate.Value : DateTime.Now;
				Entity.ExpirationDate = Entity.Date.AddDays(10);
				Entity.Supplier = order.Client;

				if(Entity.Id == 0)
				{
					Entity.Organization = order.Contract.Organization;
				}
				
				yTWEquipment.ItemsDataSource = equipmentList;
			} else {
				yDPDatesRange.StartDateOrNull = DateTime.Today;
				yDPDatesRange.EndDateOrNull = DateTime.Today.AddDays(10);
			}
		}

		void Templatewidget_BeforeOpen(object sender, EventArgs e)
		{
			var organizationVersion = Entity.Organization.OrganizationVersionOnDate(Entity.Date);

			if(organizationVersion == null)
			{
				MessageDialogHelper.RunErrorDialog($"На дату М-2 доверенности {Entity.Date.ToString("G")} отсутствует версия организации. Создайте версию организации.");
				templatewidget.CanOpenDocument = false;
				return;
			}

			if(organizationVersion.Leader == null)
			{
				MessageDialogHelper.RunErrorDialog($"Не выбран руководитель в версии №{organizationVersion.Id} организации \"{Entity.Organization.Name}\"");
				templatewidget.CanOpenDocument = false;
				return;
			}

			if(organizationVersion.Accountant == null)
			{
				MessageDialogHelper.RunErrorDialog($"Не выбран бухгалтер в версии №{organizationVersion.Id} организации \"{Entity.Organization.Name}\"");
				templatewidget.CanOpenDocument = false;
				return;
			}

			if(UoW.HasChanges) {
				if(MessageDialogHelper.RunQuestionDialog("Необходимо сохранить документ перед открытием печатной формы, сохранить?")) {
					UoWGeneric.Save();
					RefreshParserRootObject();
				} else {
					templatewidget.CanOpenDocument = false;
				}
			}
		}

		void RefreshParserRootObject()
		{
			if(Entity.DocumentTemplate == null)
				return;
			M2ProxyDocumentParser parser = (Entity.DocumentTemplate.DocParser as M2ProxyDocumentParser);
			parser.RootObject = Entity;
			parser.AddTableEquipmentFromClient(equipmentList);
		}

		void UpdateStates()
		{
			bool isNewDoc = !(Entity.Id > 0);
			evmeOrder.Sensitive = yDPDatesRange.Sensitive = evmeEmployee.Sensitive = evmeSupplier.Sensitive = yETicketNr.Sensitive
				= yDTicketDate.Sensitive = yTWEquipment.Sensitive = evmeOrganization.Sensitive = isNewDoc;

			if(Entity.Organization == null || !isNewDoc) {
				return;
			}
			templatewidget.AvailableTemplates = _docTemplateRepository.GetAvailableTemplates(UoW, TemplateType.M2Proxy, Entity.Organization);
			templatewidget.Template = templatewidget.AvailableTemplates.FirstOrDefault();
		}

		public override bool Save()
		{
			if(Entity.Order == null)
				return true;

			var valid = new QSValidator<M2ProxyDocument>(Entity);
			if(valid.RunDlgIfNotValid((Gtk.Window)this.Toplevel))
				return false;

			if(Entity.Order != null) {
				List<OrderDocument> list = new List<OrderDocument> {
					new OrderM2Proxy {
						AttachedToOrder = Entity.Order,
						M2Proxy = Entity,
						Order = Entity.Order
					}
				};
				Entity.Order.AddAdditionalDocuments(list);
			}

			return true;
		}
	}
}
