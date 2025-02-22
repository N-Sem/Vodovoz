﻿using Vodovoz.Domain.FastPayments;

namespace FastPaymentsAPI.Library.DTO_s.Responses
{
	public class QRResponseDTO : IErrorResponse
	{
		public string QRCode { get; set; }
		public string ErrorMessage { get; set; }
		public FastPaymentStatus FastPaymentStatus { get; set; }
	}
}
