using System;

namespace WSULibs.EZID
{
	public class DeleteRequest : Request
	{
		public const string PATH = "/ezid/id/";

		public string Identifier { get; set; }

		public DeleteRequest(string identifier = null, ApiAuthentication authentication = null)
		{
			if (null != identifier)
				this.Identifier = identifier;

			if (null != authentication)
				this.Authentication = authentication;
		}

		/// <summary>
		/// Execute the delete request
		/// </summary>
		/// <returns>Ark identifier that was deleted deleted</returns>
		/// <exception cref="System.Net.WebException">Thrown when there is a problem with the HTTP request</exception>
		/// <exception cref="WSULibs.EZID.EZIDApiException">Thrown when the EZID API returns an API level error</exception>
		public string ExecuteRequest()
		{
			if (string.IsNullOrWhiteSpace(this.Identifier))
				throw new InvalidOperationException("Identifier cannot of empty or null");

			if (this.Authentication == null)
				throw new InvalidOperationException("Authentication must not be null");

			var response = new Response(Request.ExecuteRequest(GetRequest.PATH + this.Identifier, RequestMethod.DELETE));

			var map = response.Parse();
			if (response.HasError)
				throw new EZIDApiException(response.StatusCode, map[Response.ResponseStatusKeys.Error]);

			return map[Response.ResponseStatusKeys.Success];
		}
	}
}
