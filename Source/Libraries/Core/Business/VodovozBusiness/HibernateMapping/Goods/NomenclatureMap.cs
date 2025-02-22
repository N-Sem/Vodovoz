﻿using FluentNHibernate.Mapping;
using Vodovoz.Domain.Goods;

namespace Vodovoz.HibernateMapping
{
	public class NomenclatureMap : ClassMap<Nomenclature>
	{
		public NomenclatureMap()
		{
			Table("nomenclature");
			Not.LazyLoad();

			Id(x => x.Id).Column("id").GeneratedBy.Native();

			Map(x => x.CreateDate).Column("create_date");
			Map(x => x.IsArchive).Column("is_archive");
			Map(x => x.CanPrintPrice).Column("can_print_price");
			Map(x => x.Name).Column("name");
			Map(x => x.OfficialName).Column("official_name");
			Map(x => x.Model).Column("model");
			Map(x => x.Weight).Column("weight");
			Map(x => x.Length).Column("length");
			Map(x => x.Width).Column("width");
			Map(x => x.Height).Column("height");
			Map(x => x.VAT).Column("vat").CustomType<VATStringType>();
			Map(x => x.DoNotReserve).Column("reserve");
			Map(x => x.RentPriority).Column("rent_priority");
			Map(x => x.IsDuty).Column("is_duty");
			Map(x => x.IsSerial).Column("serial");
			Map(x => x.Category).Column("category").CustomType<NomenclatureCategoryStringType>();
			Map(x => x.TareVolume).Column("tare_volume").CustomType<TareVolumeStringType>();
			Map(x => x.IsDisposableTare).Column("is_disposable_tare");
			Map(x => x.Code1c).Column("code_1c");
			Map(x => x.SumOfDamage).Column("sum_of_damage");
			Map(x => x.ShortName).Column("short_name");
			Map(x => x.Hide).Column("hide");
			Map(x => x.NoDelivery).Column("no_delivery");
			Map(x => x.IsNewBottle).Column("is_new_bottle");
			Map(x => x.IsDefectiveBottle).Column("is_defective_bottle");
			Map(x => x.IsShabbyBottle).Column("is_shabby_bottle");
			Map(x => x.IsDiler).Column("is_diler");
			Map(x => x.PercentForMaster).Column("percent_for_master");
			Map(x => x.TypeOfDepositCategory).Column("type_of_deposit").CustomType<TypeOfDepositCategoryStringType>();
			Map(x => x.SaleCategory).Column("subtype_of_equipment").CustomType<SaleCategoryStringType>();
			Map(x => x.OnlineStoreGuid).Column("online_store_guid");
			Map(x => x.MinStockCount).Column("min_stock_count");
			Map(x => x.MobileCatalog).Column("mobile_catalog").CustomType<MobileCatalogStringType>();
			Map(x => x.Description).Column("description");
			Map(x => x.BottleCapColor).Column("bottle_cap_color");
			Map(x => x.OnlineStoreExternalId).Column("online_store_external_id");
			Map(x => x.UsingInGroupPriceSet).Column("using_in_group_price_set");

			//Характеристики товара
			Map(x => x.Color).Column("color");
			Map(x => x.Material).Column("material");
			Map(x => x.Liters).Column("liters");
			Map(x => x.Size).Column("size");
			Map(x => x.Package).Column("package");
			Map(x => x.DegreeOfRoast).Column("degree_of_roast");
			Map(x => x.Smell).Column("smell");
			Map(x => x.Taste).Column("taste");
			Map(x => x.RefrigeratorCapacity).Column("refrigerator_capacity");
			Map(x => x.CoolingType).Column("cooling_type");
			Map(x => x.HeatingPower).Column("heating_power");
			Map(x => x.CoolingPower).Column("cooling_power");
			Map(x => x.HeatingPerformance).Column("heating_performance");
			Map(x => x.CoolingPerformance).Column("cooling_performance");
			Map(x => x.NumberOfCartridges).Column("number_of_cartridges");
			Map(x => x.CharacteristicsOfCartridges).Column("characteristics_of_cartridges");
			Map(x => x.CountryOfOrigin).Column("country_of_origin");
			Map(x => x.AmountInAPackage).Column("amount_in_a_package");

			Map(x => x.StorageCell).Column("storage_cell");

			//Планирование продаж для КЦ
			Map(x => x.PlanDay).Column("plan_day");
			Map(x => x.PlanMonth).Column("plan_month");

			//Честный знак
			Map(x => x.IsAccountableInChestniyZnak).Column("is_accountable_in_chestniy_znak");
			Map(x => x.Gtin).Column("gtin");

			References(x => x.ShipperCounterparty).Column("shipper_counterparty_id");
			References(x => x.CreatedBy).Column("created_by");
			References(x => x.DependsOnNomenclature).Column("depends_on_nomenclature");
			References(x => x.Unit).Column("unit_id").Not.LazyLoad();
			References(x => x.EquipmentColor).Column("color_id");
			References(x => x.Kind).Column("kind_id");
			References(x => x.Manufacturer).Column("manufacturer_id");
			References(x => x.RouteListColumn).Column("route_column_id");
			References(x => x.Folder1C).Column("folder_1c_id");
			References(x => x.ProductGroup).Column("group_id");
			References(x => x.FuelType).Column("fuel_type_id");
			References(x => x.OnlineStore).Column("online_store_id");

			HasMany(x => x.NomenclaturePrice).Inverse().Cascade.AllDeleteOrphan().LazyLoad().KeyColumn("nomenclature_id");
			HasMany(x => x.Images).Inverse().Cascade.AllDeleteOrphan().LazyLoad().KeyColumn("nomenclature_id");
			HasMany(x => x.CostPrices).Inverse().Cascade.AllDeleteOrphan().LazyLoad().KeyColumn("nomenclature_id");
			HasMany(x => x.PurchasePrices).Inverse().Cascade.AllDeleteOrphan().LazyLoad().KeyColumn("nomenclature_id");
			HasMany(x => x.InnerDeliveryPrices).Inverse().Cascade.AllDeleteOrphan().LazyLoad().KeyColumn("nomenclature_id");
		}
	}
}

