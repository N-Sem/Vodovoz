﻿using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;

namespace Vodovoz.Domain.Employees
{
	[Appellative(Gender = GrammaticalGender.Masculine,
		NominativePlural = "Иностранные гражданства",
		Nominative = "Иностранное гражданство")]
	public class Citizenship: PropertyChangedBase, IDomainObject
	{
		public virtual int Id { get; set; }

		string name;

		[Required(ErrorMessage = "Название должно быть заполнено.")]
		[Display(Name = "Название")]
		public virtual string Name {
			get { return name; }
			set { SetField(ref name, value, () => Name); }
		}

		public Citizenship()
		{
			Name = string.Empty;
		}
	}
}
