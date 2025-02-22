
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.JournalViewers
{
	public partial class TasksView
	{
		private global::Gtk.VBox vboxJournal;

		private global::Gtk.HBox hbxDlgControls;

		private global::Gtk.Button buttonAdd;

		private global::Gtk.Button buttonEdit;

		private global::Gtk.Button buttonDelete;

		private global::Gtk.RadioButton radiobuttonEditSelected;

		private global::Gamma.GtkWidgets.yButton buttonExport;

		private global::Gtk.RadioButton radiobuttonShowFilter;

		private global::Gtk.Button buttonRefresh;

		private global::Gamma.GtkWidgets.yCheckButton ycheckbuttonAutoUpdate;

		private global::Gtk.HSeparator hseparator1;

		private global::Gtk.HBox hboxEditSelected;

		private global::Gtk.HBox hbox6;

		private global::Gtk.Button buttonChandgeEployee;

		private global::Gamma.Widgets.yEnumComboBox taskStatusComboBox;

		private global::Gtk.Button buttonCompleteSelected;

		private global::QS.Widgets.GtkUI.DatePicker datepickerDeadlineChange;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry evmeEmployee;

		private global::Gtk.HBox hboxTasksFilter;

		private global::Gtk.HSeparator hseparator2;

		private global::Gtk.VBox vbox2;

		private global::Gtk.HBox hbox10;

		private global::QSWidgetLib.SearchEntity searchentity;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::QSOrmProject.RepresentationTreeView representationtreeviewTask;

		private global::Gtk.HBox hboxStatistics;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.JournalViewers.TasksView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.JournalViewers.TasksView";
			// Container child Vodovoz.JournalViewers.TasksView.Gtk.Container+ContainerChild
			this.vboxJournal = new global::Gtk.VBox();
			this.vboxJournal.Name = "vboxJournal";
			this.vboxJournal.Spacing = 6;
			// Container child vboxJournal.Gtk.Box+BoxChild
			this.hbxDlgControls = new global::Gtk.HBox();
			this.hbxDlgControls.Name = "hbxDlgControls";
			this.hbxDlgControls.Spacing = 6;
			// Container child hbxDlgControls.Gtk.Box+BoxChild
			this.buttonAdd = new global::Gtk.Button();
			this.buttonAdd.CanFocus = true;
			this.buttonAdd.Name = "buttonAdd";
			this.buttonAdd.UseUnderline = true;
			this.buttonAdd.Label = global::Mono.Unix.Catalog.GetString("Добавить");
			global::Gtk.Image w1 = new global::Gtk.Image();
			w1.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-add", global::Gtk.IconSize.Menu);
			this.buttonAdd.Image = w1;
			this.hbxDlgControls.Add(this.buttonAdd);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbxDlgControls[this.buttonAdd]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbxDlgControls.Gtk.Box+BoxChild
			this.buttonEdit = new global::Gtk.Button();
			this.buttonEdit.CanFocus = true;
			this.buttonEdit.Name = "buttonEdit";
			this.buttonEdit.UseUnderline = true;
			this.buttonEdit.Label = global::Mono.Unix.Catalog.GetString("Изменить");
			global::Gtk.Image w3 = new global::Gtk.Image();
			w3.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-edit", global::Gtk.IconSize.Menu);
			this.buttonEdit.Image = w3;
			this.hbxDlgControls.Add(this.buttonEdit);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbxDlgControls[this.buttonEdit]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbxDlgControls.Gtk.Box+BoxChild
			this.buttonDelete = new global::Gtk.Button();
			this.buttonDelete.CanFocus = true;
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.UseUnderline = true;
			this.buttonDelete.Label = global::Mono.Unix.Catalog.GetString("Удалить");
			global::Gtk.Image w5 = new global::Gtk.Image();
			w5.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-remove", global::Gtk.IconSize.Menu);
			this.buttonDelete.Image = w5;
			this.hbxDlgControls.Add(this.buttonDelete);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbxDlgControls[this.buttonDelete]));
			w6.Position = 2;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hbxDlgControls.Gtk.Box+BoxChild
			this.radiobuttonEditSelected = new global::Gtk.RadioButton(global::Mono.Unix.Catalog.GetString("Изменить задачи"));
			this.radiobuttonEditSelected.CanFocus = true;
			this.radiobuttonEditSelected.Name = "radiobuttonEditSelected";
			this.radiobuttonEditSelected.DrawIndicator = false;
			this.radiobuttonEditSelected.UseUnderline = true;
			this.radiobuttonEditSelected.Group = new global::GLib.SList(global::System.IntPtr.Zero);
			this.hbxDlgControls.Add(this.radiobuttonEditSelected);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbxDlgControls[this.radiobuttonEditSelected]));
			w7.PackType = ((global::Gtk.PackType)(1));
			w7.Position = 4;
			w7.Expand = false;
			// Container child hbxDlgControls.Gtk.Box+BoxChild
			this.buttonExport = new global::Gamma.GtkWidgets.yButton();
			this.buttonExport.CanFocus = true;
			this.buttonExport.Name = "buttonExport";
			this.buttonExport.UseUnderline = true;
			this.buttonExport.Label = global::Mono.Unix.Catalog.GetString("Экспорт");
			this.hbxDlgControls.Add(this.buttonExport);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbxDlgControls[this.buttonExport]));
			w8.PackType = ((global::Gtk.PackType)(1));
			w8.Position = 5;
			w8.Expand = false;
			w8.Fill = false;
			// Container child hbxDlgControls.Gtk.Box+BoxChild
			this.radiobuttonShowFilter = new global::Gtk.RadioButton(global::Mono.Unix.Catalog.GetString("Фильтр"));
			this.radiobuttonShowFilter.CanFocus = true;
			this.radiobuttonShowFilter.Name = "radiobuttonShowFilter";
			this.radiobuttonShowFilter.DrawIndicator = false;
			this.radiobuttonShowFilter.UseUnderline = true;
			this.radiobuttonShowFilter.Group = this.radiobuttonEditSelected.Group;
			this.hbxDlgControls.Add(this.radiobuttonShowFilter);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbxDlgControls[this.radiobuttonShowFilter]));
			w9.PackType = ((global::Gtk.PackType)(1));
			w9.Position = 6;
			w9.Expand = false;
			// Container child hbxDlgControls.Gtk.Box+BoxChild
			this.buttonRefresh = new global::Gtk.Button();
			this.buttonRefresh.CanFocus = true;
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.UseUnderline = true;
			this.buttonRefresh.Label = global::Mono.Unix.Catalog.GetString("Обновить");
			global::Gtk.Image w10 = new global::Gtk.Image();
			w10.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-refresh", global::Gtk.IconSize.Menu);
			this.buttonRefresh.Image = w10;
			this.hbxDlgControls.Add(this.buttonRefresh);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbxDlgControls[this.buttonRefresh]));
			w11.PackType = ((global::Gtk.PackType)(1));
			w11.Position = 7;
			w11.Expand = false;
			w11.Fill = false;
			// Container child hbxDlgControls.Gtk.Box+BoxChild
			this.ycheckbuttonAutoUpdate = new global::Gamma.GtkWidgets.yCheckButton();
			this.ycheckbuttonAutoUpdate.CanFocus = true;
			this.ycheckbuttonAutoUpdate.Name = "ycheckbuttonAutoUpdate";
			this.ycheckbuttonAutoUpdate.Label = global::Mono.Unix.Catalog.GetString("Автообновление");
			this.ycheckbuttonAutoUpdate.Active = true;
			this.ycheckbuttonAutoUpdate.DrawIndicator = true;
			this.ycheckbuttonAutoUpdate.UseUnderline = true;
			this.hbxDlgControls.Add(this.ycheckbuttonAutoUpdate);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbxDlgControls[this.ycheckbuttonAutoUpdate]));
			w12.PackType = ((global::Gtk.PackType)(1));
			w12.Position = 8;
			w12.Expand = false;
			w12.Fill = false;
			this.vboxJournal.Add(this.hbxDlgControls);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vboxJournal[this.hbxDlgControls]));
			w13.Position = 0;
			w13.Expand = false;
			w13.Fill = false;
			// Container child vboxJournal.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator();
			this.hseparator1.Name = "hseparator1";
			this.vboxJournal.Add(this.hseparator1);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vboxJournal[this.hseparator1]));
			w14.Position = 1;
			w14.Expand = false;
			w14.Fill = false;
			// Container child vboxJournal.Gtk.Box+BoxChild
			this.hboxEditSelected = new global::Gtk.HBox();
			this.hboxEditSelected.Name = "hboxEditSelected";
			this.hboxEditSelected.Spacing = 6;
			// Container child hboxEditSelected.Gtk.Box+BoxChild
			this.hbox6 = new global::Gtk.HBox();
			this.hbox6.Name = "hbox6";
			this.hbox6.Spacing = 6;
			// Container child hbox6.Gtk.Box+BoxChild
			this.buttonChandgeEployee = new global::Gtk.Button();
			this.buttonChandgeEployee.CanFocus = true;
			this.buttonChandgeEployee.Name = "buttonChandgeEployee";
			this.buttonChandgeEployee.UseUnderline = true;
			this.buttonChandgeEployee.Label = global::Mono.Unix.Catalog.GetString("Ответственный");
			this.hbox6.Add(this.buttonChandgeEployee);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.buttonChandgeEployee]));
			w15.Position = 0;
			w15.Expand = false;
			w15.Fill = false;
			this.hboxEditSelected.Add(this.hbox6);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hboxEditSelected[this.hbox6]));
			w16.Position = 0;
			w16.Expand = false;
			w16.Fill = false;
			// Container child hboxEditSelected.Gtk.Box+BoxChild
			this.taskStatusComboBox = new global::Gamma.Widgets.yEnumComboBox();
			this.taskStatusComboBox.Name = "taskStatusComboBox";
			this.taskStatusComboBox.ShowSpecialStateAll = false;
			this.taskStatusComboBox.ShowSpecialStateNot = false;
			this.taskStatusComboBox.UseShortTitle = false;
			this.taskStatusComboBox.DefaultFirst = false;
			this.hboxEditSelected.Add(this.taskStatusComboBox);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hboxEditSelected[this.taskStatusComboBox]));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			// Container child hboxEditSelected.Gtk.Box+BoxChild
			this.buttonCompleteSelected = new global::Gtk.Button();
			this.buttonCompleteSelected.CanFocus = true;
			this.buttonCompleteSelected.Name = "buttonCompleteSelected";
			this.buttonCompleteSelected.UseUnderline = true;
			this.buttonCompleteSelected.Label = global::Mono.Unix.Catalog.GetString("Задачи выполнены");
			global::Gtk.Image w18 = new global::Gtk.Image();
			w18.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-ok", global::Gtk.IconSize.Menu);
			this.buttonCompleteSelected.Image = w18;
			this.hboxEditSelected.Add(this.buttonCompleteSelected);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.hboxEditSelected[this.buttonCompleteSelected]));
			w19.Position = 2;
			w19.Expand = false;
			w19.Fill = false;
			// Container child hboxEditSelected.Gtk.Box+BoxChild
			this.datepickerDeadlineChange = new global::QS.Widgets.GtkUI.DatePicker();
			this.datepickerDeadlineChange.Events = ((global::Gdk.EventMask)(256));
			this.datepickerDeadlineChange.Name = "datepickerDeadlineChange";
			this.datepickerDeadlineChange.WithTime = true;
			this.datepickerDeadlineChange.HideCalendarButton = false;
			this.datepickerDeadlineChange.Date = new global::System.DateTime(0);
			this.datepickerDeadlineChange.IsEditable = true;
			this.datepickerDeadlineChange.AutoSeparation = false;
			this.hboxEditSelected.Add(this.datepickerDeadlineChange);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hboxEditSelected[this.datepickerDeadlineChange]));
			w20.Position = 3;
			w20.Expand = false;
			w20.Fill = false;
			// Container child hboxEditSelected.Gtk.Box+BoxChild
			this.evmeEmployee = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.evmeEmployee.Events = ((global::Gdk.EventMask)(256));
			this.evmeEmployee.Name = "evmeEmployee";
			this.evmeEmployee.CanEditReference = true;
			this.hboxEditSelected.Add(this.evmeEmployee);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.hboxEditSelected[this.evmeEmployee]));
			w21.Position = 4;
			this.vboxJournal.Add(this.hboxEditSelected);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.vboxJournal[this.hboxEditSelected]));
			w22.Position = 2;
			w22.Expand = false;
			w22.Fill = false;
			// Container child vboxJournal.Gtk.Box+BoxChild
			this.hboxTasksFilter = new global::Gtk.HBox();
			this.hboxTasksFilter.Name = "hboxTasksFilter";
			this.hboxTasksFilter.Spacing = 6;
			this.vboxJournal.Add(this.hboxTasksFilter);
			global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.vboxJournal[this.hboxTasksFilter]));
			w23.Position = 3;
			w23.Expand = false;
			// Container child vboxJournal.Gtk.Box+BoxChild
			this.hseparator2 = new global::Gtk.HSeparator();
			this.hseparator2.Name = "hseparator2";
			this.vboxJournal.Add(this.hseparator2);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.vboxJournal[this.hseparator2]));
			w24.Position = 4;
			w24.Expand = false;
			w24.Fill = false;
			// Container child vboxJournal.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox10 = new global::Gtk.HBox();
			this.hbox10.Name = "hbox10";
			this.hbox10.Spacing = 6;
			// Container child hbox10.Gtk.Box+BoxChild
			this.searchentity = new global::QSWidgetLib.SearchEntity();
			this.searchentity.Events = ((global::Gdk.EventMask)(256));
			this.searchentity.Name = "searchentity";
			this.hbox10.Add(this.searchentity);
			global::Gtk.Box.BoxChild w25 = ((global::Gtk.Box.BoxChild)(this.hbox10[this.searchentity]));
			w25.Position = 0;
			this.vbox2.Add(this.hbox10);
			global::Gtk.Box.BoxChild w26 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox10]));
			w26.Position = 0;
			w26.Expand = false;
			w26.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.representationtreeviewTask = new global::QSOrmProject.RepresentationTreeView();
			this.representationtreeviewTask.CanFocus = true;
			this.representationtreeviewTask.Name = "representationtreeviewTask";
			this.GtkScrolledWindow.Add(this.representationtreeviewTask);
			this.vbox2.Add(this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.GtkScrolledWindow]));
			w28.Position = 1;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hboxStatistics = new global::Gtk.HBox();
			this.hboxStatistics.Name = "hboxStatistics";
			this.hboxStatistics.Spacing = 6;
			this.vbox2.Add(this.hboxStatistics);
			global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hboxStatistics]));
			w29.Position = 2;
			w29.Expand = false;
			this.vboxJournal.Add(this.vbox2);
			global::Gtk.Box.BoxChild w30 = ((global::Gtk.Box.BoxChild)(this.vboxJournal[this.vbox2]));
			w30.Position = 5;
			this.Add(this.vboxJournal);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.evmeEmployee.Hide();
			this.hboxEditSelected.Hide();
			this.Hide();
			this.buttonAdd.Clicked += new global::System.EventHandler(this.OnAddTaskButtonClicked);
			this.buttonEdit.Clicked += new global::System.EventHandler(this.OnButtonEditClicked);
			this.buttonDelete.Clicked += new global::System.EventHandler(this.OnButtonDeleteClicked);
			this.ycheckbuttonAutoUpdate.Toggled += new global::System.EventHandler(this.OnYcheckbuttonAutoUpdateToggled);
			this.buttonRefresh.Clicked += new global::System.EventHandler(this.OnButtonRefreshClicked);
			this.radiobuttonShowFilter.Toggled += new global::System.EventHandler(this.OnRadiobuttonShowFilterToggled);
			this.radiobuttonShowFilter.Clicked += new global::System.EventHandler(this.OnRadiobuttonShowFilterClicked);
			this.radiobuttonEditSelected.Toggled += new global::System.EventHandler(this.OnRadiobuttonEditSelectedToggled);
			this.radiobuttonEditSelected.Clicked += new global::System.EventHandler(this.OnRadiobuttonEditSelectedClicked);
			this.buttonChandgeEployee.Clicked += new global::System.EventHandler(this.OnAssignedEmployeeButtonClicked);
			this.taskStatusComboBox.EnumItemSelected += new global::System.EventHandler<Gamma.Widgets.ItemSelectedEventArgs>(this.OnTaskstateButtonEnumItemClicked);
			this.buttonCompleteSelected.Clicked += new global::System.EventHandler(this.OnCompleteTaskButtonClicked);
			this.datepickerDeadlineChange.DateChangedByUser += new global::System.EventHandler(this.OnDatepickerDeadlineChangeDateChangedByUser);
			this.searchentity.TextChanged += new global::System.EventHandler(this.OnSearchentityTextChanged);
			this.representationtreeviewTask.RowActivated += new global::Gtk.RowActivatedHandler(this.OnRepresentationtreeviewTaskRowActivated);
			this.representationtreeviewTask.ButtonReleaseEvent += new global::Gtk.ButtonReleaseEventHandler(this.OnRepresentationtreeviewTaskButtonReleaseEvent);
		}
	}
}
