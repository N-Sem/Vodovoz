﻿using System;
using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;

namespace Vodovoz.Domain.Operations
{
	public class OperationBase: PropertyChangedBase, IDomainObject, IValidatableObject
	{
		public virtual int Id { get; set; }

		DateTime operationTime;
		public virtual DateTime OperationTime {
			get => operationTime;
			set => SetField (ref operationTime, value);
		}

		#region IValidatableObject implementation

		public virtual System.Collections.Generic.IEnumerable<ValidationResult> Validate (ValidationContext validationContext)
		{
			return null;
		}

		#endregion
	}
}

