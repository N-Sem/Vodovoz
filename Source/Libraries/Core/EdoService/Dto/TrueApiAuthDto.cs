﻿using System.Text.Json.Serialization;

namespace EdoService.Dto
{
	public class TrueApiAuthDto
	{
		[JsonPropertyName("uuid")]
		public string Uuid { get; set; }

		[JsonPropertyName("data")]
		public string Data { get; set; }
	}
}
