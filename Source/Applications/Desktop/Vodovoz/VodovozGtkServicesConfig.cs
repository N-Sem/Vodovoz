﻿using System;
using Vodovoz.Infrastructure.Services;
using QS.Permissions;
using Vodovoz.Infrastructure.Journal;
using QS.Services;
using QS.Project.Services.GtkUI;
using Vodovoz.EntityRepositories.Employees;
using Vodovoz.EntityRepositories.Permissions;
using QS.Project.Services;
using QS.Validation;
using Vodovoz.Services;

namespace Vodovoz
{
	public static class VodovozGtkServicesConfig
	{
		public static IEmployeeService EmployeeService { get; set; }
		public static IRepresentationEntityPicker RepresentationEntityPicker { get; set; }

		public static void CreateVodovozDefaultServices()
		{
			ServicesConfig.InteractiveService = new GtkInteractiveService();
			ServicesConfig.ValidationService = new ObjectValidator(new GtkValidationViewFactory());
			EmployeeService = new EmployeeService();

			IRepresentationJournalFactory journalFactory = new PermissionControlledRepresentationJournalFactory();
			RepresentationEntityPicker = new RepresentationEntityPickerGtk(journalFactory);

			PermissionsSettings.ConfigureEntityPermissionFinder(new Vodovoz.Domain.Permissions.EntitiesWithPermissionFinder());

			//пространство имен специально прописано чтобы при изменениях не было случайного совпадения с валидатором из QS
			var entityPermissionValidator =
				new Vodovoz.Domain.Permissions.EntityPermissionValidator(new EmployeeRepository(), new PermissionRepository());
			var presetPermissionValidator =
				new Vodovoz.Domain.Permissions.HierarchicalPresetPermissionValidator(new EmployeeRepository(), new PermissionRepository());
			var permissionService = new PermissionService(entityPermissionValidator, presetPermissionValidator);
			PermissionsSettings.PermissionService = permissionService;
			PermissionsSettings.CurrentPermissionService = new CurrentPermissionServiceAdapter(permissionService, ServicesConfig.UserService);
		}
	}
}
