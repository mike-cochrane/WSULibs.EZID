using System;
using System.Collections.Generic;
using System.Net;

namespace WSULibs.EZID
{
	public class GetRequest : Request
	{
		public const string PATH = "/ezid/id/";

		public string Identifier { get; set; }

		/// <summary>
		/// Create an instance of an EZID GetRequest object
		/// </summary>
		/// <param name="identifier">EZID Identifier (ie: ark:/99999/fk4cz3dh0)</param>
		public GetRequest(string identifier = null)
		{
			this.Identifier = identifier;
		}

		public IDictionary<string, string> ExecuteRequest()
		{
			if (string.IsNullOrWhiteSpace(this.Identifier))
				throw new InvalidOperationException("Identifier cannot of empty or null");

			Response response = null;
			try
			{
				response = new Response(Request.ExecuteRequest(GetRequest.PATH + this.Identifier, RequestMethod.GET));
			}
			catch (WebException e)
			{
				throw new EZIDException(e.Message, e);
			}

			var map = response.Parse();
			if (response.StatusCode != System.Net.HttpStatusCode.OK)
				throw new EZIDException(map[Response.ResponseStatusKeys.Error]);

			return response.Parse();
		}
	}
}
