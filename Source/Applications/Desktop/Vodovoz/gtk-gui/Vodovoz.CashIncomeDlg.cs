
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class CashIncomeDlg
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.HBox hbox2;

		private global::Gtk.Button buttonSave;

		private global::Gtk.Button buttonCancel;

		private global::Gtk.Button buttonPrint;

		private global::Gtk.HBox hboxSubdivision;

		private global::Vodovoz.Core.Permissions.AccessFilteredSubdivisionSelectorWidget accessfilteredsubdivisionselectorwidget;

		private global::Gtk.Table table1;

		private global::Gamma.GtkWidgets.yCheckButton checkNoClose;

		private global::Gamma.Widgets.ySpecComboBox comboCategory;

		private global::Gamma.Widgets.ySpecComboBox comboExpense;

		private global::Gamma.Widgets.yEnumComboBox enumcomboOperation;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry evmeCashier;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry evmeEmployee;

		private global::Gtk.HBox hbox7;

		private global::Gamma.GtkWidgets.ySpinButton yspinMoney;

		private global::QSProjectsLib.CurrencyLabel currencylabel1;

		private global::Gtk.Label label1;

		private global::Gtk.Label label2;

		private global::Gtk.Label label3;

		private global::Gtk.Label label5;

		private global::Gtk.Label labelClientTitle;

		private global::Gtk.Label labelEmploeey;

		private global::Gtk.Label labelExpenseTitle;

		private global::Gtk.Label labelIncomeTitle;

		private global::Gtk.Label lblRouteList;

		private global::QS.Widgets.GtkUI.SpecialListComboBox specialListCmbOrganisation;

		private global::QS.Widgets.GtkUI.DatePicker ydateDocument;

		private global::Gamma.Widgets.yEntryReference yentryClient;

		private global::QS.Widgets.GtkUI.RepresentationEntry yEntryRouteList;

		private global::Gamma.GtkWidgets.yLabel ylabel1;

		private global::Gtk.VBox vboxDebts;

		private global::Gtk.Label labelTableTitle;

		private global::Gtk.ScrolledWindow GtkScrolledWindow1;

		private global::Gamma.GtkWidgets.yTreeView ytreeviewDebts;

		private global::Gtk.Label label6;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gamma.GtkWidgets.yTextView ytextviewDescription;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.CashIncomeDlg
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.CashIncomeDlg";
			// Container child Vodovoz.CashIncomeDlg.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonSave = new global::Gtk.Button();
			this.buttonSave.CanFocus = true;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.UseUnderline = true;
			this.buttonSave.Label = global::Mono.Unix.Catalog.GetString("Сохранить");
			global::Gtk.Image w1 = new global::Gtk.Image();
			w1.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-save", global::Gtk.IconSize.Menu);
			this.buttonSave.Image = w1;
			this.hbox2.Add(this.buttonSave);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.buttonSave]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString("Отменить");
			global::Gtk.Image w3 = new global::Gtk.Image();
			w3.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-revert-to-saved", global::Gtk.IconSize.Menu);
			this.buttonCancel.Image = w3;
			this.hbox2.Add(this.buttonCancel);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.buttonCancel]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonPrint = new global::Gtk.Button();
			this.buttonPrint.CanFocus = true;
			this.buttonPrint.Name = "buttonPrint";
			this.buttonPrint.UseUnderline = true;
			this.buttonPrint.Label = global::Mono.Unix.Catalog.GetString("Печать");
			global::Gtk.Image w5 = new global::Gtk.Image();
			w5.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-print", global::Gtk.IconSize.Menu);
			this.buttonPrint.Image = w5;
			this.hbox2.Add(this.buttonPrint);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.buttonPrint]));
			w6.Position = 2;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.hboxSubdivision = new global::Gtk.HBox();
			this.hboxSubdivision.Name = "hboxSubdivision";
			this.hboxSubdivision.Spacing = 6;
			// Container child hboxSubdivision.Gtk.Box+BoxChild
			this.accessfilteredsubdivisionselectorwidget = new global::Vodovoz.Core.Permissions.AccessFilteredSubdivisionSelectorWidget();
			this.accessfilteredsubdivisionselectorwidget.Events = ((global::Gdk.EventMask)(256));
			this.accessfilteredsubdivisionselectorwidget.Name = "accessfilteredsubdivisionselectorwidget";
			this.accessfilteredsubdivisionselectorwidget.NeedChooseSubdivision = false;
			this.hboxSubdivision.Add(this.accessfilteredsubdivisionselectorwidget);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hboxSubdivision[this.accessfilteredsubdivisionselectorwidget]));
			w7.PackType = ((global::Gtk.PackType)(1));
			w7.Position = 2;
			w7.Expand = false;
			w7.Fill = false;
			this.hbox2.Add(this.hboxSubdivision);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.hboxSubdivision]));
			w8.Position = 3;
			this.vbox1.Add(this.hbox2);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox2]));
			w9.Position = 0;
			w9.Expand = false;
			w9.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table(((uint)(9)), ((uint)(4)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.checkNoClose = new global::Gamma.GtkWidgets.yCheckButton();
			this.checkNoClose.CanFocus = true;
			this.checkNoClose.Name = "checkNoClose";
			this.checkNoClose.Label = global::Mono.Unix.Catalog.GetString("Частичный возврат");
			this.checkNoClose.DrawIndicator = true;
			this.checkNoClose.UseUnderline = true;
			this.table1.Add(this.checkNoClose);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1[this.checkNoClose]));
			w10.TopAttach = ((uint)(7));
			w10.BottomAttach = ((uint)(8));
			w10.LeftAttach = ((uint)(1));
			w10.RightAttach = ((uint)(2));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.comboCategory = new global::Gamma.Widgets.ySpecComboBox();
			this.comboCategory.Name = "comboCategory";
			this.comboCategory.AddIfNotExist = false;
			this.comboCategory.DefaultFirst = false;
			this.comboCategory.ShowSpecialStateAll = false;
			this.comboCategory.ShowSpecialStateNot = true;
			this.table1.Add(this.comboCategory);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1[this.comboCategory]));
			w11.TopAttach = ((uint)(1));
			w11.BottomAttach = ((uint)(2));
			w11.LeftAttach = ((uint)(1));
			w11.RightAttach = ((uint)(2));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.comboExpense = new global::Gamma.Widgets.ySpecComboBox();
			this.comboExpense.Name = "comboExpense";
			this.comboExpense.AddIfNotExist = false;
			this.comboExpense.DefaultFirst = false;
			this.comboExpense.ShowSpecialStateAll = false;
			this.comboExpense.ShowSpecialStateNot = true;
			this.table1.Add(this.comboExpense);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table1[this.comboExpense]));
			w12.TopAttach = ((uint)(2));
			w12.BottomAttach = ((uint)(3));
			w12.LeftAttach = ((uint)(1));
			w12.RightAttach = ((uint)(2));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.enumcomboOperation = new global::Gamma.Widgets.yEnumComboBox();
			this.enumcomboOperation.Name = "enumcomboOperation";
			this.enumcomboOperation.ShowSpecialStateAll = false;
			this.enumcomboOperation.ShowSpecialStateNot = false;
			this.enumcomboOperation.UseShortTitle = false;
			this.enumcomboOperation.DefaultFirst = false;
			this.table1.Add(this.enumcomboOperation);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.table1[this.enumcomboOperation]));
			w13.LeftAttach = ((uint)(1));
			w13.RightAttach = ((uint)(2));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.evmeCashier = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.evmeCashier.Sensitive = false;
			this.evmeCashier.Events = ((global::Gdk.EventMask)(256));
			this.evmeCashier.Name = "evmeCashier";
			this.evmeCashier.CanEditReference = true;
			this.table1.Add(this.evmeCashier);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.table1[this.evmeCashier]));
			w14.LeftAttach = ((uint)(3));
			w14.RightAttach = ((uint)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.evmeEmployee = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.evmeEmployee.Events = ((global::Gdk.EventMask)(256));
			this.evmeEmployee.Name = "evmeEmployee";
			this.evmeEmployee.CanEditReference = true;
			this.table1.Add(this.evmeEmployee);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.table1[this.evmeEmployee]));
			w15.TopAttach = ((uint)(6));
			w15.BottomAttach = ((uint)(7));
			w15.LeftAttach = ((uint)(1));
			w15.RightAttach = ((uint)(2));
			w15.XOptions = ((global::Gtk.AttachOptions)(4));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hbox7 = new global::Gtk.HBox();
			this.hbox7.Name = "hbox7";
			this.hbox7.Spacing = 6;
			// Container child hbox7.Gtk.Box+BoxChild
			this.yspinMoney = new global::Gamma.GtkWidgets.ySpinButton(0D, 2147483647D, 100D);
			this.yspinMoney.CanDefault = true;
			this.yspinMoney.CanFocus = true;
			this.yspinMoney.Name = "yspinMoney";
			this.yspinMoney.Adjustment.PageIncrement = 1000D;
			this.yspinMoney.ClimbRate = 1D;
			this.yspinMoney.Digits = ((uint)(2));
			this.yspinMoney.Numeric = true;
			this.yspinMoney.ValueAsDecimal = 0m;
			this.yspinMoney.ValueAsInt = 0;
			this.hbox7.Add(this.yspinMoney);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.yspinMoney]));
			w16.Position = 0;
			// Container child hbox7.Gtk.Box+BoxChild
			this.currencylabel1 = new global::QSProjectsLib.CurrencyLabel();
			this.currencylabel1.Name = "currencylabel1";
			this.currencylabel1.LabelProp = global::Mono.Unix.Catalog.GetString("currencylabel1");
			this.hbox7.Add(this.currencylabel1);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.currencylabel1]));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			this.table1.Add(this.hbox7);
			global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.table1[this.hbox7]));
			w18.TopAttach = ((uint)(8));
			w18.BottomAttach = ((uint)(9));
			w18.LeftAttach = ((uint)(1));
			w18.RightAttach = ((uint)(2));
			w18.XOptions = ((global::Gtk.AttachOptions)(4));
			w18.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.Xalign = 1F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Дата:");
			this.table1.Add(this.label1);
			global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.table1[this.label1]));
			w19.TopAttach = ((uint)(1));
			w19.BottomAttach = ((uint)(2));
			w19.LeftAttach = ((uint)(2));
			w19.RightAttach = ((uint)(3));
			w19.XOptions = ((global::Gtk.AttachOptions)(4));
			w19.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.Xalign = 1F;
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Кассир:");
			this.table1.Add(this.label2);
			global::Gtk.Table.TableChild w20 = ((global::Gtk.Table.TableChild)(this.table1[this.label2]));
			w20.LeftAttach = ((uint)(2));
			w20.RightAttach = ((uint)(3));
			w20.XOptions = ((global::Gtk.AttachOptions)(4));
			w20.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.label3.Xalign = 1F;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Тип операции:");
			this.table1.Add(this.label3);
			global::Gtk.Table.TableChild w21 = ((global::Gtk.Table.TableChild)(this.table1[this.label3]));
			w21.XOptions = ((global::Gtk.AttachOptions)(4));
			w21.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label5 = new global::Gtk.Label();
			this.label5.Name = "label5";
			this.label5.Xalign = 1F;
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString("Сумма:");
			this.table1.Add(this.label5);
			global::Gtk.Table.TableChild w22 = ((global::Gtk.Table.TableChild)(this.table1[this.label5]));
			w22.TopAttach = ((uint)(8));
			w22.BottomAttach = ((uint)(9));
			w22.XOptions = ((global::Gtk.AttachOptions)(4));
			w22.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelClientTitle = new global::Gtk.Label();
			this.labelClientTitle.Name = "labelClientTitle";
			this.labelClientTitle.Xalign = 1F;
			this.labelClientTitle.LabelProp = global::Mono.Unix.Catalog.GetString("Клиент:");
			this.table1.Add(this.labelClientTitle);
			global::Gtk.Table.TableChild w23 = ((global::Gtk.Table.TableChild)(this.table1[this.labelClientTitle]));
			w23.TopAttach = ((uint)(5));
			w23.BottomAttach = ((uint)(6));
			w23.XOptions = ((global::Gtk.AttachOptions)(4));
			w23.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelEmploeey = new global::Gtk.Label();
			this.labelEmploeey.Name = "labelEmploeey";
			this.labelEmploeey.Xalign = 1F;
			this.labelEmploeey.LabelProp = global::Mono.Unix.Catalog.GetString("Сотрудник:");
			this.table1.Add(this.labelEmploeey);
			global::Gtk.Table.TableChild w24 = ((global::Gtk.Table.TableChild)(this.table1[this.labelEmploeey]));
			w24.TopAttach = ((uint)(6));
			w24.BottomAttach = ((uint)(7));
			w24.XOptions = ((global::Gtk.AttachOptions)(4));
			w24.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelExpenseTitle = new global::Gtk.Label();
			this.labelExpenseTitle.Name = "labelExpenseTitle";
			this.labelExpenseTitle.Xalign = 1F;
			this.labelExpenseTitle.LabelProp = global::Mono.Unix.Catalog.GetString("Статья расхода:");
			this.table1.Add(this.labelExpenseTitle);
			global::Gtk.Table.TableChild w25 = ((global::Gtk.Table.TableChild)(this.table1[this.labelExpenseTitle]));
			w25.TopAttach = ((uint)(2));
			w25.BottomAttach = ((uint)(3));
			w25.XOptions = ((global::Gtk.AttachOptions)(4));
			w25.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelIncomeTitle = new global::Gtk.Label();
			this.labelIncomeTitle.Name = "labelIncomeTitle";
			this.labelIncomeTitle.Xalign = 1F;
			this.labelIncomeTitle.LabelProp = global::Mono.Unix.Catalog.GetString("Статья дохода:");
			this.table1.Add(this.labelIncomeTitle);
			global::Gtk.Table.TableChild w26 = ((global::Gtk.Table.TableChild)(this.table1[this.labelIncomeTitle]));
			w26.TopAttach = ((uint)(1));
			w26.BottomAttach = ((uint)(2));
			w26.XOptions = ((global::Gtk.AttachOptions)(4));
			w26.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.lblRouteList = new global::Gtk.Label();
			this.lblRouteList.Name = "lblRouteList";
			this.lblRouteList.Xalign = 1F;
			this.lblRouteList.LabelProp = global::Mono.Unix.Catalog.GetString("Маршрутный лист:");
			this.table1.Add(this.lblRouteList);
			global::Gtk.Table.TableChild w27 = ((global::Gtk.Table.TableChild)(this.table1[this.lblRouteList]));
			w27.TopAttach = ((uint)(3));
			w27.BottomAttach = ((uint)(4));
			w27.XOptions = ((global::Gtk.AttachOptions)(4));
			w27.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.specialListCmbOrganisation = new global::QS.Widgets.GtkUI.SpecialListComboBox();
			this.specialListCmbOrganisation.Name = "specialListCmbOrganisation";
			this.specialListCmbOrganisation.AddIfNotExist = false;
			this.specialListCmbOrganisation.DefaultFirst = false;
			this.specialListCmbOrganisation.ShowSpecialStateAll = false;
			this.specialListCmbOrganisation.ShowSpecialStateNot = false;
			this.table1.Add(this.specialListCmbOrganisation);
			global::Gtk.Table.TableChild w28 = ((global::Gtk.Table.TableChild)(this.table1[this.specialListCmbOrganisation]));
			w28.TopAttach = ((uint)(4));
			w28.BottomAttach = ((uint)(5));
			w28.LeftAttach = ((uint)(1));
			w28.RightAttach = ((uint)(2));
			w28.XOptions = ((global::Gtk.AttachOptions)(4));
			w28.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ydateDocument = new global::QS.Widgets.GtkUI.DatePicker();
			this.ydateDocument.Events = ((global::Gdk.EventMask)(256));
			this.ydateDocument.Name = "ydateDocument";
			this.ydateDocument.WithTime = false;
			this.ydateDocument.HideCalendarButton = false;
			this.ydateDocument.Date = new global::System.DateTime(0);
			this.ydateDocument.IsEditable = true;
			this.ydateDocument.AutoSeparation = true;
			this.table1.Add(this.ydateDocument);
			global::Gtk.Table.TableChild w29 = ((global::Gtk.Table.TableChild)(this.table1[this.ydateDocument]));
			w29.TopAttach = ((uint)(1));
			w29.BottomAttach = ((uint)(2));
			w29.LeftAttach = ((uint)(3));
			w29.RightAttach = ((uint)(4));
			w29.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yentryClient = new global::Gamma.Widgets.yEntryReference();
			this.yentryClient.Events = ((global::Gdk.EventMask)(256));
			this.yentryClient.Name = "yentryClient";
			this.table1.Add(this.yentryClient);
			global::Gtk.Table.TableChild w30 = ((global::Gtk.Table.TableChild)(this.table1[this.yentryClient]));
			w30.TopAttach = ((uint)(5));
			w30.BottomAttach = ((uint)(6));
			w30.LeftAttach = ((uint)(1));
			w30.RightAttach = ((uint)(2));
			w30.XOptions = ((global::Gtk.AttachOptions)(4));
			w30.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yEntryRouteList = new global::QS.Widgets.GtkUI.RepresentationEntry();
			this.yEntryRouteList.Events = ((global::Gdk.EventMask)(256));
			this.yEntryRouteList.Name = "yEntryRouteList";
			this.table1.Add(this.yEntryRouteList);
			global::Gtk.Table.TableChild w31 = ((global::Gtk.Table.TableChild)(this.table1[this.yEntryRouteList]));
			w31.TopAttach = ((uint)(3));
			w31.BottomAttach = ((uint)(4));
			w31.LeftAttach = ((uint)(1));
			w31.RightAttach = ((uint)(2));
			w31.XOptions = ((global::Gtk.AttachOptions)(4));
			w31.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabel1 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel1.Name = "ylabel1";
			this.ylabel1.Xalign = 1F;
			this.ylabel1.LabelProp = global::Mono.Unix.Catalog.GetString("Организация:");
			this.table1.Add(this.ylabel1);
			global::Gtk.Table.TableChild w32 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabel1]));
			w32.TopAttach = ((uint)(4));
			w32.BottomAttach = ((uint)(5));
			w32.XOptions = ((global::Gtk.AttachOptions)(4));
			w32.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox1.Add(this.table1);
			global::Gtk.Box.BoxChild w33 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.table1]));
			w33.Position = 1;
			w33.Expand = false;
			w33.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.vboxDebts = new global::Gtk.VBox();
			this.vboxDebts.Name = "vboxDebts";
			this.vboxDebts.Spacing = 6;
			this.vboxDebts.BorderWidth = ((uint)(6));
			// Container child vboxDebts.Gtk.Box+BoxChild
			this.labelTableTitle = new global::Gtk.Label();
			this.labelTableTitle.Name = "labelTableTitle";
			this.labelTableTitle.Xalign = 0F;
			this.labelTableTitle.LabelProp = global::Mono.Unix.Catalog.GetString("Последние выданные авансы:");
			this.vboxDebts.Add(this.labelTableTitle);
			global::Gtk.Box.BoxChild w34 = ((global::Gtk.Box.BoxChild)(this.vboxDebts[this.labelTableTitle]));
			w34.Position = 0;
			w34.Expand = false;
			w34.Fill = false;
			// Container child vboxDebts.Gtk.Box+BoxChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.ytreeviewDebts = new global::Gamma.GtkWidgets.yTreeView();
			this.ytreeviewDebts.CanFocus = true;
			this.ytreeviewDebts.Name = "ytreeviewDebts";
			this.GtkScrolledWindow1.Add(this.ytreeviewDebts);
			this.vboxDebts.Add(this.GtkScrolledWindow1);
			global::Gtk.Box.BoxChild w36 = ((global::Gtk.Box.BoxChild)(this.vboxDebts[this.GtkScrolledWindow1]));
			w36.Position = 1;
			this.vbox1.Add(this.vboxDebts);
			global::Gtk.Box.BoxChild w37 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.vboxDebts]));
			w37.Position = 2;
			// Container child vbox1.Gtk.Box+BoxChild
			this.label6 = new global::Gtk.Label();
			this.label6.Name = "label6";
			this.label6.Xalign = 0F;
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString("Основание:");
			this.vbox1.Add(this.label6);
			global::Gtk.Box.BoxChild w38 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.label6]));
			w38.Position = 3;
			w38.Expand = false;
			w38.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.ytextviewDescription = new global::Gamma.GtkWidgets.yTextView();
			this.ytextviewDescription.CanFocus = true;
			this.ytextviewDescription.Name = "ytextviewDescription";
			this.ytextviewDescription.WrapMode = ((global::Gtk.WrapMode)(3));
			this.GtkScrolledWindow.Add(this.ytextviewDescription);
			this.vbox1.Add(this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w40 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow]));
			w40.Position = 4;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.comboExpense.Hide();
			this.labelExpenseTitle.Hide();
			this.lblRouteList.Hide();
			this.vboxDebts.Hide();
			this.Hide();
			this.buttonPrint.Clicked += new global::System.EventHandler(this.OnButtonPrintClicked);
			this.enumcomboOperation.EnumItemSelected += new global::System.EventHandler<Gamma.Widgets.ItemSelectedEventArgs>(this.OnEnumcomboOperationEnumItemSelected);
			this.comboExpense.ItemSelected += new global::System.EventHandler<Gamma.Widgets.ItemSelectedEventArgs>(this.OnComboExpenseItemSelected);
			this.checkNoClose.Toggled += new global::System.EventHandler(this.OnCheckNoCloseToggled);
		}
	}
}
