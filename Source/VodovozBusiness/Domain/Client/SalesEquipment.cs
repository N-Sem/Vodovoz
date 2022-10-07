﻿using System;
using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;
using Vodovoz.Domain.Goods;

namespace Vodovoz.Domain.Client
{
	[Appellative(Gender = GrammaticalGender.Feminine,
		NominativePlural = "оборудование для продажи",
		Nominative = "оборудование для продажи")]
	public class SalesEquipment : PropertyChangedBase, IDomainObject
	{
		public virtual int Id { get; set; }

		public virtual string Title {
			get {
				return $"{Nomenclature.Name} - {Price}";
			}
		}

		AdditionalAgreement additionalAgreement;

		[Display(Name = "Соглашение")]
		public virtual AdditionalAgreement AdditionalAgreement {
			get { return additionalAgreement; }
			set { SetField(ref additionalAgreement, value, () => AdditionalAgreement); }
		}

		Nomenclature nomenclature;

		[Display(Name = "Номенклатура")]
		public virtual Nomenclature Nomenclature {
			get { return nomenclature; }
			set { SetField(ref nomenclature, value, () => Nomenclature); }
		}

		Decimal price;

		[Display(Name = "Цена")]
		public virtual Decimal Price {
			get { return price; }
			set { SetField(ref price, value, () => Price); }
		}

		int count;

		[Display(Name = "Количество")]
		public virtual int Count {
			get { return count; }
			set { SetField(ref count, value, () => Count); }
		}

	}
}
