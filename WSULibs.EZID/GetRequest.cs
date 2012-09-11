using System;
using System.Collections.Generic;
using System.Net;

namespace WSULibs.EZID
{
	public class GetRequest : Request
	{
		public const string PATH = "/ezid/id/";

		/// <summary>
		/// Create an instance of an EZID GetRequest object
		/// </summary>
		/// <param name="identifier">EZID Identifier (ie: ark:/99999/fk4cz3dh0)</param>
		public GetRequest(string identifier)
			: base(GetRequest.PATH + identifier, RequestMethod.GET)
		{
			if (String.IsNullOrWhiteSpace(identifier))
				throw new ArgumentNullException("identifier");
		}

		public IDictionary<string, string> ExecuteRequest()
		{
			Response response = null;
			try
			{
				response = new Response(this.ExecuteRequest(null));
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
