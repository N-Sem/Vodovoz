﻿using FluentNHibernate.Mapping;
using Vodovoz.Domain.Orders;

namespace Vodovoz.HibernateMapping.Logistic
{
	public class TrueMarkApiDocumentMap : ClassMap<TrueMarkApiDocument>
	{
		public TrueMarkApiDocumentMap()
		{
			Table("true_mark_api_documents");

			Id(x => x.Id).Column("id").GeneratedBy.Native();
			Map(x => x.CreationDate).Column("creation_date").ReadOnly();
			Map(x => x.Guid).Column("guid");
			Map(x => x.IsSuccess).Column("is_success");
			Map(x => x.ErrorMessage).Column("error_message");

			References(x => x.Order).Column("order_id");
		}
	}
}
