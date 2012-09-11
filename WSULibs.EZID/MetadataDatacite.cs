using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSULibs.EZID
{
	public class MetadataDatacite : Metadata
	{
		public string Creator { get; set; }

		public string Title { get; set; }

		public string Publisher { get; set; }

		public int PublicationYear { get; set; }

		public string ResourceType { get; set; }

		public MetadataDatacite()
		{
			this.Profile = Metadata.MetadataTypes.Datacite;
		}

		public MetadataDatacite(IDictionary<string, string> map)
			: base(map)
		{
			this.Profile = Metadata.MetadataTypes.Datacite;

			if (map.ContainsKey(MetadataKeys.Creator))
				this.Creator = map[MetadataKeys.Creator];

			if (map.ContainsKey(MetadataKeys.Title))
				this.Title = map[MetadataKeys.Title];

			if (map.ContainsKey(MetadataKeys.Publisher))
				this.Publisher = map[MetadataKeys.Publisher];

			if (map.ContainsKey(MetadataKeys.PublicationYear))
			{
				int publicationYear;
				if (int.TryParse(map[MetadataKeys.PublicationYear], out publicationYear))
					this.PublicationYear = publicationYear;
			}

			if (map.ContainsKey(MetadataKeys.ResourceType))
				this.ResourceType = map[MetadataKeys.ResourceType];
		}

		public override IDictionary<string, string> AsDictionary()
		{
			var map = base.AsDictionary();

			if (!String.IsNullOrWhiteSpace(this.Creator))
				map.Add(MetadataKeys.Creator, this.Creator);

			if (!String.IsNullOrWhiteSpace(this.Title))
				map.Add(MetadataKeys.Title, this.Title);

			if (!String.IsNullOrWhiteSpace(this.Publisher))
				map.Add(MetadataKeys.Publisher, this.Publisher);

			if (!String.IsNullOrWhiteSpace(this.PublicationYear.ToString()) && this.PublicationYear.ToString().Length == 4)
				map.Add(MetadataKeys.PublicationYear, this.PublicationYear.ToString());

			if (!String.IsNullOrWhiteSpace(this.ResourceType))
				map.Add(MetadataKeys.ResourceType, this.ResourceType);

			return map;
		}

		public static new class MetadataKeys
		{
			public const string Creator = "datacite.creator";
			public const string Title = "datacite.title";
			public const string Publisher = "datacite.publisher";
			public const string PublicationYear = "datacite.publicationyear";
			public const string ResourceType = "datacite.resourcetype";
		}
	}
}
