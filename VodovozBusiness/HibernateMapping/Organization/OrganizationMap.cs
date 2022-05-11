﻿using FluentNHibernate.Mapping;

namespace Vodovoz.HibernateMapping
{
    public class OrganizationMap : ClassMap<Domain.Organizations.Organization>
    {
        public OrganizationMap()
        {
            Table("organizations");

            Id(x => x.Id).Column("id").GeneratedBy.Native();
            Map(x => x.Name).Column("name");
            Map(x => x.FullName).Column("full_name");
            Map(x => x.INN).Column("INN");
            Map(x => x.KPP).Column("KPP");
            Map(x => x.OGRN).Column("OGRN");
            Map(x => x.OKPO).Column("OKPO");
            Map(x => x.OKVED).Column("OKVED");
            Map(x => x.Email).Column("email");
            Map(x => x.Address).Column("address");
            Map(x => x.JurAddress).Column("jur_address");
            Map(x => x.WithoutVAT).Column("without_vat").ReadOnly();
            Map(x => x.CashBoxId).Column("cash_box_id");

            References(x => x.Leader).Column("leader_id");
            References(x => x.Buhgalter).Column("buhgalter_id");
            References(x => x.Stamp).Column("stamp_id");

            HasMany(x => x.Accounts).Cascade.AllDeleteOrphan().LazyLoad().KeyColumn("org_id");
            HasMany(x => x.Phones).Cascade.AllDeleteOrphan().LazyLoad().KeyColumn("org_id");
        }
    }
}
