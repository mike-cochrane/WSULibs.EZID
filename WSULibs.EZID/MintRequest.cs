using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSULibs.EZID
{
	public class MintRequest : Request
	{
		public const string PATH = "/ezid/shoulder/";

		public string Shoulder { get; set; }

		public Metadata Metadata { get; set; }

		public MintRequest(string shoulder, Metadata metadata, ApiAuthentication authentication)
			: base(MintRequest.PATH + shoulder, RequestMethod.POST, authentication)
		{
			this.Shoulder = shoulder;
			this.Metadata = metadata;
		}

		public string ExecuteRequest()
		{
			if (this.Shoulder == null)
				throw new NullReferenceException("Shoulder must not be null");

			if (this.Metadata == null)
				throw new NullReferenceException("Metadata must not be null");

			if (String.IsNullOrWhiteSpace(this.Metadata.Target))
				throw new NullReferenceException("Metadata.Target must not be null");

			Response response = null;
			try
			{
				response = new Response(this.ExecuteRequest(this.Metadata.AsDictionary()));
			}
			catch (Exception e)
			{
				throw new EZIDException(e.Message, e);
			}

			var map = response.Parse();
			if (response.StatusCode != System.Net.HttpStatusCode.Created)
				throw new EZIDException(map[Response.ResponseStatusKeys.Error]);

			return map[Response.ResponseStatusKeys.Success];
		}
	}
}
