using System;
using System.Linq;
using Vodovoz.EntityRepositories.Counterparties;

namespace RoboAtsService.Requests
{
	//public class LastOrderDataReturnHandler : GetRequestHandlerBase
	//{
	//	private readonly RoboatsRepository _roboatsRepository;

	//	public override string Request => RoboatsRequestType.LastOrderDataReturn;

	//	public LastOrderDataReturnHandler(RoboatsRepository roboatsRepository, RequestDto requestDto) : base(requestDto)
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


	//		var bottlesReturn = _roboatsRepository.GetLastOrderBottlesReturnCount(counterpartyIds.First());

	//		return $"{bottlesReturn}";
	//	}
	//}

}
