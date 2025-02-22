﻿using DriverAPI.Library.DTOs;
using Vodovoz.Domain.Logistic;

namespace DriverAPI.Library.Converters
{
	public class RouteListCompletionStatusConverter
	{
		public RouteListDtoCompletionStatus convertToAPIRouteListCompletionStatus(RouteListStatus routeListStatus)
		{
			switch (routeListStatus)
			{
				case RouteListStatus.New:
				case RouteListStatus.Confirmed:
				case RouteListStatus.InLoading:
				case RouteListStatus.EnRoute:
					return RouteListDtoCompletionStatus.Incompleted;
				case RouteListStatus.Delivered:
				case RouteListStatus.OnClosing:
				case RouteListStatus.MileageCheck:
				case RouteListStatus.Closed:
					return RouteListDtoCompletionStatus.Completed;
				default:
					throw new ConverterException(nameof(routeListStatus), routeListStatus, $"Значение { routeListStatus } не поддерживается");
			}
		}
	}
}
