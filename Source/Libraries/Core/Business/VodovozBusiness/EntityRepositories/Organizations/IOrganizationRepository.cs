﻿using QS.DomainModel.UoW;
using Vodovoz.Domain.Organizations;

namespace Vodovoz.EntityRepositories.Organizations
{
	public interface IOrganizationRepository
	{
		Organization GetOrganizationByInn(IUnitOfWork uow, string inn);
		Organization GetOrganizationByAccountNumber(IUnitOfWork uow, string accountNumber);
		Organization GetOrganizationById(IUnitOfWork uow, int organizationId);
		Organization GetPaymentFromOrganizationById(IUnitOfWork uow, int paymentFromId);
		Organization GetOrganizationByTaxcomEdoAccountId(IUnitOfWork uow, string edoAccountId);
	}
}
