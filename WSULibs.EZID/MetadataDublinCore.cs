using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSULibs.EZID
{
	public class MetadataDublinCore : Metadata
	{
		public string Creator { get; set; }

		public string Title { get; set; }

		public string Publisher { get; set; }

		public DateTime? Date { get; set; }

		public string DateFormat { get; set; }

		public MetadataDublinCore()
		{
			this.DateFormat = "yyyy-MM-dd";
			this.Profile = Metadata.MetadataTypes.DublinCore;
		}

		public MetadataDublinCore(IDictionary<string, string> map)
			: base(map)
		{
			this.DateFormat = "yyyy-MM-dd";
			this.Profile = Metadata.MetadataTypes.DublinCore;

			if (map.ContainsKey(MetadataKeys.Creator))
				this.Creator = map[MetadataKeys.Creator];

			if (map.ContainsKey(MetadataKeys.Title))
				this.Title = map[MetadataKeys.Title];

			if (map.ContainsKey(MetadataKeys.Publisher))
				this.Publisher = map[MetadataKeys.Publisher];

			if (map.ContainsKey(MetadataKeys.Date))
				this.Date = DateTime.Parse(map[MetadataKeys.Date]);
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

			if (this.Date != null && this.Date.HasValue)
				map.Add(MetadataKeys.Date, this.Date.Value.ToString(this.DateFormat));

			return map;
		}

		public static new class MetadataKeys
		{
			public const string Creator = "dc.creator";
			public const string Title = "dc.title";
			public const string Publisher = "dc.publisher";
			public const string Date = "dc.date";
		}
	}
}
