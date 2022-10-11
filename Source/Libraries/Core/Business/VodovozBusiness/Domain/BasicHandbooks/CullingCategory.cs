﻿using System;
using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;

namespace Vodovoz.Domain
{
	[Appellative (Gender = GrammaticalGender.Feminine,
		NominativePlural = "категории выбраковки",
		Nominative = "категория выбраковки")]
	public class CullingCategory : PropertyChangedBase, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		string name;

		[Required (ErrorMessage = "Название категории должно быть заполнено.")]
		[Display (Name = "Название")]
		public virtual string Name {
			get { return name; }
			set { SetField (ref name, value, () => Name); }
		}

		#endregion

		public CullingCategory ()
		{
			Name = String.Empty;
		}
	}
}

