using QS.DomainModel.UoW;
using System.Linq;
using Vodovoz.EntityRepositories.Counterparties;

namespace RoboAtsService.Requests
{
	//public class LastOrderBottleCountHandler : GetRequestHandlerBase
	//{
	//	public override string Request => RoboatsRequestType.LastOrderDataBottles;

	//	public LastOrderBottleCountHandler(RequestDto requestDto) : base(requestDto)
	//	{
	//	}

	//	public override string Execute()
	//	{
	//		var repo = new RoboatsRepository(UnitOfWorkFactory.GetDefaultFactory);
	//		var counterpartyIds = repo.GetCounterpartyIdsByPhone(ClientPhone);
	//		if(counterpartyIds.Count() != 1)
	//		{
	//			return ErrorMessage;
	//		}


	//		var bottles =  repo.GetLastOrderBottlesCount(counterpartyIds.First());

	//		return $"{bottles}";
	//	}
	//}

}
