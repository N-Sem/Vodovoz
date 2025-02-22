
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Views.Logistic
{
	public partial class CarEventView
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.HBox hbox1;

		private global::Gtk.Button buttonSave;

		private global::Gtk.Button buttonCancel;

		private global::Gtk.Table table1;

		private global::Gamma.GtkWidgets.yCheckButton checkbuttonDoNotShowInOperation;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entityviewmodelentryCar;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entityviewmodelentryCarEventType;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry evmeDriver;

		private global::Gtk.ScrolledWindow GtkScrolledWindowComment;

		private global::Gamma.GtkWidgets.yTextView ytextviewCommnet;

		private global::Gtk.ScrolledWindow GtkScrolledWindowFoundation;

		private global::Gamma.GtkWidgets.yTextView ytextviewFoundation;

		private global::Gtk.Label labelAuthor;

		private global::Gtk.Label labelCar;

		private global::Gtk.Label labelCarEventType;

		private global::Gtk.Label labelComment;

		private global::Gtk.Label labelComment1;

		private global::Gtk.Label labelComment2;

		private global::Gtk.Label labelComment3;

		private global::Gtk.Label labelCreateDate;

		private global::Gtk.Label labelDateEventEnd;

		private global::Gtk.Label labelDateEventStart;

		private global::Gtk.Label labelDriver;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry originalCarEvent;

		private global::Gamma.Widgets.yDatePicker ydatepickerEndEventDate;

		private global::Gamma.Widgets.yDatePicker ydatepickerStartEventDate;

		private global::Gamma.GtkWidgets.yLabel ylabelAuthor;

		private global::Gamma.GtkWidgets.yLabel ylabelCreateDate;

		private global::Gamma.GtkWidgets.yLabel ylabelOriginalCarEvent;

		private global::Gamma.GtkWidgets.ySpinButton yspinPaymentTotalCarEvent;

		private global::Gtk.Label labelComment4;

		private global::Gtk.ScrolledWindow GtkScrolledWindow5;

		private global::Gamma.GtkWidgets.yTreeView ytreeviewFines;

		private global::Gtk.HBox hboxFineButtons;

		private global::Gamma.GtkWidgets.yButton buttonAttachFine;

		private global::Gamma.GtkWidgets.yButton buttonAddFine;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Views.Logistic.CarEventView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Views.Logistic.CarEventView";
			// Container child Vodovoz.Views.Logistic.CarEventView.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			this.vbox1.BorderWidth = ((uint)(6));
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonSave = new global::Gtk.Button();
			this.buttonSave.CanFocus = true;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.UseUnderline = true;
			this.buttonSave.Label = global::Mono.Unix.Catalog.GetString("Сохранить");
			global::Gtk.Image w1 = new global::Gtk.Image();
			w1.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-floppy", global::Gtk.IconSize.Menu);
			this.buttonSave.Image = w1;
			this.hbox1.Add(this.buttonSave);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.buttonSave]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString("Отменить");
			global::Gtk.Image w3 = new global::Gtk.Image();
			w3.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-close", global::Gtk.IconSize.Menu);
			this.buttonCancel.Image = w3;
			this.hbox1.Add(this.buttonCancel);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.buttonCancel]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			this.vbox1.Add(this.hbox1);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table(((uint)(12)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.checkbuttonDoNotShowInOperation = new global::Gamma.GtkWidgets.yCheckButton();
			this.checkbuttonDoNotShowInOperation.CanFocus = true;
			this.checkbuttonDoNotShowInOperation.Name = "checkbuttonDoNotShowInOperation";
			this.checkbuttonDoNotShowInOperation.Label = "";
			this.checkbuttonDoNotShowInOperation.DrawIndicator = true;
			this.checkbuttonDoNotShowInOperation.UseUnderline = true;
			this.table1.Add(this.checkbuttonDoNotShowInOperation);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.checkbuttonDoNotShowInOperation]));
			w6.TopAttach = ((uint)(9));
			w6.BottomAttach = ((uint)(10));
			w6.LeftAttach = ((uint)(1));
			w6.RightAttach = ((uint)(2));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entityviewmodelentryCar = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entityviewmodelentryCar.Events = ((global::Gdk.EventMask)(256));
			this.entityviewmodelentryCar.Name = "entityviewmodelentryCar";
			this.entityviewmodelentryCar.CanEditReference = true;
			this.entityviewmodelentryCar.CanOpenWithoutTabParent = false;
			this.table1.Add(this.entityviewmodelentryCar);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1[this.entityviewmodelentryCar]));
			w7.TopAttach = ((uint)(2));
			w7.BottomAttach = ((uint)(3));
			w7.LeftAttach = ((uint)(1));
			w7.RightAttach = ((uint)(2));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entityviewmodelentryCarEventType = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entityviewmodelentryCarEventType.Events = ((global::Gdk.EventMask)(256));
			this.entityviewmodelentryCarEventType.Name = "entityviewmodelentryCarEventType";
			this.entityviewmodelentryCarEventType.CanEditReference = true;
			this.entityviewmodelentryCarEventType.CanOpenWithoutTabParent = false;
			this.table1.Add(this.entityviewmodelentryCarEventType);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1[this.entityviewmodelentryCarEventType]));
			w8.TopAttach = ((uint)(4));
			w8.BottomAttach = ((uint)(5));
			w8.LeftAttach = ((uint)(1));
			w8.RightAttach = ((uint)(2));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.evmeDriver = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.evmeDriver.Events = ((global::Gdk.EventMask)(256));
			this.evmeDriver.Name = "evmeDriver";
			this.evmeDriver.CanEditReference = true;
			this.evmeDriver.CanOpenWithoutTabParent = false;
			this.table1.Add(this.evmeDriver);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1[this.evmeDriver]));
			w9.TopAttach = ((uint)(3));
			w9.BottomAttach = ((uint)(4));
			w9.LeftAttach = ((uint)(1));
			w9.RightAttach = ((uint)(2));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.GtkScrolledWindowComment = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindowComment.Name = "GtkScrolledWindowComment";
			this.GtkScrolledWindowComment.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindowComment.Gtk.Container+ContainerChild
			this.ytextviewCommnet = new global::Gamma.GtkWidgets.yTextView();
			this.ytextviewCommnet.WidthRequest = 250;
			this.ytextviewCommnet.CanFocus = true;
			this.ytextviewCommnet.Name = "ytextviewCommnet";
			this.GtkScrolledWindowComment.Add(this.ytextviewCommnet);
			this.table1.Add(this.GtkScrolledWindowComment);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1[this.GtkScrolledWindowComment]));
			w11.TopAttach = ((uint)(11));
			w11.BottomAttach = ((uint)(12));
			w11.LeftAttach = ((uint)(1));
			w11.RightAttach = ((uint)(2));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.GtkScrolledWindowFoundation = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindowFoundation.Name = "GtkScrolledWindowFoundation";
			this.GtkScrolledWindowFoundation.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindowFoundation.Gtk.Container+ContainerChild
			this.ytextviewFoundation = new global::Gamma.GtkWidgets.yTextView();
			this.ytextviewFoundation.WidthRequest = 250;
			this.ytextviewFoundation.CanFocus = true;
			this.ytextviewFoundation.Name = "ytextviewFoundation";
			this.GtkScrolledWindowFoundation.Add(this.ytextviewFoundation);
			this.table1.Add(this.GtkScrolledWindowFoundation);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.table1[this.GtkScrolledWindowFoundation]));
			w13.TopAttach = ((uint)(10));
			w13.BottomAttach = ((uint)(11));
			w13.LeftAttach = ((uint)(1));
			w13.RightAttach = ((uint)(2));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelAuthor = new global::Gtk.Label();
			this.labelAuthor.Name = "labelAuthor";
			this.labelAuthor.Xalign = 1F;
			this.labelAuthor.LabelProp = global::Mono.Unix.Catalog.GetString("Автор события:");
			this.table1.Add(this.labelAuthor);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.table1[this.labelAuthor]));
			w14.TopAttach = ((uint)(1));
			w14.BottomAttach = ((uint)(2));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelCar = new global::Gtk.Label();
			this.labelCar.Name = "labelCar";
			this.labelCar.Xalign = 1F;
			this.labelCar.LabelProp = global::Mono.Unix.Catalog.GetString("Автомобиль:");
			this.table1.Add(this.labelCar);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.table1[this.labelCar]));
			w15.TopAttach = ((uint)(2));
			w15.BottomAttach = ((uint)(3));
			w15.XOptions = ((global::Gtk.AttachOptions)(4));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelCarEventType = new global::Gtk.Label();
			this.labelCarEventType.Name = "labelCarEventType";
			this.labelCarEventType.Xalign = 1F;
			this.labelCarEventType.LabelProp = global::Mono.Unix.Catalog.GetString("Вид события ТС:");
			this.table1.Add(this.labelCarEventType);
			global::Gtk.Table.TableChild w16 = ((global::Gtk.Table.TableChild)(this.table1[this.labelCarEventType]));
			w16.TopAttach = ((uint)(4));
			w16.BottomAttach = ((uint)(5));
			w16.XOptions = ((global::Gtk.AttachOptions)(4));
			w16.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelComment = new global::Gtk.Label();
			this.labelComment.Name = "labelComment";
			this.labelComment.Xalign = 1F;
			this.labelComment.Yalign = 0F;
			this.labelComment.LabelProp = global::Mono.Unix.Catalog.GetString("Комментарий:");
			this.table1.Add(this.labelComment);
			global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.table1[this.labelComment]));
			w17.TopAttach = ((uint)(11));
			w17.BottomAttach = ((uint)(12));
			w17.XOptions = ((global::Gtk.AttachOptions)(4));
			w17.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelComment1 = new global::Gtk.Label();
			this.labelComment1.Name = "labelComment1";
			this.labelComment1.Xalign = 1F;
			this.labelComment1.Yalign = 0F;
			this.labelComment1.LabelProp = global::Mono.Unix.Catalog.GetString("Основание:");
			this.table1.Add(this.labelComment1);
			global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.table1[this.labelComment1]));
			w18.TopAttach = ((uint)(10));
			w18.BottomAttach = ((uint)(11));
			w18.XOptions = ((global::Gtk.AttachOptions)(4));
			w18.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelComment2 = new global::Gtk.Label();
			this.labelComment2.Name = "labelComment2";
			this.labelComment2.Xalign = 1F;
			this.labelComment2.Yalign = 0F;
			this.labelComment2.LabelProp = global::Mono.Unix.Catalog.GetString("Стоимость:");
			this.table1.Add(this.labelComment2);
			global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.table1[this.labelComment2]));
			w19.TopAttach = ((uint)(8));
			w19.BottomAttach = ((uint)(9));
			w19.XOptions = ((global::Gtk.AttachOptions)(4));
			w19.YOptions = ((global::Gtk.AttachOptions)(0));
			// Container child table1.Gtk.Table+TableChild
			this.labelComment3 = new global::Gtk.Label();
			this.labelComment3.Name = "labelComment3";
			this.labelComment3.Xalign = 1F;
			this.labelComment3.Yalign = 0F;
			this.labelComment3.LabelProp = global::Mono.Unix.Catalog.GetString("Не отражать в эксплуатации ТС");
			this.table1.Add(this.labelComment3);
			global::Gtk.Table.TableChild w20 = ((global::Gtk.Table.TableChild)(this.table1[this.labelComment3]));
			w20.TopAttach = ((uint)(9));
			w20.BottomAttach = ((uint)(10));
			w20.XOptions = ((global::Gtk.AttachOptions)(4));
			w20.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelCreateDate = new global::Gtk.Label();
			this.labelCreateDate.Name = "labelCreateDate";
			this.labelCreateDate.Xalign = 1F;
			this.labelCreateDate.LabelProp = global::Mono.Unix.Catalog.GetString("Дата создания:");
			this.table1.Add(this.labelCreateDate);
			global::Gtk.Table.TableChild w21 = ((global::Gtk.Table.TableChild)(this.table1[this.labelCreateDate]));
			w21.XOptions = ((global::Gtk.AttachOptions)(4));
			w21.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelDateEventEnd = new global::Gtk.Label();
			this.labelDateEventEnd.Name = "labelDateEventEnd";
			this.labelDateEventEnd.Xalign = 1F;
			this.labelDateEventEnd.LabelProp = global::Mono.Unix.Catalog.GetString("Дата окончания события:");
			this.table1.Add(this.labelDateEventEnd);
			global::Gtk.Table.TableChild w22 = ((global::Gtk.Table.TableChild)(this.table1[this.labelDateEventEnd]));
			w22.TopAttach = ((uint)(6));
			w22.BottomAttach = ((uint)(7));
			w22.XOptions = ((global::Gtk.AttachOptions)(4));
			w22.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelDateEventStart = new global::Gtk.Label();
			this.labelDateEventStart.Name = "labelDateEventStart";
			this.labelDateEventStart.Xalign = 1F;
			this.labelDateEventStart.LabelProp = global::Mono.Unix.Catalog.GetString("Дата начала события:");
			this.table1.Add(this.labelDateEventStart);
			global::Gtk.Table.TableChild w23 = ((global::Gtk.Table.TableChild)(this.table1[this.labelDateEventStart]));
			w23.TopAttach = ((uint)(5));
			w23.BottomAttach = ((uint)(6));
			w23.XOptions = ((global::Gtk.AttachOptions)(4));
			w23.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelDriver = new global::Gtk.Label();
			this.labelDriver.Name = "labelDriver";
			this.labelDriver.Xalign = 1F;
			this.labelDriver.LabelProp = global::Mono.Unix.Catalog.GetString("Водитель:");
			this.table1.Add(this.labelDriver);
			global::Gtk.Table.TableChild w24 = ((global::Gtk.Table.TableChild)(this.table1[this.labelDriver]));
			w24.TopAttach = ((uint)(3));
			w24.BottomAttach = ((uint)(4));
			w24.XOptions = ((global::Gtk.AttachOptions)(4));
			w24.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.originalCarEvent = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.originalCarEvent.Events = ((global::Gdk.EventMask)(256));
			this.originalCarEvent.Name = "originalCarEvent";
			this.originalCarEvent.CanEditReference = true;
			this.originalCarEvent.CanOpenWithoutTabParent = false;
			this.table1.Add(this.originalCarEvent);
			global::Gtk.Table.TableChild w25 = ((global::Gtk.Table.TableChild)(this.table1[this.originalCarEvent]));
			w25.TopAttach = ((uint)(7));
			w25.BottomAttach = ((uint)(8));
			w25.LeftAttach = ((uint)(1));
			w25.RightAttach = ((uint)(2));
			w25.XOptions = ((global::Gtk.AttachOptions)(4));
			w25.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ydatepickerEndEventDate = new global::Gamma.Widgets.yDatePicker();
			this.ydatepickerEndEventDate.Events = ((global::Gdk.EventMask)(256));
			this.ydatepickerEndEventDate.Name = "ydatepickerEndEventDate";
			this.ydatepickerEndEventDate.WithTime = false;
			this.ydatepickerEndEventDate.Date = new global::System.DateTime(0);
			this.ydatepickerEndEventDate.IsEditable = true;
			this.ydatepickerEndEventDate.AutoSeparation = false;
			this.table1.Add(this.ydatepickerEndEventDate);
			global::Gtk.Table.TableChild w26 = ((global::Gtk.Table.TableChild)(this.table1[this.ydatepickerEndEventDate]));
			w26.TopAttach = ((uint)(6));
			w26.BottomAttach = ((uint)(7));
			w26.LeftAttach = ((uint)(1));
			w26.RightAttach = ((uint)(2));
			w26.XOptions = ((global::Gtk.AttachOptions)(4));
			w26.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ydatepickerStartEventDate = new global::Gamma.Widgets.yDatePicker();
			this.ydatepickerStartEventDate.Events = ((global::Gdk.EventMask)(256));
			this.ydatepickerStartEventDate.Name = "ydatepickerStartEventDate";
			this.ydatepickerStartEventDate.WithTime = false;
			this.ydatepickerStartEventDate.Date = new global::System.DateTime(0);
			this.ydatepickerStartEventDate.IsEditable = true;
			this.ydatepickerStartEventDate.AutoSeparation = false;
			this.table1.Add(this.ydatepickerStartEventDate);
			global::Gtk.Table.TableChild w27 = ((global::Gtk.Table.TableChild)(this.table1[this.ydatepickerStartEventDate]));
			w27.TopAttach = ((uint)(5));
			w27.BottomAttach = ((uint)(6));
			w27.LeftAttach = ((uint)(1));
			w27.RightAttach = ((uint)(2));
			w27.XOptions = ((global::Gtk.AttachOptions)(4));
			w27.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabelAuthor = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelAuthor.Name = "ylabelAuthor";
			this.ylabelAuthor.Xalign = 0F;
			this.ylabelAuthor.LabelProp = global::Mono.Unix.Catalog.GetString("ylabelAuthor");
			this.table1.Add(this.ylabelAuthor);
			global::Gtk.Table.TableChild w28 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabelAuthor]));
			w28.TopAttach = ((uint)(1));
			w28.BottomAttach = ((uint)(2));
			w28.LeftAttach = ((uint)(1));
			w28.RightAttach = ((uint)(2));
			w28.XOptions = ((global::Gtk.AttachOptions)(4));
			w28.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabelCreateDate = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelCreateDate.Name = "ylabelCreateDate";
			this.ylabelCreateDate.Xalign = 0F;
			this.ylabelCreateDate.LabelProp = global::Mono.Unix.Catalog.GetString("ylabelCreateDate");
			this.table1.Add(this.ylabelCreateDate);
			global::Gtk.Table.TableChild w29 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabelCreateDate]));
			w29.LeftAttach = ((uint)(1));
			w29.RightAttach = ((uint)(2));
			w29.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabelOriginalCarEvent = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelOriginalCarEvent.Name = "ylabelOriginalCarEvent";
			this.ylabelOriginalCarEvent.Xalign = 1F;
			this.ylabelOriginalCarEvent.Yalign = 0F;
			this.ylabelOriginalCarEvent.LabelProp = global::Mono.Unix.Catalog.GetString("Исходное ремонтное событие:");
			this.table1.Add(this.ylabelOriginalCarEvent);
			global::Gtk.Table.TableChild w30 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabelOriginalCarEvent]));
			w30.TopAttach = ((uint)(7));
			w30.BottomAttach = ((uint)(8));
			w30.XOptions = ((global::Gtk.AttachOptions)(4));
			w30.YOptions = ((global::Gtk.AttachOptions)(0));
			// Container child table1.Gtk.Table+TableChild
			this.yspinPaymentTotalCarEvent = new global::Gamma.GtkWidgets.ySpinButton(0D, 1000000D, 1D);
			this.yspinPaymentTotalCarEvent.CanFocus = true;
			this.yspinPaymentTotalCarEvent.Name = "yspinPaymentTotalCarEvent";
			this.yspinPaymentTotalCarEvent.Adjustment.PageIncrement = 100D;
			this.yspinPaymentTotalCarEvent.ClimbRate = 1D;
			this.yspinPaymentTotalCarEvent.Digits = ((uint)(2));
			this.yspinPaymentTotalCarEvent.Numeric = true;
			this.yspinPaymentTotalCarEvent.ValueAsDecimal = 0m;
			this.yspinPaymentTotalCarEvent.ValueAsInt = 0;
			this.table1.Add(this.yspinPaymentTotalCarEvent);
			global::Gtk.Table.TableChild w31 = ((global::Gtk.Table.TableChild)(this.table1[this.yspinPaymentTotalCarEvent]));
			w31.TopAttach = ((uint)(8));
			w31.BottomAttach = ((uint)(9));
			w31.LeftAttach = ((uint)(1));
			w31.RightAttach = ((uint)(2));
			w31.XOptions = ((global::Gtk.AttachOptions)(4));
			w31.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox1.Add(this.table1);
			global::Gtk.Box.BoxChild w32 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.table1]));
			w32.Position = 1;
			w32.Expand = false;
			w32.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.labelComment4 = new global::Gtk.Label();
			this.labelComment4.Name = "labelComment4";
			this.labelComment4.Xalign = 0F;
			this.labelComment4.Yalign = 0F;
			this.labelComment4.LabelProp = global::Mono.Unix.Catalog.GetString("Возмещение затрат за счет штрафов:");
			this.vbox1.Add(this.labelComment4);
			global::Gtk.Box.BoxChild w33 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.labelComment4]));
			w33.Position = 2;
			w33.Expand = false;
			w33.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow5 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow5.Name = "GtkScrolledWindow5";
			this.GtkScrolledWindow5.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow5.Gtk.Container+ContainerChild
			this.ytreeviewFines = new global::Gamma.GtkWidgets.yTreeView();
			this.ytreeviewFines.CanFocus = true;
			this.ytreeviewFines.Name = "ytreeviewFines";
			this.GtkScrolledWindow5.Add(this.ytreeviewFines);
			this.vbox1.Add(this.GtkScrolledWindow5);
			global::Gtk.Box.BoxChild w35 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow5]));
			w35.Position = 3;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hboxFineButtons = new global::Gtk.HBox();
			this.hboxFineButtons.Name = "hboxFineButtons";
			this.hboxFineButtons.Spacing = 6;
			// Container child hboxFineButtons.Gtk.Box+BoxChild
			this.buttonAttachFine = new global::Gamma.GtkWidgets.yButton();
			this.buttonAttachFine.CanFocus = true;
			this.buttonAttachFine.Name = "buttonAttachFine";
			this.buttonAttachFine.UseUnderline = true;
			this.buttonAttachFine.Label = global::Mono.Unix.Catalog.GetString("Прикрепить штраф");
			this.hboxFineButtons.Add(this.buttonAttachFine);
			global::Gtk.Box.BoxChild w36 = ((global::Gtk.Box.BoxChild)(this.hboxFineButtons[this.buttonAttachFine]));
			w36.Position = 0;
			w36.Expand = false;
			w36.Fill = false;
			// Container child hboxFineButtons.Gtk.Box+BoxChild
			this.buttonAddFine = new global::Gamma.GtkWidgets.yButton();
			this.buttonAddFine.CanFocus = true;
			this.buttonAddFine.Name = "buttonAddFine";
			this.buttonAddFine.UseUnderline = true;
			this.buttonAddFine.Label = global::Mono.Unix.Catalog.GetString("Добавить штраф");
			this.hboxFineButtons.Add(this.buttonAddFine);
			global::Gtk.Box.BoxChild w37 = ((global::Gtk.Box.BoxChild)(this.hboxFineButtons[this.buttonAddFine]));
			w37.Position = 1;
			w37.Expand = false;
			w37.Fill = false;
			this.vbox1.Add(this.hboxFineButtons);
			global::Gtk.Box.BoxChild w38 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hboxFineButtons]));
			w38.Position = 4;
			w38.Expand = false;
			w38.Fill = false;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
		}
	}
}
