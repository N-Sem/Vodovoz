﻿using System;
using System.Collections.Generic;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Contacts;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.StoredEmails;
using Vodovoz.Parameters;

namespace Vodovoz.EntityRepositories
{
	public interface IEmailRepository
	{
		List<StoredEmail> GetAllEmailsForOrder(IUnitOfWork uow, int orderId);
		List<CounterpartyEmail> GetEmailsForPreparingOrderDocuments(IUnitOfWork uow);
		StoredEmail GetStoredEmailByMessageId(IUnitOfWork uow, string messageId);
		bool HaveSendedEmailForBill(int orderId);
		bool CanSendByTimeout(string address, int orderId, OrderDocumentType type);
		int GetCurrentDatabaseId(IUnitOfWork uow);
		int GetCounterpartyIdByEmailGuidForUnsubscribing(IUnitOfWork uow, Guid emailGuid);
		IList<BulkEmailEventReason> GetUnsubscribingReasons(IUnitOfWork uow, IEmailParametersProvider emailParametersProvider, bool isForUnsubscribePage = false);
		BulkEmailEvent GetLastBulkEmailEvent(IUnitOfWork uow, int counterpartyId);
		BulkEmailEventReason GetBulkEmailEventOtherReason(IUnitOfWork uoW, IEmailParametersProvider emailParametersProvider);
		BulkEmailEventReason GetBulkEmailEventOperatorReason(IUnitOfWork uoW, IEmailParametersProvider emailParametersProvider);

		#region EmailType

		IList<EmailType> GetEmailTypes(IUnitOfWork uow);
		EmailType EmailTypeWithPurposeExists(IUnitOfWork uow, EmailPurpose emailPurpose);
		StoredEmail GetById(IUnitOfWork unitOfWork, int id);

		#endregion
	}
}
