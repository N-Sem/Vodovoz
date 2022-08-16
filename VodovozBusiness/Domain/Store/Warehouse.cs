﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Bindings.Utilities;
using QS.DomainModel.Entity;
using QS.DomainModel.Entity.EntityPermissions;
using QS.HistoryLog;

namespace Vodovoz.Domain.Store
{
	[Appellative(Gender = GrammaticalGender.Masculine,
		NominativePlural = "склады",
		Nominative = "склад")]
	[EntityPermission]
	[HistoryTrace]
	public class Warehouse : PropertyChangedBase, IDomainObject, IValidatableObject
	{
		private Subdivision _movementDocumentsNotificationsSubdivisionRecipient;
		private string _name;

		public Warehouse() { }

		#region Свойства

		public virtual int Id { get; set; }

		[Required(ErrorMessage = "Название склада должно быть заполнено.")]
		[Display(Name = "Название")]
		public virtual string Name {
			get => _name;
			set => SetField(ref _name, value, () => Name);
		}

		bool canReceiveBottles;
		public virtual bool CanReceiveBottles {
			get => canReceiveBottles;
			set => SetField(ref canReceiveBottles, value, () => CanReceiveBottles);
		}

		bool canReceiveEquipment;
		public virtual bool CanReceiveEquipment {
			get => canReceiveEquipment;
			set => SetField(ref canReceiveEquipment, value, () => CanReceiveEquipment);
		}

		private bool publishOnlineStore;
		[Display(Name = "Публиковать в интернет магазине")]
		public virtual bool PublishOnlineStore {
			get => publishOnlineStore;
			set => SetField(ref publishOnlineStore, value, () => PublishOnlineStore);
		}

		private WarehouseUsing typeOfUse;
		[Display(Name = "Тип использования")]
		public virtual WarehouseUsing TypeOfUse {
			get => typeOfUse;
			set => SetField(ref typeOfUse, value, () => TypeOfUse);
		}

		private bool isArchive;
		[Display(Name = "Архивный")]
		public virtual bool IsArchive {
			get => isArchive;
			set => SetField(ref isArchive, value, () => IsArchive);
		}

		Subdivision owningSubdivision;
		[Display(Name = "Подразделение-владелец")]
		public virtual Subdivision OwningSubdivision {
			get => owningSubdivision;
			set => SetField(ref owningSubdivision, value, () => OwningSubdivision);
		}

		[Display(Name = "Подразделение-получатель уведомлений о перемещениях на данный склад")]
		public virtual Subdivision MovementDocumentsNotificationsSubdivisionRecipient
		{
			get => _movementDocumentsNotificationsSubdivisionRecipient;
			set => SetField(ref _movementDocumentsNotificationsSubdivisionRecipient, value);
		}

		#endregion

		#region IValidatableObject implementation
		public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if(OwningSubdivision == null)
				yield return new ValidationResult(
					"К складу должно быть привязано \"Подразделение-владелец\"",
					new[] { this.GetPropertyName(o => o.OwningSubdivision) }
				);
		}
		#endregion
	}

	public enum WarehouseUsing
	{
		[Display(Name = "Отгрузка")]
		Shipment,
		[Display(Name = "Производство")]
		Production,
	}

	public class WarehouseUsingStringType : NHibernate.Type.EnumStringType
	{
		public WarehouseUsingStringType() : base(typeof(WarehouseUsing)) { }
	}
}
