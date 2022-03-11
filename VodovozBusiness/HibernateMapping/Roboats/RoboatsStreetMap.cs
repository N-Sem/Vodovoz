using FluentNHibernate.Mapping;
using Vodovoz.Domain.Roboats;

namespace Vodovoz.HibernateMapping.Roboats
{
	public class RoboatsStreetMap : ClassMap<RoboatsStreet>
	{
		public RoboatsStreetMap()
		{
			Table("roboats_streets");

			Id(x => x.Id).GeneratedBy.Native();

			Map(x => x.Name).Column("name");
			Map(x => x.Type).Column("type");
			Map(x => x.AudioFileName).Column("audio_filename");
		}
	}
}
