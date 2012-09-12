using System;
using System.Collections.Generic;
using System.Net;

namespace WSULibs.EZID
{
	/// <summary>
	/// Abstracts an EZID API get request
	/// </summary>
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

		/// <summary>
		/// Execute the requerst and 
		/// </summary>
		/// <returns>Dictionary of key/value paired metadata</returns>
		/// <exception cref="System.Net.WebException">Thrown when there is a problem with the HTTP request</exception>
		/// <exception cref="WSULibs.EZID.EZIDApiException">Thrown when the EZID API returns an API level error</exception>
		public IDictionary<string, string> ExecuteRequest()
		{
			if (string.IsNullOrWhiteSpace(this.Identifier))
				throw new InvalidOperationException("Identifier cannot of empty or null");

			var response = new Response(Request.ExecuteRequest(GetRequest.PATH + this.Identifier, RequestMethod.GET));

			var map = response.Parse();
			if (response.HasError)
				throw new EZIDApiException(response.StatusCode, map[Response.ResponseStatusKeys.Error]);

			return response.Parse();
		}
	}
}
