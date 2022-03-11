using QS.Utilities.Text;
using System;
using System.Linq;
using Vodovoz.EntityRepositories.Counterparties;

namespace RoboAtsService.Requests
{
	//public class LastOrderDataClientNameHandler : GetRequestHandlerBase
	//{
	//	private static string[] _availableRequestTypes = new[] {
	//		RoboatsRequestType.LastOrderDataFirstName,
	//		RoboatsRequestType.LastOrderDataLastName,
	//		RoboatsRequestType.LastOrderDataPatronymic
	//	};

	//	private readonly RoboatsRepository _roboatsRepository;

	//	private readonly string _requestType;
	//	public override string Request => _requestType;

	//	public LastOrderDataClientNameHandler(RoboatsRepository roboatsRepository, RequestDto requestDto) : base(requestDto)
	//	{
	//		_roboatsRepository = roboatsRepository ?? throw new ArgumentNullException(nameof(roboatsRepository));

	//		if(!_availableRequestTypes.Contains(requestDto.RequestType))
	//		{
	//			throw new InvalidOperationException("Не правильный тип запроса");
	//		}

	//		_requestType = requestDto.RequestType;
	//	}

	//	public override string Execute()
	//	{
	//		string noData = "NO DATA";

	//		var counterpartyIds = _roboatsRepository.GetCounterpartyIdsByPhone(ClientPhone);
	//		if(counterpartyIds.Count() != 1)
	//		{
	//			return ErrorMessage;
	//		}

	//		var countrpartyFullName = _roboatsRepository.GetCounterpartyFullName(counterpartyIds.First());
	//		PersonHelper.SplitFullName(countrpartyFullName, out string lastName, out string firstName, out string patronymic);

	//		string result;

	//		switch(_requestType)
	//		{
	//			case RoboatsRequestType.LastOrderDataFirstName:
	//				result = firstName;
	//				break;
	//			case RoboatsRequestType.LastOrderDataLastName:
	//				result = lastName;
	//				break;
	//			case RoboatsRequestType.LastOrderDataPatronymic:
	//				result = patronymic;
	//				break;
	//			default:
	//				result = noData;
	//				break;
	//		}

	//		if(string.IsNullOrWhiteSpace(result))
	//		{
	//			result = noData;
	//		}

	//		return result;
	//	}
	//}

}
