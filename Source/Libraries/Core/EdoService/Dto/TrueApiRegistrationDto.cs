﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EdoService.Dto
{
	public class TrueApiRegistrationDto
	{
		[JsonPropertyName("is_registered")]
		public bool IsRegistered { get; set; }

		[JsonPropertyName("productGroups")]
		public IEnumerable<string> ProductGroups { get; set; }
	}
}
