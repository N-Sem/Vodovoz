using QS.DomainModel.Entity;

namespace Vodovoz.Domain.Roboats
{
	public class RoboatsStreet : PropertyChangedBase, IDomainObject
	{
		private string _name;
		private string _type;
		private string _audioFilename;

		public virtual int Id { get; set; }

		public virtual string Name
		{
			get => _name;
			set => SetField(ref _name, value);
		}

		public virtual string Type
		{
			get => _type;
			set => SetField(ref _type, value);
		}

		public virtual string AudioFileName
		{
			get => _audioFilename;
			set => SetField(ref _audioFilename, value);
		}

	}
}
