﻿using System;
using NHibernate;
using NHibernate.Criterion;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Services;
using Vodovoz.Domain;
using QS.ViewModels;
using Vodovoz.Domain.Goods;
using Vodovoz.EntityRepositories.RentPackages;
using QS.Project.Journal.EntitySelector;
using Vodovoz.TempAdapters;

namespace Vodovoz.ViewModels.ViewModels.Rent
{
    public class FreeRentPackageViewModel : EntityTabViewModelBase<FreeRentPackage>
    {
		private readonly INomenclatureJournalFactory _nomenclatureJournalFactory;
		private readonly IRentPackageRepository _rentPackageRepository;
		private IEntityAutocompleteSelectorFactory _depositServiceSelectorFactory;


		public FreeRentPackageViewModel(
            IEntityUoWBuilder uowBuilder,
            IUnitOfWorkFactory unitOfWorkFactory,
            ICommonServices commonServices,
			INomenclatureJournalFactory nomenclatureJournalFactory,
            IRentPackageRepository rentPackageRepository) : base(uowBuilder, unitOfWorkFactory, commonServices)
	    {
			_nomenclatureJournalFactory = nomenclatureJournalFactory ?? throw new ArgumentNullException(nameof(nomenclatureJournalFactory));
			_rentPackageRepository = rentPackageRepository ?? throw new ArgumentNullException(nameof(rentPackageRepository));
		    
		    ConfigureValidateContext();
	    }
        
		public IEntityAutocompleteSelectorFactory DepositServiceSelectorFactory
		{
			get
			{
				if(_depositServiceSelectorFactory == null)
				{
					_depositServiceSelectorFactory = _nomenclatureJournalFactory.GetDepositSelectorFactory();
				}
				return _depositServiceSelectorFactory;
			}
		}

		private void ConfigureValidateContext()
        {
	        ValidationContext.ServiceContainer.AddService(typeof(IRentPackageRepository), _rentPackageRepository);
        }
    }
}
