﻿using System.Xml.Serialization;

namespace FastPaymentsAPI.Library.DTO_s.Responses
{
	[XmlRoot(ElementName = "order_response")]
	public class OrderRegistrationResponseDTO
	{
		[XmlElement(ElementName = "id")]
		public long Id { get; set; }
		
		[XmlElement(ElementName = "ticket")]
		public string Ticket { get; set; }
		
		[XmlElement(ElementName = "ok_code")]
		public string OkCode { get; set; }
		
		[XmlElement(ElementName = "failure_code")]
		public string FailureCode { get; set; }
		
		[XmlElement(ElementName = "response_code")]
		public int ResponseCode { get; set; }
		
		[XmlElement(ElementName = "response_message")]
		public string ResponseMessage { get; set; }
		
		[XmlElement(ElementName = "qr_png_base64")]
		public string QRPngBase64 { get; set; }
		
		[XmlElement(ElementName = "qrUrl")]
		public string QRUrl { get; set; }
		
		[XmlIgnore]
		public string ErrorMessage { get; set; }
	}
}
