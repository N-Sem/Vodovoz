using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vodovoz.Parameters
{
	public class RoboatsSettings
	{
		private readonly IParametersProvider _parametersProvider;

		public RoboatsSettings(IParametersProvider parametersProvider)
		{
			_parametersProvider = parametersProvider ?? throw new ArgumentNullException(nameof(parametersProvider));
		}

		public string DeliverySchedulesAudiofilesFolder => _parametersProvider.GetStringValue("roboats_delivery_schedules_audiofiles_path");
		public string AddressesAudiofilesFolder => _parametersProvider.GetStringValue("roboats_addresses_audiofiles_path");
		public string WaterTypesAudiofilesFolder => _parametersProvider.GetStringValue("roboats_water_types_audiofiles_path");

	}
}
