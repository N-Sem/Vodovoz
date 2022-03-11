using System;
using System.Globalization;

namespace RoboAtsService.Requests
{
	/*
	public class AddOrderHandler : RequestHandlerBase
	{
		public bool IsAddOrder { get; }
		public PostType? OrderPostType { get; }
		public DateTime? Date { get; }
		public string Time { get; }

		public override string ErrorMessage
		{
			get
			{
				var value = OrderPostType == PostType.CreateOrder ? "1" : "0";
				return $"ERROR. order={value}";
			}
		}

		protected AddOrderHandler(RequestDto requestDto) : base(requestDto)
		{
			if(string.IsNullOrWhiteSpace(requestDto.IsAddOrder))
			{
				IsAddOrder = false;
			}
			else
			{
				if(bool.TryParse(requestDto.IsAddOrder, out bool isAddOrder))
				{
					IsAddOrder = isAddOrder;
				}
			}

			switch(requestDto.IsOrderOrPriceCheck)
			{
				case "1":
					OrderPostType = PostType.CreateOrder;
					break;
				case "0":
					OrderPostType = PostType.PriceCheck;
					break;
				default:
					OrderPostType = null;
					break;
			}

			if(DateTime.TryParseExact(requestDto.Date, "yyyy-MM-dd", new DateTimeFormatInfo(), DateTimeStyles.None, out DateTime date))
			{
				Date = date;
			}

			Time = requestDto.Time;
		}

		public override string Execute()
		{
			return "NULL HANDLER";
		}

		public enum PostType
		{
			CreateOrder,
			PriceCheck
		}
	}
	*/
}
