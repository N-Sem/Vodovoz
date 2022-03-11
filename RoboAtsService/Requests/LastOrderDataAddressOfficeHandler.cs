using System;
using System.Linq;
using System.Text.RegularExpressions;
using Vodovoz.EntityRepositories.Counterparties;

namespace RoboAtsService.Requests
{
	//public class LastOrderDataAddressOfficeHandler : GetRequestHandlerBase
	//{
	//	private readonly RoboatsRepository _roboatsRepository;

	//	public override string Request => RoboatsRequestType.LastOrderDataAddressOffice;

	//	public LastOrderDataAddressOfficeHandler(RoboatsRepository roboatsRepository, RequestDto requestDto) : base(requestDto)
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
	//			return GetExactAddressOffice(AddressId.Value, counterpartyId);
	//		}
	//		else
	//		{
	//			return GetLastOrderAddressOffice(counterpartyId);
	//		}
	//	}

	//	private string GetExactAddressOffice(int addressId, int counterpartyId)
	//	{
	//		var deliveryPointOffice = _roboatsRepository.GetDeliveryPointOffice(addressId, counterpartyId);
	//		var result = GetOfficeNumber(deliveryPointOffice);

	//		return result;
	//	}

	//	private string GetLastOrderAddressOffice(int counterpartyId)
	//	{
	//		var deliveryPointBuilding = _roboatsRepository.GetLastOrderDataAddressOffice(counterpartyId);
	//		var house = GetOfficeNumber(deliveryPointBuilding);

	//		return house;
	//	}

	//	private string GetOfficeNumber(string fullOfficeNumber)
	//	{
	//		if(string.IsNullOrWhiteSpace(fullOfficeNumber))
	//		{
	//			return ErrorMessage;
	//		}

	//		Regex regex = new Regex("\\d{1,}");
	//		var match = regex.Match(fullOfficeNumber);
	//		if(!match.Success)
	//		{
	//			return ErrorMessage;
	//		}
	//		var house = match.Value;
	//		return house;
	//	}
	//}

}
