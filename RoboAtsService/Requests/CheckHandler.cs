using QS.Osm.DTO;
using System;
using System.Linq;
using Vodovoz.EntityRepositories.Counterparties;

namespace RoboAtsService.Requests
{
	/*
	public class CheckHandler : GetRequestHandlerBase
	{
		private readonly RoboatsRepository _roboatsRepository;

		public override string Request => RoboatsRequestType.Check;

		public CheckType RequestCheckType { get; }

		public CheckHandler(RoboatsRepository roboatsRepository, RequestDto requestDto) : base(requestDto)
		{
			_roboatsRepository = roboatsRepository ?? throw new ArgumentNullException(nameof(roboatsRepository));

			switch(RequestDto.CheckType)
			{
				case "flat":
					RequestCheckType = CheckType.RoomType;
					break;
				case "vip":
					RequestCheckType = CheckType.ClientVIP;
					break;
				case "orderondate":
					RequestCheckType = CheckType.OrderOnDate;
					break;
				case "requesttime":
					RequestCheckType = CheckType.AvailableDeliveryIntervals;
					break;
				default:
					RequestCheckType = CheckType.ClientExists;
					break;
			}
		}

		public override string Execute()
		{
			var counterpartyIds = _roboatsRepository.GetCounterpartyIdsByPhone(ClientPhone);
			var counterpartyCount = counterpartyIds.Count();
			if(counterpartyCount > 1)
			{
				return ErrorMessage;
			}

			if(counterpartyCount == 0)
			{
				if(RequestCheckType == CheckType.ClientExists)
				{
					return "0";
				}
				else
				{
					return ErrorMessage;
				}
			}

			var counterpartyId = counterpartyIds.First();
			var result = "NO DATA";

			switch(RequestCheckType)
			{
				case CheckType.RoomType:
					result = GetRoomType(counterpartyId);
					break;
				case CheckType.ClientVIP:
					result = GetClientVIPStatus();
					break;
				case CheckType.OrderOnDate:
					result = GetOrderOnDate();
					break;
				case CheckType.AvailableDeliveryIntervals:
					result = GetAvailableDeliveryIntervals();
					break;
			}

			return result;
		}
		private string GetRoomType(int counterpartyId)
		{
			RoomType roomType;
			if(AddressId.HasValue)
			{
				roomType = _roboatsRepository.GetDeliveryPointRoomType(AddressId.Value, counterpartyId);
			}
			else
			{
				roomType = _roboatsRepository.GetLastOrderDeliveryPointRoomType(counterpartyId);
			}

			return "TEST";
		}

		private string GetClientVIPStatus()
		{
			return "0";
		}

		private string GetOrderOnDate()
		{
			throw new NotImplementedException();
		}

		private string GetAvailableDeliveryIntervals()
		{
			throw new NotImplementedException();
		}

		public enum CheckType
		{
			ClientExists,
			RoomType,
			ClientVIP,
			OrderOnDate,
			AvailableDeliveryIntervals
		}
	}
	*/
}
