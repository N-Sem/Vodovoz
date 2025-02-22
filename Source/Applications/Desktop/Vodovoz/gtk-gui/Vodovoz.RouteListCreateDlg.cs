
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class RouteListCreateDlg
	{
		private global::Gamma.GtkWidgets.yVBox vboxMain;

		private global::Gamma.GtkWidgets.yHBox hboxMenu;

		private global::Gtk.Button buttonSave;

		private global::Gtk.Button btnCancel;

		private global::Gtk.VSeparator vseparator3;

		private global::Gamma.GtkWidgets.yRadioButton radioBtnInformation;

		private global::Gamma.GtkWidgets.yRadioButton radioBtnProfitability;

		private global::Gamma.GtkWidgets.yNotebook ynotebook1;

		private global::Gtk.VBox vbox1;

		private global::Gtk.Table dataRouteList;

		private global::QS.Widgets.GtkUI.DatePicker datepickerDate;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entityviewmodelentryCar;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry evmeDriver;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry evmeForwarder;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry evmeLogistician;

		private global::Vodovoz.ViewWidgets.GeographicGroupsToStringWidget ggToStringWidget;

		private global::Gtk.HBox hbox7;

		private global::Gtk.HBox hboxCash;

		private global::Gamma.GtkWidgets.yLabel labelTerminalCondition;

		private global::Gtk.VSeparator vseparator1;

		private global::Gtk.Label label8;

		private global::Gtk.HBox hboxCash2;

		private global::Gtk.VSeparator vseparator4;

		private global::Gamma.GtkWidgets.yCheckButton checkIsFixPrice;

		private global::Gamma.GtkWidgets.ySpinButton fixPriceSpin;

		private global::Gamma.GtkWidgets.yHBox hboxDriverComment;

		private global::Gamma.GtkWidgets.yLabel lblDriverCommentTitle;

		private global::Gamma.GtkWidgets.yLabel lblDriverComment;

		private global::Gamma.GtkWidgets.yHBox hboxForwarderComment;

		private global::Gamma.GtkWidgets.yLabel lblForwarderCommentTitle;

		private global::Gamma.GtkWidgets.yLabel lblForwarderComment;

		private global::Gtk.HSeparator hseparator2;

		private global::Gtk.HSeparator hseparator3;

		private global::Gtk.Label label1;

		private global::Gtk.Label label10;

		private global::Gtk.Label label3;

		private global::Gtk.Label label4;

		private global::Gtk.Label label5;

		private global::Gtk.Label label6;

		private global::Gtk.Label label7;

		private global::Gtk.Label label9;

		private global::Gamma.GtkWidgets.yLabel labelStatus;

		private global::Vodovoz.ViewWidgets.Mango.EmployeePhone phoneDriver;

		private global::Vodovoz.ViewWidgets.Mango.EmployeePhone phoneForwarder;

		private global::Vodovoz.ViewWidgets.Mango.EmployeePhone phoneLogistican;

		private global::Gamma.Widgets.ySpecComboBox speccomboShift;

		private global::Gtk.HBox hbox8;

		private global::Vodovoz.RouteListCreateItemsView createroutelistitemsview1;

		private global::Gtk.VSeparator vseparator2;

		private global::Gtk.HBox hboxAdditionalLoading;

		private global::Gtk.HBox hbox11;

		private global::Gamma.GtkWidgets.yButton printTimeButton;

		private global::QS.Widgets.EnumMenuButton enumPrint;

		private global::Gtk.Button buttonAccept;

		private global::Gamma.GtkWidgets.yButton ybuttonAddAdditionalLoad;

		private global::Gamma.GtkWidgets.yButton ybuttonRemoveAdditionalLoad;

		private global::Gtk.Label lblInformation;

		private global::Gtk.ScrolledWindow GtkScrolledWindow1;

		private global::Gamma.GtkWidgets.yTreeView treeRouteListProfitability;

		private global::Gtk.Label lblProfitability;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.RouteListCreateDlg
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.RouteListCreateDlg";
			// Container child Vodovoz.RouteListCreateDlg.Gtk.Container+ContainerChild
			this.vboxMain = new global::Gamma.GtkWidgets.yVBox();
			this.vboxMain.Name = "vboxMain";
			this.vboxMain.Spacing = 6;
			// Container child vboxMain.Gtk.Box+BoxChild
			this.hboxMenu = new global::Gamma.GtkWidgets.yHBox();
			this.hboxMenu.Name = "hboxMenu";
			this.hboxMenu.Spacing = 6;
			// Container child hboxMenu.Gtk.Box+BoxChild
			this.buttonSave = new global::Gtk.Button();
			this.buttonSave.CanFocus = true;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.UseUnderline = true;
			this.buttonSave.Label = global::Mono.Unix.Catalog.GetString("Сохранить");
			global::Gtk.Image w1 = new global::Gtk.Image();
			w1.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-save", global::Gtk.IconSize.Menu);
			this.buttonSave.Image = w1;
			this.hboxMenu.Add(this.buttonSave);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hboxMenu[this.buttonSave]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hboxMenu.Gtk.Box+BoxChild
			this.btnCancel = new global::Gtk.Button();
			this.btnCancel.CanFocus = true;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseUnderline = true;
			this.btnCancel.Label = global::Mono.Unix.Catalog.GetString("Отмена");
			global::Gtk.Image w3 = new global::Gtk.Image();
			w3.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-revert-to-saved", global::Gtk.IconSize.Menu);
			this.btnCancel.Image = w3;
			this.hboxMenu.Add(this.btnCancel);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hboxMenu[this.btnCancel]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hboxMenu.Gtk.Box+BoxChild
			this.vseparator3 = new global::Gtk.VSeparator();
			this.vseparator3.Name = "vseparator3";
			this.hboxMenu.Add(this.vseparator3);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hboxMenu[this.vseparator3]));
			w5.Position = 2;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hboxMenu.Gtk.Box+BoxChild
			this.radioBtnInformation = new global::Gamma.GtkWidgets.yRadioButton();
			this.radioBtnInformation.CanFocus = true;
			this.radioBtnInformation.Name = "radioBtnInformation";
			this.radioBtnInformation.Label = global::Mono.Unix.Catalog.GetString("Информация");
			this.radioBtnInformation.DrawIndicator = false;
			this.radioBtnInformation.UseUnderline = true;
			this.radioBtnInformation.Group = new global::GLib.SList(global::System.IntPtr.Zero);
			this.hboxMenu.Add(this.radioBtnInformation);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hboxMenu[this.radioBtnInformation]));
			w6.Position = 3;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hboxMenu.Gtk.Box+BoxChild
			this.radioBtnProfitability = new global::Gamma.GtkWidgets.yRadioButton();
			this.radioBtnProfitability.CanFocus = true;
			this.radioBtnProfitability.Name = "radioBtnProfitability";
			this.radioBtnProfitability.Label = global::Mono.Unix.Catalog.GetString("Рентабельность");
			this.radioBtnProfitability.DrawIndicator = false;
			this.radioBtnProfitability.UseUnderline = true;
			this.radioBtnProfitability.Group = this.radioBtnInformation.Group;
			this.hboxMenu.Add(this.radioBtnProfitability);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hboxMenu[this.radioBtnProfitability]));
			w7.Position = 4;
			w7.Expand = false;
			w7.Fill = false;
			this.vboxMain.Add(this.hboxMenu);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vboxMain[this.hboxMenu]));
			w8.Position = 0;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vboxMain.Gtk.Box+BoxChild
			this.ynotebook1 = new global::Gamma.GtkWidgets.yNotebook();
			this.ynotebook1.CanFocus = true;
			this.ynotebook1.Name = "ynotebook1";
			this.ynotebook1.CurrentPage = 0;
			// Container child ynotebook1.Gtk.Notebook+NotebookChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			this.vbox1.BorderWidth = ((uint)(6));
			// Container child vbox1.Gtk.Box+BoxChild
			this.dataRouteList = new global::Gtk.Table(((uint)(8)), ((uint)(7)), false);
			this.dataRouteList.Name = "dataRouteList";
			this.dataRouteList.RowSpacing = ((uint)(6));
			this.dataRouteList.ColumnSpacing = ((uint)(6));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.datepickerDate = new global::QS.Widgets.GtkUI.DatePicker();
			this.datepickerDate.Events = ((global::Gdk.EventMask)(256));
			this.datepickerDate.Name = "datepickerDate";
			this.datepickerDate.WithTime = false;
			this.datepickerDate.HideCalendarButton = false;
			this.datepickerDate.Date = new global::System.DateTime(0);
			this.datepickerDate.IsEditable = true;
			this.datepickerDate.AutoSeparation = true;
			this.dataRouteList.Add(this.datepickerDate);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.datepickerDate]));
			w9.TopAttach = ((uint)(3));
			w9.BottomAttach = ((uint)(4));
			w9.LeftAttach = ((uint)(1));
			w9.RightAttach = ((uint)(2));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.entityviewmodelentryCar = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entityviewmodelentryCar.Events = ((global::Gdk.EventMask)(256));
			this.entityviewmodelentryCar.Name = "entityviewmodelentryCar";
			this.entityviewmodelentryCar.CanEditReference = true;
			this.entityviewmodelentryCar.CanOpenWithoutTabParent = false;
			this.dataRouteList.Add(this.entityviewmodelentryCar);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.entityviewmodelentryCar]));
			w10.TopAttach = ((uint)(2));
			w10.BottomAttach = ((uint)(3));
			w10.LeftAttach = ((uint)(5));
			w10.RightAttach = ((uint)(6));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.evmeDriver = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.evmeDriver.Events = ((global::Gdk.EventMask)(256));
			this.evmeDriver.Name = "evmeDriver";
			this.evmeDriver.CanEditReference = true;
			this.evmeDriver.CanOpenWithoutTabParent = false;
			this.dataRouteList.Add(this.evmeDriver);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.evmeDriver]));
			w11.TopAttach = ((uint)(3));
			w11.BottomAttach = ((uint)(4));
			w11.LeftAttach = ((uint)(5));
			w11.RightAttach = ((uint)(6));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.evmeForwarder = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.evmeForwarder.Events = ((global::Gdk.EventMask)(256));
			this.evmeForwarder.Name = "evmeForwarder";
			this.evmeForwarder.CanEditReference = true;
			this.evmeForwarder.CanOpenWithoutTabParent = false;
			this.dataRouteList.Add(this.evmeForwarder);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.evmeForwarder]));
			w12.TopAttach = ((uint)(5));
			w12.BottomAttach = ((uint)(6));
			w12.LeftAttach = ((uint)(5));
			w12.RightAttach = ((uint)(6));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.evmeLogistician = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.evmeLogistician.Events = ((global::Gdk.EventMask)(256));
			this.evmeLogistician.Name = "evmeLogistician";
			this.evmeLogistician.CanEditReference = true;
			this.evmeLogistician.CanOpenWithoutTabParent = false;
			this.dataRouteList.Add(this.evmeLogistician);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.evmeLogistician]));
			w13.LeftAttach = ((uint)(5));
			w13.RightAttach = ((uint)(6));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.ggToStringWidget = new global::Vodovoz.ViewWidgets.GeographicGroupsToStringWidget();
			this.ggToStringWidget.Events = ((global::Gdk.EventMask)(256));
			this.ggToStringWidget.Name = "ggToStringWidget";
			this.dataRouteList.Add(this.ggToStringWidget);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.ggToStringWidget]));
			w14.TopAttach = ((uint)(5));
			w14.BottomAttach = ((uint)(6));
			w14.RightAttach = ((uint)(2));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.hbox7 = new global::Gtk.HBox();
			this.hbox7.Name = "hbox7";
			this.hbox7.Spacing = 6;
			this.dataRouteList.Add(this.hbox7);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.hbox7]));
			w15.XOptions = ((global::Gtk.AttachOptions)(0));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.hboxCash = new global::Gtk.HBox();
			this.hboxCash.Name = "hboxCash";
			this.hboxCash.Spacing = 6;
			// Container child hboxCash.Gtk.Box+BoxChild
			this.labelTerminalCondition = new global::Gamma.GtkWidgets.yLabel();
			this.labelTerminalCondition.Name = "labelTerminalCondition";
			this.labelTerminalCondition.LabelProp = global::Mono.Unix.Catalog.GetString("Состояние терминала: ");
			this.hboxCash.Add(this.labelTerminalCondition);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hboxCash[this.labelTerminalCondition]));
			w16.Position = 0;
			w16.Expand = false;
			w16.Fill = false;
			// Container child hboxCash.Gtk.Box+BoxChild
			this.vseparator1 = new global::Gtk.VSeparator();
			this.vseparator1.Name = "vseparator1";
			this.hboxCash.Add(this.vseparator1);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hboxCash[this.vseparator1]));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			// Container child hboxCash.Gtk.Box+BoxChild
			this.label8 = new global::Gtk.Label();
			this.label8.Name = "label8";
			this.label8.LabelProp = global::Mono.Unix.Catalog.GetString("Сдается в кассу:");
			this.hboxCash.Add(this.label8);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.hboxCash[this.label8]));
			w18.Position = 2;
			w18.Expand = false;
			w18.Fill = false;
			this.dataRouteList.Add(this.hboxCash);
			global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.hboxCash]));
			w19.LeftAttach = ((uint)(2));
			w19.RightAttach = ((uint)(4));
			w19.XOptions = ((global::Gtk.AttachOptions)(0));
			w19.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.hboxCash2 = new global::Gtk.HBox();
			this.hboxCash2.Name = "hboxCash2";
			this.hboxCash2.Spacing = 6;
			// Container child hboxCash2.Gtk.Box+BoxChild
			this.vseparator4 = new global::Gtk.VSeparator();
			this.vseparator4.Name = "vseparator4";
			this.hboxCash2.Add(this.vseparator4);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hboxCash2[this.vseparator4]));
			w20.Position = 0;
			w20.Expand = false;
			w20.Fill = false;
			// Container child hboxCash2.Gtk.Box+BoxChild
			this.checkIsFixPrice = new global::Gamma.GtkWidgets.yCheckButton();
			this.checkIsFixPrice.CanFocus = true;
			this.checkIsFixPrice.Name = "checkIsFixPrice";
			this.checkIsFixPrice.Label = "";
			this.checkIsFixPrice.DrawIndicator = true;
			this.checkIsFixPrice.UseUnderline = true;
			this.hboxCash2.Add(this.checkIsFixPrice);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.hboxCash2[this.checkIsFixPrice]));
			w21.Position = 1;
			w21.Expand = false;
			w21.Fill = false;
			// Container child hboxCash2.Gtk.Box+BoxChild
			this.fixPriceSpin = new global::Gamma.GtkWidgets.ySpinButton(0D, 100000D, 1D);
			this.fixPriceSpin.CanFocus = true;
			this.fixPriceSpin.Name = "fixPriceSpin";
			this.fixPriceSpin.Adjustment.PageIncrement = 1D;
			this.fixPriceSpin.ClimbRate = 1D;
			this.fixPriceSpin.Numeric = true;
			this.fixPriceSpin.ValueAsDecimal = 0m;
			this.fixPriceSpin.ValueAsInt = 0;
			this.hboxCash2.Add(this.fixPriceSpin);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.hboxCash2[this.fixPriceSpin]));
			w22.Position = 2;
			this.dataRouteList.Add(this.hboxCash2);
			global::Gtk.Table.TableChild w23 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.hboxCash2]));
			w23.TopAttach = ((uint)(2));
			w23.BottomAttach = ((uint)(3));
			w23.LeftAttach = ((uint)(3));
			w23.RightAttach = ((uint)(4));
			w23.XOptions = ((global::Gtk.AttachOptions)(4));
			w23.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.hboxDriverComment = new global::Gamma.GtkWidgets.yHBox();
			this.hboxDriverComment.Name = "hboxDriverComment";
			this.hboxDriverComment.Spacing = 6;
			// Container child hboxDriverComment.Gtk.Box+BoxChild
			this.lblDriverCommentTitle = new global::Gamma.GtkWidgets.yLabel();
			this.lblDriverCommentTitle.Name = "lblDriverCommentTitle";
			this.lblDriverCommentTitle.Xalign = 1F;
			this.lblDriverCommentTitle.LabelProp = global::Mono.Unix.Catalog.GetString("Комментарий по водителю:");
			this.hboxDriverComment.Add(this.lblDriverCommentTitle);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.hboxDriverComment[this.lblDriverCommentTitle]));
			w24.Position = 0;
			w24.Expand = false;
			w24.Fill = false;
			// Container child hboxDriverComment.Gtk.Box+BoxChild
			this.lblDriverComment = new global::Gamma.GtkWidgets.yLabel();
			this.lblDriverComment.Name = "lblDriverComment";
			this.lblDriverComment.LabelProp = global::Mono.Unix.Catalog.GetString("\"Комментарий по водителю\"");
			this.hboxDriverComment.Add(this.lblDriverComment);
			global::Gtk.Box.BoxChild w25 = ((global::Gtk.Box.BoxChild)(this.hboxDriverComment[this.lblDriverComment]));
			w25.Position = 1;
			w25.Expand = false;
			w25.Fill = false;
			this.dataRouteList.Add(this.hboxDriverComment);
			global::Gtk.Table.TableChild w26 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.hboxDriverComment]));
			w26.TopAttach = ((uint)(4));
			w26.BottomAttach = ((uint)(5));
			w26.LeftAttach = ((uint)(3));
			w26.RightAttach = ((uint)(6));
			w26.XOptions = ((global::Gtk.AttachOptions)(0));
			w26.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.hboxForwarderComment = new global::Gamma.GtkWidgets.yHBox();
			this.hboxForwarderComment.Name = "hboxForwarderComment";
			this.hboxForwarderComment.Spacing = 6;
			// Container child hboxForwarderComment.Gtk.Box+BoxChild
			this.lblForwarderCommentTitle = new global::Gamma.GtkWidgets.yLabel();
			this.lblForwarderCommentTitle.Name = "lblForwarderCommentTitle";
			this.lblForwarderCommentTitle.Xalign = 1F;
			this.lblForwarderCommentTitle.LabelProp = global::Mono.Unix.Catalog.GetString("Комментарий по экспедитору:");
			this.hboxForwarderComment.Add(this.lblForwarderCommentTitle);
			global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.hboxForwarderComment[this.lblForwarderCommentTitle]));
			w27.Position = 0;
			w27.Expand = false;
			w27.Fill = false;
			// Container child hboxForwarderComment.Gtk.Box+BoxChild
			this.lblForwarderComment = new global::Gamma.GtkWidgets.yLabel();
			this.lblForwarderComment.Name = "lblForwarderComment";
			this.lblForwarderComment.LabelProp = global::Mono.Unix.Catalog.GetString("\"Комментарий по экспедитору\"");
			this.hboxForwarderComment.Add(this.lblForwarderComment);
			global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.hboxForwarderComment[this.lblForwarderComment]));
			w28.Position = 1;
			w28.Expand = false;
			w28.Fill = false;
			this.dataRouteList.Add(this.hboxForwarderComment);
			global::Gtk.Table.TableChild w29 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.hboxForwarderComment]));
			w29.TopAttach = ((uint)(6));
			w29.BottomAttach = ((uint)(7));
			w29.LeftAttach = ((uint)(3));
			w29.RightAttach = ((uint)(6));
			w29.XOptions = ((global::Gtk.AttachOptions)(0));
			w29.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.hseparator2 = new global::Gtk.HSeparator();
			this.hseparator2.Name = "hseparator2";
			this.dataRouteList.Add(this.hseparator2);
			global::Gtk.Table.TableChild w30 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.hseparator2]));
			w30.TopAttach = ((uint)(1));
			w30.BottomAttach = ((uint)(2));
			w30.RightAttach = ((uint)(6));
			w30.XOptions = ((global::Gtk.AttachOptions)(4));
			w30.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.hseparator3 = new global::Gtk.HSeparator();
			this.hseparator3.Name = "hseparator3";
			this.dataRouteList.Add(this.hseparator3);
			global::Gtk.Table.TableChild w31 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.hseparator3]));
			w31.TopAttach = ((uint)(7));
			w31.BottomAttach = ((uint)(8));
			w31.RightAttach = ((uint)(7));
			w31.XOptions = ((global::Gtk.AttachOptions)(4));
			w31.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.Xalign = 1F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Смена:");
			this.dataRouteList.Add(this.label1);
			global::Gtk.Table.TableChild w32 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.label1]));
			w32.TopAttach = ((uint)(3));
			w32.BottomAttach = ((uint)(4));
			w32.LeftAttach = ((uint)(2));
			w32.RightAttach = ((uint)(3));
			w32.XOptions = ((global::Gtk.AttachOptions)(4));
			w32.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.label10 = new global::Gtk.Label();
			this.label10.Name = "label10";
			this.label10.Xalign = 1F;
			this.label10.LabelProp = global::Mono.Unix.Catalog.GetString("Логист:");
			this.dataRouteList.Add(this.label10);
			global::Gtk.Table.TableChild w33 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.label10]));
			w33.LeftAttach = ((uint)(4));
			w33.RightAttach = ((uint)(5));
			w33.XOptions = ((global::Gtk.AttachOptions)(4));
			w33.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.label3.Xalign = 1F;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Водитель:");
			this.dataRouteList.Add(this.label3);
			global::Gtk.Table.TableChild w34 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.label3]));
			w34.TopAttach = ((uint)(3));
			w34.BottomAttach = ((uint)(4));
			w34.LeftAttach = ((uint)(4));
			w34.RightAttach = ((uint)(5));
			w34.XOptions = ((global::Gtk.AttachOptions)(4));
			w34.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label();
			this.label4.Name = "label4";
			this.label4.Xalign = 1F;
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString("Машина:");
			this.dataRouteList.Add(this.label4);
			global::Gtk.Table.TableChild w35 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.label4]));
			w35.TopAttach = ((uint)(2));
			w35.BottomAttach = ((uint)(3));
			w35.LeftAttach = ((uint)(4));
			w35.RightAttach = ((uint)(5));
			w35.XOptions = ((global::Gtk.AttachOptions)(4));
			w35.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.label5 = new global::Gtk.Label();
			this.label5.Name = "label5";
			this.label5.Xalign = 1F;
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString("Состояние:");
			this.dataRouteList.Add(this.label5);
			global::Gtk.Table.TableChild w36 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.label5]));
			w36.TopAttach = ((uint)(2));
			w36.BottomAttach = ((uint)(3));
			w36.XOptions = ((global::Gtk.AttachOptions)(4));
			w36.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.label6 = new global::Gtk.Label();
			this.label6.Name = "label6";
			this.label6.Xalign = 1F;
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString("Фиксированная стоимость доставки:");
			this.dataRouteList.Add(this.label6);
			global::Gtk.Table.TableChild w37 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.label6]));
			w37.TopAttach = ((uint)(2));
			w37.BottomAttach = ((uint)(3));
			w37.LeftAttach = ((uint)(2));
			w37.RightAttach = ((uint)(3));
			w37.XOptions = ((global::Gtk.AttachOptions)(4));
			w37.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.label7 = new global::Gtk.Label();
			this.label7.Name = "label7";
			this.label7.Xalign = 1F;
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString("Дата:");
			this.dataRouteList.Add(this.label7);
			global::Gtk.Table.TableChild w38 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.label7]));
			w38.TopAttach = ((uint)(3));
			w38.BottomAttach = ((uint)(4));
			w38.XOptions = ((global::Gtk.AttachOptions)(4));
			w38.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.label9 = new global::Gtk.Label();
			this.label9.Name = "label9";
			this.label9.Xalign = 1F;
			this.label9.LabelProp = global::Mono.Unix.Catalog.GetString("Экспедитор:");
			this.dataRouteList.Add(this.label9);
			global::Gtk.Table.TableChild w39 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.label9]));
			w39.TopAttach = ((uint)(5));
			w39.BottomAttach = ((uint)(6));
			w39.LeftAttach = ((uint)(4));
			w39.RightAttach = ((uint)(5));
			w39.XOptions = ((global::Gtk.AttachOptions)(4));
			w39.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.labelStatus = new global::Gamma.GtkWidgets.yLabel();
			this.labelStatus.Name = "labelStatus";
			this.labelStatus.Xalign = 0F;
			this.labelStatus.LabelProp = global::Mono.Unix.Catalog.GetString("--");
			this.dataRouteList.Add(this.labelStatus);
			global::Gtk.Table.TableChild w40 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.labelStatus]));
			w40.TopAttach = ((uint)(2));
			w40.BottomAttach = ((uint)(3));
			w40.LeftAttach = ((uint)(1));
			w40.RightAttach = ((uint)(2));
			w40.XOptions = ((global::Gtk.AttachOptions)(4));
			w40.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.phoneDriver = new global::Vodovoz.ViewWidgets.Mango.EmployeePhone();
			this.phoneDriver.CanFocus = true;
			this.phoneDriver.Name = "phoneDriver";
			this.phoneDriver.UseUnderline = true;
			this.phoneDriver.UseMarkup = false;
			this.phoneDriver.LabelXAlign = 0F;
			this.dataRouteList.Add(this.phoneDriver);
			global::Gtk.Table.TableChild w41 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.phoneDriver]));
			w41.TopAttach = ((uint)(3));
			w41.BottomAttach = ((uint)(4));
			w41.LeftAttach = ((uint)(6));
			w41.RightAttach = ((uint)(7));
			w41.XOptions = ((global::Gtk.AttachOptions)(4));
			w41.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.phoneForwarder = new global::Vodovoz.ViewWidgets.Mango.EmployeePhone();
			this.phoneForwarder.CanFocus = true;
			this.phoneForwarder.Name = "phoneForwarder";
			this.phoneForwarder.UseUnderline = true;
			this.phoneForwarder.UseMarkup = false;
			this.phoneForwarder.LabelXAlign = 0F;
			this.dataRouteList.Add(this.phoneForwarder);
			global::Gtk.Table.TableChild w42 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.phoneForwarder]));
			w42.TopAttach = ((uint)(5));
			w42.BottomAttach = ((uint)(6));
			w42.LeftAttach = ((uint)(6));
			w42.RightAttach = ((uint)(7));
			w42.XOptions = ((global::Gtk.AttachOptions)(4));
			w42.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.phoneLogistican = new global::Vodovoz.ViewWidgets.Mango.EmployeePhone();
			this.phoneLogistican.CanFocus = true;
			this.phoneLogistican.Name = "phoneLogistican";
			this.phoneLogistican.UseUnderline = true;
			this.phoneLogistican.UseMarkup = false;
			this.phoneLogistican.LabelXAlign = 0F;
			this.dataRouteList.Add(this.phoneLogistican);
			global::Gtk.Table.TableChild w43 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.phoneLogistican]));
			w43.LeftAttach = ((uint)(6));
			w43.RightAttach = ((uint)(7));
			w43.XOptions = ((global::Gtk.AttachOptions)(4));
			w43.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child dataRouteList.Gtk.Table+TableChild
			this.speccomboShift = new global::Gamma.Widgets.ySpecComboBox();
			this.speccomboShift.Name = "speccomboShift";
			this.speccomboShift.AddIfNotExist = false;
			this.speccomboShift.DefaultFirst = false;
			this.speccomboShift.ShowSpecialStateAll = false;
			this.speccomboShift.ShowSpecialStateNot = true;
			this.dataRouteList.Add(this.speccomboShift);
			global::Gtk.Table.TableChild w44 = ((global::Gtk.Table.TableChild)(this.dataRouteList[this.speccomboShift]));
			w44.TopAttach = ((uint)(3));
			w44.BottomAttach = ((uint)(4));
			w44.LeftAttach = ((uint)(3));
			w44.RightAttach = ((uint)(4));
			w44.XOptions = ((global::Gtk.AttachOptions)(4));
			w44.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox1.Add(this.dataRouteList);
			global::Gtk.Box.BoxChild w45 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.dataRouteList]));
			w45.Position = 0;
			w45.Expand = false;
			w45.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox8 = new global::Gtk.HBox();
			this.hbox8.Name = "hbox8";
			this.hbox8.Spacing = 6;
			// Container child hbox8.Gtk.Box+BoxChild
			this.createroutelistitemsview1 = new global::Vodovoz.RouteListCreateItemsView();
			this.createroutelistitemsview1.HeightRequest = 150;
			this.createroutelistitemsview1.Events = ((global::Gdk.EventMask)(256));
			this.createroutelistitemsview1.Name = "createroutelistitemsview1";
			this.createroutelistitemsview1.DisableColumnsUpdate = false;
			this.hbox8.Add(this.createroutelistitemsview1);
			global::Gtk.Box.BoxChild w46 = ((global::Gtk.Box.BoxChild)(this.hbox8[this.createroutelistitemsview1]));
			w46.Position = 0;
			// Container child hbox8.Gtk.Box+BoxChild
			this.vseparator2 = new global::Gtk.VSeparator();
			this.vseparator2.Name = "vseparator2";
			this.hbox8.Add(this.vseparator2);
			global::Gtk.Box.BoxChild w47 = ((global::Gtk.Box.BoxChild)(this.hbox8[this.vseparator2]));
			w47.Position = 1;
			w47.Expand = false;
			w47.Fill = false;
			// Container child hbox8.Gtk.Box+BoxChild
			this.hboxAdditionalLoading = new global::Gtk.HBox();
			this.hboxAdditionalLoading.Name = "hboxAdditionalLoading";
			this.hboxAdditionalLoading.Spacing = 6;
			this.hbox8.Add(this.hboxAdditionalLoading);
			global::Gtk.Box.BoxChild w48 = ((global::Gtk.Box.BoxChild)(this.hbox8[this.hboxAdditionalLoading]));
			w48.Position = 2;
			w48.Expand = false;
			w48.Fill = false;
			this.vbox1.Add(this.hbox8);
			global::Gtk.Box.BoxChild w49 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox8]));
			w49.Position = 1;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox11 = new global::Gtk.HBox();
			this.hbox11.Name = "hbox11";
			this.hbox11.Spacing = 6;
			// Container child hbox11.Gtk.Box+BoxChild
			this.printTimeButton = new global::Gamma.GtkWidgets.yButton();
			this.printTimeButton.CanFocus = true;
			this.printTimeButton.Name = "printTimeButton";
			this.printTimeButton.UseUnderline = true;
			this.printTimeButton.Label = global::Mono.Unix.Catalog.GetString("Время печати МЛ");
			this.hbox11.Add(this.printTimeButton);
			global::Gtk.Box.BoxChild w50 = ((global::Gtk.Box.BoxChild)(this.hbox11[this.printTimeButton]));
			w50.Position = 0;
			w50.Expand = false;
			w50.Fill = false;
			// Container child hbox11.Gtk.Box+BoxChild
			this.enumPrint = new global::QS.Widgets.EnumMenuButton();
			this.enumPrint.CanFocus = true;
			this.enumPrint.Name = "enumPrint";
			this.enumPrint.UseUnderline = true;
			this.enumPrint.UseMarkup = false;
			this.enumPrint.LabelXAlign = 0F;
			this.enumPrint.Label = global::Mono.Unix.Catalog.GetString("Распечатать");
			global::Gtk.Image w51 = new global::Gtk.Image();
			w51.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-print", global::Gtk.IconSize.Menu);
			this.enumPrint.Image = w51;
			this.hbox11.Add(this.enumPrint);
			global::Gtk.Box.BoxChild w52 = ((global::Gtk.Box.BoxChild)(this.hbox11[this.enumPrint]));
			w52.PackType = ((global::Gtk.PackType)(1));
			w52.Position = 2;
			w52.Expand = false;
			w52.Fill = false;
			// Container child hbox11.Gtk.Box+BoxChild
			this.buttonAccept = new global::Gtk.Button();
			this.buttonAccept.CanFocus = true;
			this.buttonAccept.Name = "buttonAccept";
			this.buttonAccept.UseUnderline = true;
			this.buttonAccept.Label = global::Mono.Unix.Catalog.GetString("Подтвердить");
			global::Gtk.Image w53 = new global::Gtk.Image();
			w53.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-apply", global::Gtk.IconSize.Menu);
			this.buttonAccept.Image = w53;
			this.hbox11.Add(this.buttonAccept);
			global::Gtk.Box.BoxChild w54 = ((global::Gtk.Box.BoxChild)(this.hbox11[this.buttonAccept]));
			w54.PackType = ((global::Gtk.PackType)(1));
			w54.Position = 3;
			w54.Expand = false;
			w54.Fill = false;
			// Container child hbox11.Gtk.Box+BoxChild
			this.ybuttonAddAdditionalLoad = new global::Gamma.GtkWidgets.yButton();
			this.ybuttonAddAdditionalLoad.CanFocus = true;
			this.ybuttonAddAdditionalLoad.Name = "ybuttonAddAdditionalLoad";
			this.ybuttonAddAdditionalLoad.UseUnderline = true;
			this.ybuttonAddAdditionalLoad.Label = global::Mono.Unix.Catalog.GetString("Добавить запас");
			global::Gtk.Image w55 = new global::Gtk.Image();
			w55.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-add", global::Gtk.IconSize.Menu);
			this.ybuttonAddAdditionalLoad.Image = w55;
			this.hbox11.Add(this.ybuttonAddAdditionalLoad);
			global::Gtk.Box.BoxChild w56 = ((global::Gtk.Box.BoxChild)(this.hbox11[this.ybuttonAddAdditionalLoad]));
			w56.PackType = ((global::Gtk.PackType)(1));
			w56.Position = 4;
			w56.Expand = false;
			w56.Fill = false;
			// Container child hbox11.Gtk.Box+BoxChild
			this.ybuttonRemoveAdditionalLoad = new global::Gamma.GtkWidgets.yButton();
			this.ybuttonRemoveAdditionalLoad.CanFocus = true;
			this.ybuttonRemoveAdditionalLoad.Name = "ybuttonRemoveAdditionalLoad";
			this.ybuttonRemoveAdditionalLoad.UseUnderline = true;
			this.ybuttonRemoveAdditionalLoad.Label = global::Mono.Unix.Catalog.GetString("Удалить запас");
			global::Gtk.Image w57 = new global::Gtk.Image();
			w57.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-delete", global::Gtk.IconSize.Menu);
			this.ybuttonRemoveAdditionalLoad.Image = w57;
			this.hbox11.Add(this.ybuttonRemoveAdditionalLoad);
			global::Gtk.Box.BoxChild w58 = ((global::Gtk.Box.BoxChild)(this.hbox11[this.ybuttonRemoveAdditionalLoad]));
			w58.PackType = ((global::Gtk.PackType)(1));
			w58.Position = 5;
			w58.Expand = false;
			w58.Fill = false;
			this.vbox1.Add(this.hbox11);
			global::Gtk.Box.BoxChild w59 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox11]));
			w59.Position = 2;
			w59.Expand = false;
			w59.Fill = false;
			this.ynotebook1.Add(this.vbox1);
			// Notebook tab
			this.lblInformation = new global::Gtk.Label();
			this.lblInformation.Name = "lblInformation";
			this.lblInformation.LabelProp = global::Mono.Unix.Catalog.GetString("Информация");
			this.ynotebook1.SetTabLabel(this.vbox1, this.lblInformation);
			this.lblInformation.ShowAll();
			// Container child ynotebook1.Gtk.Notebook+NotebookChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.treeRouteListProfitability = new global::Gamma.GtkWidgets.yTreeView();
			this.treeRouteListProfitability.CanFocus = true;
			this.treeRouteListProfitability.Name = "treeRouteListProfitability";
			this.GtkScrolledWindow1.Add(this.treeRouteListProfitability);
			this.ynotebook1.Add(this.GtkScrolledWindow1);
			global::Gtk.Notebook.NotebookChild w62 = ((global::Gtk.Notebook.NotebookChild)(this.ynotebook1[this.GtkScrolledWindow1]));
			w62.Position = 1;
			// Notebook tab
			this.lblProfitability = new global::Gtk.Label();
			this.lblProfitability.Name = "lblProfitability";
			this.lblProfitability.LabelProp = global::Mono.Unix.Catalog.GetString("Рентабельность");
			this.ynotebook1.SetTabLabel(this.GtkScrolledWindow1, this.lblProfitability);
			this.lblProfitability.ShowAll();
			this.vboxMain.Add(this.ynotebook1);
			global::Gtk.Box.BoxChild w63 = ((global::Gtk.Box.BoxChild)(this.vboxMain[this.ynotebook1]));
			w63.Position = 1;
			this.Add(this.vboxMain);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.buttonAccept.Clicked += new global::System.EventHandler(this.OnButtonAcceptClicked);
		}
	}
}
