using System;
using System.Linq;
using Vodovoz.EntityRepositories.Counterparties;

namespace RoboAtsService.Requests
{
	//public class LastOrderDataFirstNameCodeHandler : GetRequestHandlerBase
	//{
	//	private readonly RoboatsRepository _roboatsRepository;

	//	public override string Request => RoboatsRequestType.LastOrderDataFirstNameCode;

	//	public LastOrderDataFirstNameCodeHandler(RoboatsRepository roboatsRepository, RequestDto requestDto) : base(requestDto)
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


	//		var result = "NO DATA";

	//		return result;
	//	}
	//}

}
