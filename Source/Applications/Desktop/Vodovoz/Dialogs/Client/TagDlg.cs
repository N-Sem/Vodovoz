﻿using NLog;
using QS.Dialog.Gtk;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Client;
using Vodovoz.Infrastructure.Converters;

namespace Vodovoz.Dialogs.Client
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class TagDlg : EntityDialogBase<Tag>
	{
		protected static Logger logger = LogManager.GetCurrentClassLogger();

		public TagDlg()
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<Tag>();
			ConfigureDlg();
		}

		public TagDlg(int id)
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<Tag>(id);
			ConfigureDlg();
		}

		public TagDlg(Tag entity) : this(entity.Id)
		{
		}

		public override bool Save()
		{
			logger.Info("Сохраняем  тег контрагента...");
			UoWGeneric.Save();
			logger.Info("Ok");
			return true;
		}

		private void ConfigureDlg()
		{
			entryName1.Binding.AddBinding(Entity, x => x.Name, x => x.Text).InitializeFromSource();
			ycolorbutton.Binding.AddBinding(Entity, x => x.ColorText, x => x.Color, new ColorTextToGdkColorConverter()).InitializeFromSource();
		}
	}
}
