using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSULibs.EZID
{
	/// <summary>
	/// Represents metadata values common to all metadata types
	/// </summary>
	public class Metadata
	{
		public static string DetermineType(IDictionary<string, string> map)
		{
			if (map.ContainsKey(MetadataKeys.Profile))
				return map[MetadataKeys.Profile];

			return MetadataTypes.Basic;
		}

		public string Owner { get; private set; }

		public string OwnerGroup { get; private set; }

		public List<string> CoOwners { get; set; }

		public long Created { get; private set; }

		public long Updated { get; private set; }

		public string Target { get; set; }

		public string Profile { get; set; }

		public string Status { get; set; }

		/// <summary>
		/// Initilize base metadata object
		/// </summary>
		/// <param name="map">Dictionary of key/value metadata pairs</param>
		public Metadata(IDictionary<string, string> map = null)
		{
			if (null != map)
			{
				// parse dictionary into metadata object
				if (map.ContainsKey(MetadataKeys.Owner))
					this.Owner = map[MetadataKeys.Owner];

				if (map.ContainsKey(MetadataKeys.OwnerGroup))
					this.OwnerGroup = map[MetadataKeys.OwnerGroup];

				if (map.ContainsKey(MetadataKeys.CoOwners))
					this.CoOwners = map[MetadataKeys.CoOwners].Split(';').ToList();

				if (map.ContainsKey(MetadataKeys.Created))
				{
					long created;
					if (long.TryParse(map[MetadataKeys.Created], out created))
						this.Created = created;
				}

				if (map.ContainsKey(MetadataKeys.Updated))
				{
					long updated;
					if (long.TryParse(map[MetadataKeys.Updated], out updated))
						this.Updated = updated;
				}

				if (map.ContainsKey(MetadataKeys.Target))
					this.Target = map[MetadataKeys.Target];

				if (map.ContainsKey(MetadataKeys.Status))
					this.Status = map[MetadataKeys.Status];
			}
		}

		/// <summary>
		/// Convert metadata object to a dictionary
		/// </summary>
		/// <returns>A dictionary of key/value metadata pairs</returns>
		public virtual IDictionary<string, string> AsDictionary()
		{
			var map = new Dictionary<string, string>();

			if (this.CoOwners != null && this.CoOwners.Count > 0)
				map.Add(Metadata.MetadataKeys.CoOwners, String.Join(";", this.CoOwners.ToArray()));

			if (!String.IsNullOrWhiteSpace(this.Target))
				map.Add(MetadataKeys.Target, this.Target);

			if (!String.IsNullOrWhiteSpace(this.Profile))
				map.Add(MetadataKeys.Profile, this.Profile);

			if (!String.IsNullOrWhiteSpace(this.Status))
				map.Add(MetadataKeys.Status, this.Status);

			return map;
		}

		/// <summary>
		/// Collection of EZID internal metadata keys
		/// </summary>
		public static class MetadataKeys
		{
			public const string Owner = "_owner";
			public const string OwnerGroup = "_ownergroup";
			public const string CoOwners = "_coowners";
			public const string Created = "_created";
			public const string Updated = "_updated";
			public const string Target = "_target";
			public const string Shadows = "_shadows";
			public const string ShadowedBy = "_shadowedby";
			public const string Profile = "_profile";
			public const string Status = "_status";
		}

		/// <summary>
		/// Collection of EZID metadata types
		/// </summary>
		public static class MetadataTypes
		{
			public const string Basic = "basic";
			public const string Datacite = "datacite";
			public const string DublinCore = "dc";
			public const string ERC = "erc";
		}

		public static class MetadataStatus
		{
			public const string Public = "public";
			public const string Reserved = "reserved";
			public const string Unavailable = "unavailable";
		}
	}
}
