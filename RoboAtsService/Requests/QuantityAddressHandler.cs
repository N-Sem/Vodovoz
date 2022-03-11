using System;
using System.Linq;
using Vodovoz.EntityRepositories.Counterparties;

namespace RoboAtsService.Requests
{
	//public class QuantityAddressHandler : GetRequestHandlerBase
	//{
	//	private readonly RoboatsRepository _roboatsRepository;

	//	public override string Request => RoboatsRequestType.QuantityAddress;

	//	public QuantityAddressHandler(RoboatsRepository roboatsRepository, RequestDto requestDto) : base(requestDto)
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


	//		var deliveryPointIds = _roboatsRepository.GetDeliveryPointIds(counterpartyIds.First());
	//		string result;
	//		if(deliveryPointIds.Any())
	//		{
	//			result = string.Join('|', deliveryPointIds);
	//		}
	//		else
	//		{
	//			result = "NO DATA";
	//		}

	//		return result;
	//	}
	//}

}
