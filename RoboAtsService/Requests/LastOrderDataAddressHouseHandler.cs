using System;
using System.Linq;
using System.Text.RegularExpressions;
using Vodovoz.EntityRepositories.Counterparties;

namespace RoboAtsService.Requests
{
	//public class LastOrderDataAddressHouseHandler : GetRequestHandlerBase
	//{
	//	private readonly RoboatsRepository _roboatsRepository;

	//	public override string Request => RoboatsRequestType.LastOrderDataAddressHouse;

	//	public LastOrderDataAddressHouseHandler(RoboatsRepository roboatsRepository, RequestDto requestDto) : base(requestDto)
	//	{
	//		_roboatsRepository = roboatsRepository ?? throw new ArgumentNullException(nameof(roboatsRepository));
	//	}

	//	public override string Execute()
	//	{
	//		var counterpartyIds = _roboatsRepository.GetCounterpartyIdsByPhone(ClientPhone);
	//		if(counterpartyIds.Count() != 1)
	//		{
	//			return ErrorMessage;
	//		}

	//		var counterpartyId = counterpartyIds.First();

	//		if(AddressId.HasValue)
	//		{
	//			return GetExactAddressBuilding(AddressId.Value, counterpartyId);
	//		}
	//		else
	//		{
	//			return GetLastOrderAddressBuilding(counterpartyId);
	//		}
	//	}

	//	private string GetExactAddressBuilding(int addressId, int counterpartyId)
	//	{
	//		var deliveryPointBuilding = _roboatsRepository.GetDeliveryPointBuilding(addressId, counterpartyId);
	//		var result = GetHouseNumber(deliveryPointBuilding);

	//		return result;
	//	}

	//	private string GetLastOrderAddressBuilding(int counterpartyId)
	//	{
	//		var deliveryPointBuilding = _roboatsRepository.GetLastOrderDataAddressBuilding(counterpartyId);
	//		var result = GetHouseNumber(deliveryPointBuilding);

	//		return result;
	//	}

	//	private string GetHouseNumber(string fullBuildingNumber)
	//	{
	//		if(string.IsNullOrWhiteSpace(fullBuildingNumber))
	//		{
	//			return ErrorMessage;
	//		}

	//		Regex regex = new Regex("\\d{1,}");
	//		var match = regex.Match(fullBuildingNumber);
	//		if(!match.Success)
	//		{
	//			return ErrorMessage;
	//		}
	//		var result = match.Value;
	//		return result;
	//	}
	//}

}
