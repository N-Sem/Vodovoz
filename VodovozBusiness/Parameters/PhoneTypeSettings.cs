using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vodovoz.Services;

namespace Vodovoz.Parameters
{
	public class PhoneTypeSettings : IPhoneTypeSettings
	{
		private readonly IParametersProvider _parametersProvider;
		public PhoneTypeSettings(IParametersProvider parametersProvider)
		{
			_parametersProvider = parametersProvider;
		}

		public int ArchiveId => _parametersProvider.GetIntValue("phone_type_archive_id");
	}
}
