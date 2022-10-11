﻿using System;
using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;

namespace Vodovoz.Domain.Employees
{
	[Appellative (Gender = GrammaticalGender.Feminine,
		NominativePlural = "национальности",
		Nominative = "национальность")]
	public class Nationality : PropertyChangedBase, IDomainObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		string name;

		[Required (ErrorMessage = "Название должно быть заполнено.")]
		[Display (Name = "Название")]
		public virtual string Name {
			get { return name; }
			set { SetField (ref name, value, () => Name); }
		}

		#endregion


		public Nationality ()
		{
			Name = String.Empty;
		}
	}
}

