using System;
using System.Linq;
using System.Text.RegularExpressions;
using Vodovoz.EntityRepositories.Counterparties;

namespace RoboAtsService.Requests
{
	//public class LastOrderDataAddressApartmentHandler : GetRequestHandlerBase
	//{
	//	private readonly RoboatsRepository _roboatsRepository;

	//	public override string Request => RoboatsRequestType.LastOrderDataAddressApartment;

	//	public LastOrderDataAddressApartmentHandler(RoboatsRepository roboatsRepository, RequestDto requestDto) : base(requestDto)
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
	//			return GetExactAddressApartment(AddressId.Value, counterpartyId);
	//		}
	//		else
	//		{
	//			return GetLastOrderAddressApartment(counterpartyId);
	//		}
	//	}

	//	private string GetExactAddressApartment(int addressId, int counterpartyId)
	//	{
	//		var deliveryPointApartment = _roboatsRepository.GetDeliveryPointApartment(addressId, counterpartyId);
	//		var result = GetApartmentNumber(deliveryPointApartment);

	//		return result;
	//	}

	//	private string GetLastOrderAddressApartment(int counterpartyId)
	//	{
	//		var deliveryPointBuilding = _roboatsRepository.GetLastOrderDataAddressApartment(counterpartyId);
	//		var house = GetApartmentNumber(deliveryPointBuilding);

	//		return house;
	//	}

	//	private string GetApartmentNumber(string fullApartmentNumber)
	//	{
	//		if(string.IsNullOrWhiteSpace(fullApartmentNumber))
	//		{
	//			return ErrorMessage;
	//		}

	//		Regex regex = new Regex("\\d{1,}");
	//		var match = regex.Match(fullApartmentNumber);
	//		if(!match.Success)
	//		{
	//			return ErrorMessage;
	//		}
	//		var house = match.Value;
	//		return house;
	//	}
	//}

}
