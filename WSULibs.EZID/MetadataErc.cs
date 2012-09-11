using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSULibs.EZID
{
	public class MetadataErc : Metadata
	{
		public string Who { get; set; }

		public string What { get; set; }

		public string When { get; set; }

		public MetadataErc()
		{
			this.Profile = Metadata.MetadataTypes.ERC;
		}

		public MetadataErc(IDictionary<string, string> map)
			: base(map)
		{
			this.Profile = Metadata.MetadataTypes.ERC;

			if (map.ContainsKey(MetadataKeys.Who))
				this.Who = map[MetadataKeys.Who];

			if (map.ContainsKey(MetadataKeys.What))
				this.What = map[MetadataKeys.What];

			if (map.ContainsKey(MetadataKeys.When))
				this.When = map[MetadataKeys.When];
		}

		public override IDictionary<string, string> AsDictionary()
		{
			var map = base.AsDictionary();

			if (!String.IsNullOrWhiteSpace(this.Who))
				map.Add(MetadataKeys.Who, this.Who);

			if (!String.IsNullOrWhiteSpace(this.What))
				map.Add(MetadataKeys.What, this.What);

			if (!String.IsNullOrWhiteSpace(this.When))
				map.Add(MetadataKeys.When, this.When);

			return map;
		}

		public static new class MetadataKeys
		{
			public const string Who = "erc.who";
			public const string What = "erc.what";
			public const string When = "erc.when";
		}
	}
}
