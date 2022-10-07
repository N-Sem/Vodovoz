using System;
using Gamma.Binding.Core;
using Gdk;
using Gtk;
using Vodovoz.Domain.Contacts;
using Vodovoz.Infrastructure.Mango;

namespace Vodovoz.ViewWidgets.Mango
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class HandsetView : QS.Dialog.Gtk.WidgetOnDialogBase
	{
		private Phone Phone;
		public BindingControler<HandsetView> Binding { get; private set; }

		public HandsetView(string number)
		{
			this.Build();

			Binding = new BindingControler<HandsetView>(this);
			Phone = new Phone();
			Phone.Number = number;
		}

		public void SetPhone(string number)
		{
			Phone = new Phone();
			Phone.Number = number;
		}
		protected void Clicked_buttonMakeCall(object sender, EventArgs e)
		{
			if(Phone.DigitsNumber.Length == 10)
				MainClass.MainWin.MangoManager.MakeCall("7"+Phone.DigitsNumber);
		}
	}
}
