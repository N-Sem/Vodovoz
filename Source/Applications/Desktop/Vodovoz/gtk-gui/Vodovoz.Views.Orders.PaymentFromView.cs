
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Views.Orders
{
	public partial class PaymentFromView
	{
		private global::Gamma.GtkWidgets.yVBox vboxMain;

		private global::Gamma.GtkWidgets.yHBox hboxSaveAndClose;

		private global::Gamma.GtkWidgets.yButton btnSave;

		private global::Gamma.GtkWidgets.yButton btnCancel;

		private global::Gtk.HSeparator hseparator1;

		private global::Gamma.GtkWidgets.yTable tableMain;

		private global::Gamma.GtkWidgets.yEntry entryName;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entryOrganizationForAvangardPayments;

		private global::Gamma.GtkWidgets.yLabel lblName;

		private global::Gamma.GtkWidgets.yLabel lblOrganizationForAvangardPayments;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Views.Orders.PaymentFromView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Views.Orders.PaymentFromView";
			// Container child Vodovoz.Views.Orders.PaymentFromView.Gtk.Container+ContainerChild
			this.vboxMain = new global::Gamma.GtkWidgets.yVBox();
			this.vboxMain.Name = "vboxMain";
			this.vboxMain.Spacing = 6;
			// Container child vboxMain.Gtk.Box+BoxChild
			this.hboxSaveAndClose = new global::Gamma.GtkWidgets.yHBox();
			this.hboxSaveAndClose.Name = "hboxSaveAndClose";
			this.hboxSaveAndClose.Spacing = 6;
			// Container child hboxSaveAndClose.Gtk.Box+BoxChild
			this.btnSave = new global::Gamma.GtkWidgets.yButton();
			this.btnSave.CanFocus = true;
			this.btnSave.Name = "btnSave";
			this.btnSave.UseUnderline = true;
			this.btnSave.Label = global::Mono.Unix.Catalog.GetString("Сохранить");
			this.hboxSaveAndClose.Add(this.btnSave);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hboxSaveAndClose[this.btnSave]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hboxSaveAndClose.Gtk.Box+BoxChild
			this.btnCancel = new global::Gamma.GtkWidgets.yButton();
			this.btnCancel.CanFocus = true;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseUnderline = true;
			this.btnCancel.Label = global::Mono.Unix.Catalog.GetString("Отмена");
			this.hboxSaveAndClose.Add(this.btnCancel);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hboxSaveAndClose[this.btnCancel]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			this.vboxMain.Add(this.hboxSaveAndClose);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vboxMain[this.hboxSaveAndClose]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vboxMain.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator();
			this.hseparator1.Name = "hseparator1";
			this.vboxMain.Add(this.hseparator1);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vboxMain[this.hseparator1]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vboxMain.Gtk.Box+BoxChild
			this.tableMain = new global::Gamma.GtkWidgets.yTable();
			this.tableMain.Name = "tableMain";
			this.tableMain.NRows = ((uint)(2));
			this.tableMain.NColumns = ((uint)(3));
			this.tableMain.RowSpacing = ((uint)(6));
			this.tableMain.ColumnSpacing = ((uint)(6));
			// Container child tableMain.Gtk.Table+TableChild
			this.entryName = new global::Gamma.GtkWidgets.yEntry();
			this.entryName.CanFocus = true;
			this.entryName.Name = "entryName";
			this.entryName.IsEditable = true;
			this.entryName.InvisibleChar = '•';
			this.tableMain.Add(this.entryName);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.tableMain[this.entryName]));
			w5.LeftAttach = ((uint)(1));
			w5.RightAttach = ((uint)(2));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableMain.Gtk.Table+TableChild
			this.entryOrganizationForAvangardPayments = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entryOrganizationForAvangardPayments.Events = ((global::Gdk.EventMask)(256));
			this.entryOrganizationForAvangardPayments.Name = "entryOrganizationForAvangardPayments";
			this.entryOrganizationForAvangardPayments.CanEditReference = false;
			this.tableMain.Add(this.entryOrganizationForAvangardPayments);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.tableMain[this.entryOrganizationForAvangardPayments]));
			w6.TopAttach = ((uint)(1));
			w6.BottomAttach = ((uint)(2));
			w6.LeftAttach = ((uint)(1));
			w6.RightAttach = ((uint)(2));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableMain.Gtk.Table+TableChild
			this.lblName = new global::Gamma.GtkWidgets.yLabel();
			this.lblName.Name = "lblName";
			this.lblName.Xalign = 1F;
			this.lblName.LabelProp = global::Mono.Unix.Catalog.GetString("Название:");
			this.tableMain.Add(this.lblName);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.tableMain[this.lblName]));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableMain.Gtk.Table+TableChild
			this.lblOrganizationForAvangardPayments = new global::Gamma.GtkWidgets.yLabel();
			this.lblOrganizationForAvangardPayments.Name = "lblOrganizationForAvangardPayments";
			this.lblOrganizationForAvangardPayments.Xalign = 1F;
			this.lblOrganizationForAvangardPayments.LabelProp = global::Mono.Unix.Catalog.GetString("Организация для оплат через Авангард:");
			this.tableMain.Add(this.lblOrganizationForAvangardPayments);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.tableMain[this.lblOrganizationForAvangardPayments]));
			w8.TopAttach = ((uint)(1));
			w8.BottomAttach = ((uint)(2));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vboxMain.Add(this.tableMain);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vboxMain[this.tableMain]));
			w9.Position = 2;
			w9.Expand = false;
			w9.Fill = false;
			this.Add(this.vboxMain);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
		}
	}
}
