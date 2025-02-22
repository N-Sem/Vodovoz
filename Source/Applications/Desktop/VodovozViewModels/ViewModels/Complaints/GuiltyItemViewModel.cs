﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QS.DomainModel.UoW;
using QS.Project.Journal.EntitySelector;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Complaints;
using Vodovoz.EntityRepositories.Subdivisions;
using Vodovoz.Parameters;

namespace Vodovoz.ViewModels.Complaints
{
	public class GuiltyItemViewModel : EntityWidgetViewModelBase<ComplaintGuiltyItem>
	{
		private readonly ISubdivisionParametersProvider _subdivisionParametersProvider;
		private bool _isForSalesDepartment;

		public GuiltyItemViewModel(
			ComplaintGuiltyItem entity,
			ICommonServices commonServices,
			ISubdivisionRepository subdivisionRepository,
			IEntityAutocompleteSelectorFactory employeeSelectorFactory,
			ISubdivisionParametersProvider subdivisionParametersProvider,
			IUnitOfWork uow,
			bool fromComplaintsJournalFilter = false
		) : base(entity, commonServices)
		{
			UoW = uow ?? throw new ArgumentNullException(nameof(uow));
			EmployeeSelectorFactory = employeeSelectorFactory ?? throw new ArgumentNullException(nameof(employeeSelectorFactory));
			if(subdivisionRepository == null) {
				throw new ArgumentNullException(nameof(subdivisionRepository));
			}
			_subdivisionParametersProvider = subdivisionParametersProvider ?? throw new ArgumentNullException(nameof(subdivisionParametersProvider)); ;
			ConfigureEntityPropertyChanges();
			AllDepartments = subdivisionRepository.GetAllDepartmentsOrderedByName(UoW);
			HideClientFromGuilty = !fromComplaintsJournalFilter;
			ResponsibleList = uow.GetAll<Responsible>().Where(r => !r.IsArchived).ToList();
		}

		public event EventHandler OnGuiltyItemReady;

		private IList<Subdivision> allDepartments;
		public IList<Subdivision> AllDepartments {
			get => allDepartments;
			private set => SetField(ref allDepartments, value);
		}

		public bool CanChooseEmployee => Entity.Responsible != null && Entity.Responsible.IsEmployeeResponsible;

		public bool CanChooseSubdivision => Entity.Responsible != null && Entity.Responsible.IsSubdivisionResponsible;
		public bool HideClientFromGuilty { get; }
		public IList<Responsible> ResponsibleList { get; }

		public bool IsForSalesDepartment
		{
			get => _isForSalesDepartment;
			set
			{
				_isForSalesDepartment = value;

				if(value)
				{
					Entity.Responsible = ResponsibleList.FirstOrDefault(r => r.IsSubdivisionResponsible);
					var salesSubDivisionId = _subdivisionParametersProvider.GetSalesSubdivisionId();
					Entity.Subdivision = UoW.GetById<Subdivision>(salesSubDivisionId);
				}
			}
		}

		public IEntityAutocompleteSelectorFactory EmployeeSelectorFactory { get; }

		void ConfigureEntityPropertyChanges()
		{
			SetPropertyChangeRelation(
				e => e.Responsible,
				() => CanChooseEmployee,
				() => CanChooseSubdivision
			);

			OnEntityPropertyChanged(
				() => OnGuiltyItemReady?.Invoke(this, EventArgs.Empty),
				e => e.Responsible,
				e => e.Employee,
				e => e.Subdivision
			);
		}
	}
}
