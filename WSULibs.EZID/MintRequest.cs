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

		public MintRequest(string shoulder, Metadata metadata, ApiAuthentication authentication = null)
		{
			this.Shoulder = shoulder;
			this.Metadata = metadata;

			if (authentication != null)
				this.Authentication = authentication;
		}

		public string ExecuteRequest()
		{
			if (this.Shoulder == null)
				throw new InvalidOperationException("Shoulder must not be null");

			if (this.Metadata == null)
				throw new InvalidOperationException("Metadata must not be null");

			if (this.Authentication == null)
				throw new InvalidOperationException("Authentication must not be null");

			if (String.IsNullOrWhiteSpace(this.Metadata.Target))
				throw new InvalidOperationException("Metadata.Target must not be null");

			Response response = null;
			try
			{
				var httpResponse = Request.ExecuteRequest(this.Shoulder, RequestMethod.POST, authentication: this.Authentication, map: this.Metadata.AsDictionary());
				response = new Response(httpResponse);
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
