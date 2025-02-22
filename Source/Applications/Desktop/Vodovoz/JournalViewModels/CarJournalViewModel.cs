﻿using System;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QS.Project.DB;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Project.Services;
using QS.Services;
using Vodovoz.Controllers;
using Vodovoz.Core.DataService;
using Vodovoz.Domain.Employees;
using Vodovoz.Filters.ViewModels;
using Vodovoz.Domain.Logistic.Cars;
using Vodovoz.Domain.WageCalculation.CalculationServices.RouteList;
using Vodovoz.EntityRepositories.WageCalculation;
using Vodovoz.Factories;
using Vodovoz.Infrastructure.Services;
using Vodovoz.JournalNodes;
using Vodovoz.Models;
using Vodovoz.Parameters;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Factories;
using Vodovoz.ViewModels.Journals.JournalFactories;
using Vodovoz.ViewModels.ViewModels.Logistic;

namespace Vodovoz.JournalViewModels
{
	public class CarJournalViewModel : FilterableSingleEntityJournalViewModelBase
		<Car, CarViewModel, CarJournalNode, CarJournalFilterViewModel>
	{
		public CarJournalViewModel(
			CarJournalFilterViewModel filterViewModel,
			IUnitOfWorkFactory unitOfWorkFactory,
			ICommonServices commonServices) : base(filterViewModel, unitOfWorkFactory, commonServices)
		{
			TabName = "Журнал автомобилей";
			UpdateOnChanges(
				typeof(Car),
				typeof(Employee)
			);
		}

		protected override Func<IUnitOfWork, IQueryOver<Car>> ItemsSourceQueryFunction => uow =>
		{
			var currentDateTime = DateTime.Now;

			CarJournalNode carJournalNodeAlias = null;
			Car carAlias = null;
			CarModel carModelAlias = null;
			CarVersion currentCarVersion = null;
			Employee driverAlias = null;
			CarManufacturer carManufacturerAlias = null;

			var query = uow.Session.QueryOver<Car>(() => carAlias)
				.Inner.JoinAlias(c => c.CarModel, () => carModelAlias)
				.Inner.JoinAlias(() => carModelAlias.CarManufacturer, () => carManufacturerAlias)
				.JoinEntityAlias(() => currentCarVersion,
					() => currentCarVersion.Car.Id == carAlias.Id
						&& currentCarVersion.StartDate <= currentDateTime
						&& (currentCarVersion.EndDate == null || currentCarVersion.EndDate >= currentDateTime))
				.Left.JoinAlias(c => c.Driver, () => driverAlias);

			if(FilterViewModel.Archive != null)
			{
				query.Where(c => c.IsArchive == FilterViewModel.Archive);
			}

			if(FilterViewModel.VisitingMasters != null)
			{
				if(FilterViewModel.VisitingMasters.Value)
				{
					query.Where(() => driverAlias.VisitingMaster);
				}
				else
				{
					query.Where(Restrictions.Disjunction()
						.Add(Restrictions.IsNull(Projections.Property(() => driverAlias.Id)))
						.Add(() => !driverAlias.VisitingMaster));
				}
			}

			if(FilterViewModel.RestrictedCarTypesOfUse != null)
			{
				query.WhereRestrictionOn(() => carModelAlias.CarTypeOfUse).IsIn(FilterViewModel.RestrictedCarTypesOfUse.ToArray());
			}

			if(FilterViewModel.RestrictedCarOwnTypes != null)
			{
				query.WhereRestrictionOn(() => currentCarVersion.CarOwnType).IsIn(FilterViewModel.RestrictedCarOwnTypes.ToArray());
			}

			if(FilterViewModel.CarModel != null)
			{
				query.Where(() => carModelAlias.Id == FilterViewModel.CarModel.Id);
			}

			query.Where(GetSearchCriterion(
				() => carAlias.Id,
				() => carManufacturerAlias.Name,
				() => carModelAlias.Name,
				() => carAlias.RegistrationNumber,
				() => driverAlias.Name,
				() => driverAlias.LastName,
				() => driverAlias.Patronymic
			));

			var result = query
				.SelectList(list => list
					.Select(c => c.Id).WithAlias(() => carJournalNodeAlias.Id)
					.Select(() => carManufacturerAlias.Name).WithAlias(() => carJournalNodeAlias.ManufacturerName)
					.Select(() => carModelAlias.Name).WithAlias(() => carJournalNodeAlias.ModelName)
					.Select(c => c.RegistrationNumber).WithAlias(() => carJournalNodeAlias.RegistrationNumber)
					.Select(c => c.IsArchive).WithAlias(() => carJournalNodeAlias.IsArchive)
					.Select(CustomProjections.Concat_WS(" ",
						Projections.Property(() => driverAlias.LastName),
						Projections.Property(() => driverAlias.Name),
						Projections.Property(() => driverAlias.Patronymic))
					).WithAlias(() => carJournalNodeAlias.DriverName))
				.OrderBy(() => carAlias.Id).Asc
				.TransformUsing(Transformers.AliasToBean<CarJournalNode>());

			return result;
		};

		protected override Func<CarViewModel> CreateDialogFunction
		{
			get
			{
				return () =>
				{
					var commonServices = ServicesConfig.CommonServices;
					var subdivisionJournalFactory = new SubdivisionJournalFactory();
					var warehouseJournalFactory = new WarehouseJournalFactory();
					var employeeService = new EmployeeService();
					var geoGroupVersionsModel = new GeoGroupVersionsModel(commonServices.UserService, employeeService);
					var geoGroupJournalFactory = new GeoGroupJournalFactory(UnitOfWorkFactory, commonServices, subdivisionJournalFactory, warehouseJournalFactory, geoGroupVersionsModel);
					var wageParameterService = new WageParameterService(new WageCalculationRepository(), new BaseParametersProvider(new ParametersProvider()));

					var viewModel = new CarViewModel(
						EntityUoWBuilder.ForCreate(),
						UnitOfWorkFactory,
						commonServices,
						new EmployeeJournalFactory(),
						new AttachmentsViewModelFactory(),
						new CarModelJournalFactory(),
						new CarVersionsViewModelFactory(commonServices),
						new OdometerReadingsViewModelFactory(commonServices),
						new RouteListsWageController(wageParameterService),
						geoGroupJournalFactory,
						NavigationManager
					);
					return viewModel;
				};
			}
		}

		protected override Func<CarJournalNode, CarViewModel> OpenDialogFunction
		{
			get
			{
				return (node) =>
				{
					var commonServices = ServicesConfig.CommonServices;
					var subdivisionJournalFactory = new SubdivisionJournalFactory();
					var warehouseJournalFactory = new WarehouseJournalFactory();
					var employeeService = new EmployeeService();
					var geoGroupVersionsModel = new GeoGroupVersionsModel(commonServices.UserService, employeeService);
					var geoGroupJournalFactory = new GeoGroupJournalFactory(UnitOfWorkFactory, commonServices, subdivisionJournalFactory, warehouseJournalFactory, geoGroupVersionsModel);
					var wageParameterService = new WageParameterService(new WageCalculationRepository(), new BaseParametersProvider(new ParametersProvider()));

					var viewModel = new CarViewModel(
						EntityUoWBuilder.ForOpen(node.Id),
						UnitOfWorkFactory,
						commonServices,
						new EmployeeJournalFactory(),
						new AttachmentsViewModelFactory(),
						new CarModelJournalFactory(),
						new CarVersionsViewModelFactory(commonServices),
						new OdometerReadingsViewModelFactory(commonServices),
						new RouteListsWageController(wageParameterService),
						geoGroupJournalFactory,
						NavigationManager
					);
					return viewModel;
				};
			}
		}
	}
}
