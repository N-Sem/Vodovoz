﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NHibernate;
using NHibernate.Criterion;
using QS.DomainModel.Entity;
using QS.DomainModel.Entity.EntityPermissions;
using Vodovoz.Domain.Client;

namespace Vodovoz.Domain.Goods
{
	/// <summary>
	/// Оборудование только для посерийного учета
	/// </summary>
	[Appellative (Gender = GrammaticalGender.Neuter,
		NominativePlural = "оборудование",
		Nominative = "оборудование",
		Prepositional = "оборудовании"
	)]
	[EntityPermission]
	public class Equipment: PropertyChangedBase, IDomainObject, IValidatableObject
	{
		#region Свойства

		public virtual int Id { get; set; }

		bool onDuty;

		[Display (Name = "Дежурный куллер")]
		public virtual bool OnDuty {
			get { return onDuty; }
			set { SetField (ref onDuty, value, () => OnDuty); }
		}

		string serial;

		[Display (Name = "Серийный номер")]
		public virtual string Serial {
			get { return Id > 0 ? Id.ToString () : "не определён"; }
			set { SetField (ref serial, value, () => Serial); }
		}

		string comment;

		[Display (Name = "Комментарий")]
		public virtual string Comment {
			get { return comment; }
			set { SetField (ref comment, value, () => Comment); }
		}

		Nomenclature nomenclature;

		[Display (Name = "Номенклатура")]
		public virtual Nomenclature Nomenclature {
			get { return nomenclature; }
			set { SetField (ref nomenclature, value, () => Nomenclature); }
		}

		DateTime lastServiceDate;

		[Display (Name = "Последняя сан. обработка")]
		public virtual DateTime LastServiceDate {
			get { return lastServiceDate; }
			set { SetField (ref lastServiceDate, value, () => LastServiceDate); }
		}

		DateTime? warrantyEndDate;

		[Display (Name = "Окончание гарантии")]
		public virtual DateTime? WarrantyEndDate {
			get { return warrantyEndDate; }
			set { SetField (ref warrantyEndDate, value, () => WarrantyEndDate); }
		}

		Counterparty assignedToClient;

		[Display (Name = "Привязан к клиенту")]
		public virtual Counterparty AssignedToClient {
			get { return assignedToClient; }
			set {
				SetField (ref assignedToClient, value, () => AssignedToClient); 
			}
		}

		#endregion

		public virtual DateTime? NextServiceDate {
			get{ 
				if (LastServiceDate == DateTime.MinValue)
					return null;
				return LastServiceDate.AddMonths(6);
			}
		}

		public virtual string Title { 
			get { return Nomenclature == null ? String.Empty : 
				String.Format ("{0} (с/н: {1})", 
					String.IsNullOrWhiteSpace(Nomenclature.Model) ? Nomenclature.Name : Nomenclature.Model,
					Serial); } 
		}

		[Display (Name = "Наименование")]
		public virtual string NomenclatureName { get { return Nomenclature == null ? String.Empty : Nomenclature.Name; } }

		public Equipment ()
		{
			Comment = String.Empty;
		}

		#region IValidatableObject implementation

		public virtual IEnumerable<ValidationResult> Validate (ValidationContext validationContext)
		{
			if (LastServiceDate > DateTime.Now)
				yield return new ValidationResult ("Дата последней санитарной обработки не может быть в будущем.");
			if (Nomenclature == null)
				yield return new ValidationResult ("Должна быть указана номенклатура.");
		}

		#endregion
	}
}

