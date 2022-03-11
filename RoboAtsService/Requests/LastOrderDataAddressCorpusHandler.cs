using System;
using System.Linq;
using System.Text.RegularExpressions;
using Vodovoz.EntityRepositories.Counterparties;

namespace RoboAtsService.Requests
{
	//public class LastOrderDataAddressCorpusHandler : GetRequestHandlerBase
	//{
	//	private readonly RoboatsRepository _roboatsRepository;

	//	public override string Request => RoboatsRequestType.LastOrderDataAddressCorp;

	//	public LastOrderDataAddressCorpusHandler(RoboatsRepository roboatsRepository, RequestDto requestDto) : base(requestDto)
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
	//			return GetExactAddressCorpus(AddressId.Value, counterpartyId);
	//		}
	//		else
	//		{
	//			return GetLastOrderAddressCorpus(counterpartyId);
	//		}
	//	}

	//	private string GetExactAddressCorpus(int addressId, int counterpartyId)
	//	{
	//		var deliveryPointBuilding = _roboatsRepository.GetDeliveryPointBuilding(addressId, counterpartyId);
	//		var result = GetCorpusNumber(deliveryPointBuilding);

	//		return result;
	//	}

	//	private string GetLastOrderAddressCorpus(int counterpartyId)
	//	{
	//		var deliveryPointBuilding = _roboatsRepository.GetLastOrderDataAddressBuilding(counterpartyId);
	//		var result = GetCorpusNumber(deliveryPointBuilding);

	//		return result;
	//	}

	//	private string GetCorpusNumber(string fullBuildingNumber)
	//	{
	//		if(string.IsNullOrWhiteSpace(fullBuildingNumber))
	//		{
	//			return "NO DATA";
	//		}

	//		Regex regex = new Regex(@"((к[ ]*[.]?[ ]*\d+)|(кор[ ]*[.]?[ ]*\d+)|(корпус[ ]*[.]?[ ]*\d+)){1,}");
	//		var match = regex.Match(fullBuildingNumber);
	//		if(!match.Success)
	//		{
	//			return "NO DATA";
	//		}
	//		var result = match.Value;
	//		return result;
	//	}
	//}

}
