﻿using System;
using QS.Dialog.GtkUI;
using QS.DomainModel.Entity.EntityPermissions.EntityExtendedPermission;
using QS.DomainModel.UoW;
using QS.Project.Services;
using Vodovoz.Additions.Store;
using Vodovoz.Domain.Documents;
using Vodovoz.Domain.Permissions.Warehouses;
using Vodovoz.EntityRepositories;
using Vodovoz.EntityRepositories.Employees;
using Vodovoz.PermissionExtensions;
using Vodovoz.ViewModels.Journals.JournalViewModels.Store;
using Vodovoz.ViewModels.Journals.JournalFactories;

namespace Vodovoz
{
	public partial class RegradingOfGoodsDocumentDlg : QS.Dialog.Gtk.EntityDialogBase<RegradingOfGoodsDocument>
	{
		static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();
		private readonly IEmployeeRepository _employeeRepository = new EmployeeRepository();
		private readonly IUserRepository _userRepository = new UserRepository();
		private readonly StoreDocumentHelper _storeDocumentHelper = new StoreDocumentHelper();

		public RegradingOfGoodsDocumentDlg()
		{
			this.Build();
			UoWGeneric = UnitOfWorkFactory.CreateWithNewRoot<RegradingOfGoodsDocument> ();
			Entity.Author = _employeeRepository.GetEmployeeForCurrentUser (UoW);
			if(Entity.Author == null)
			{
				MessageDialogHelper.RunErrorDialog ("Ваш пользователь не привязан к действующему сотруднику, вы не можете создавать складские документы, так как некого указывать в качестве кладовщика.");
				FailInitialize = true;
				return;
			}
			
			Entity.Warehouse = _storeDocumentHelper.GetDefaultWarehouse(UoW, WarehousePermissionsType.RegradingOfGoodsEdit);

			ConfigureDlg();
		}

		public RegradingOfGoodsDocumentDlg (int id)
		{
			this.Build ();
			UoWGeneric = UnitOfWorkFactory.CreateForRoot<RegradingOfGoodsDocument> (id);
			
			ConfigureDlg();
		}

		public RegradingOfGoodsDocumentDlg (RegradingOfGoodsDocument sub) : this (sub.Id)
		{
		}

		void ConfigureDlg ()
		{
			if(_storeDocumentHelper.CheckAllPermissions(UoW.IsNew, WarehousePermissionsType.RegradingOfGoodsEdit, Entity.Warehouse)) {
				FailInitialize = true;
				return;
			}

			var editing = _storeDocumentHelper.CanEditDocument(WarehousePermissionsType.RegradingOfGoodsEdit, Entity.Warehouse);
			regradingofgoodsitemsview.Sensitive = editing;

			ylabelDate.Binding.AddFuncBinding(Entity, e => e.TimeStamp.ToString("g"), w => w.LabelProp).InitializeFromSource();
			
			var userHasOnlyAccessToWarehouseAndComplaints =
				ServicesConfig.CommonServices.CurrentPermissionService.ValidatePresetPermission("user_have_access_only_to_warehouse_and_complaints")
				&& !ServicesConfig.CommonServices.UserService.GetCurrentUser(UoW).IsAdmin;

			if(userHasOnlyAccessToWarehouseAndComplaints)
			{
				warehouseEntry.CanEditReference = false;
			}
			else
			{
				warehouseEntry.CanEditReference = true;
			}

			var availableWarehousesIds =
				_storeDocumentHelper.GetRestrictedWarehousesIds(UoW, WarehousePermissionsType.RegradingOfGoodsEdit);
			var warehouseFilter = new WarehouseJournalFilterViewModel
			{
				IncludeWarehouseIds = availableWarehousesIds
			};
			warehouseEntry.SetEntityAutocompleteSelectorFactory(new WarehouseSelectorFactory(warehouseFilter));
			warehouseEntry.Binding.AddBinding(Entity, e => e.Warehouse, w => w.Subject).InitializeFromSource();
			ytextviewCommnet.Binding.AddBinding(Entity, e => e.Comment, w => w.Buffer.Text).InitializeFromSource();

			regradingofgoodsitemsview.DocumentUoW = UoWGeneric;
			if (Entity.Items.Count > 0)
				warehouseEntry.Sensitive = false;

			var permmissionValidator = new EntityExtendedPermissionValidator(PermissionExtensionSingletonStore.GetInstance(), _employeeRepository);
			Entity.CanEdit = permmissionValidator.Validate(typeof(RegradingOfGoodsDocument), _userRepository.GetCurrentUser(UoW).Id, nameof(RetroactivelyClosePermission));
			if(!Entity.CanEdit && Entity.TimeStamp.Date != DateTime.Now.Date) {
				ytextviewCommnet.Binding.AddFuncBinding(Entity, e => e.CanEdit, w => w.Sensitive).InitializeFromSource();
				warehouseEntry.Binding.AddFuncBinding(Entity, e => e.CanEdit, w => w.Sensitive).InitializeFromSource();
				regradingofgoodsitemsview.Sensitive = false;

				buttonSave.Sensitive = false;
			} else {
				Entity.CanEdit = true;
			}
		}

		public override bool Save ()
		{
			if(!Entity.CanEdit)
				return false;

			var valid = new QS.Validation.QSValidator<RegradingOfGoodsDocument> (UoWGeneric.Root);
			if (valid.RunDlgIfNotValid ((Gtk.Window)this.Toplevel))
				return false;

			Entity.LastEditor = _employeeRepository.GetEmployeeForCurrentUser (UoW);
			Entity.LastEditedTime = DateTime.Now;
			if(Entity.LastEditor == null)
			{
				MessageDialogHelper.RunErrorDialog ("Ваш пользователь не привязан к действующему сотруднику, вы не можете изменять складские документы, так как некого указывать в качестве кладовщика.");
				return false;
			}

			logger.Info ("Сохраняем документ пересортицы...");
			UoWGeneric.Save ();
			logger.Info ("Ok.");
			return true;
		}
	}
}

