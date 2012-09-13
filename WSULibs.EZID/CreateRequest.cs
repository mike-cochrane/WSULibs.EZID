using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSULibs.EZID
{
	public class CreateRequest : Request
	{
		public const string PATH = "/ezid/id/";

		public string Identifier { get; set; }

		public Metadata Metadata { get; set; }

		public CreateRequest(string identifier = null, Metadata metadata = null, ApiAuthentication authentication = null)
		{
			if (null != identifier)
				this.Identifier = identifier;

			if (null != metadata)
				this.Metadata = metadata;

			if (null != authentication)
				this.Authentication = authentication;
		}

		/// <summary>
		/// Execute the create request
		/// </summary>
		/// <returns>ARK identifier</returns>
		/// <exception cref="System.Net.WebException">Thrown when there is a problem with the HTTP request</exception>
		/// <exception cref="WSULibs.EZID.EZIDApiException">Thrown when the EZID API returns an API level error</exception>
		public string ExecuteRequest()
		{
			if (string.IsNullOrWhiteSpace(this.Identifier))
				throw new InvalidOperationException("Identifier cannot of empty or null");

			if (this.Authentication == null)
				throw new InvalidOperationException("Authentication must not be null");

			IDictionary<string, string> metadata = null;
			if (null != this.Metadata)
				metadata = this.Metadata.AsDictionary();

			var httpResponse = Request.ExecuteRequest(MintRequest.PATH + this.Identifier, RequestMethod.PUT, authentication: this.Authentication, metadataDictionary: metadata);
			var response = new Response(httpResponse);

			var map = response.Parse();
			if (response.HasError)
				throw new EZIDApiException(response.StatusCode, map[Response.ResponseStatusKeys.Error]);

			return map[Response.ResponseStatusKeys.Success];
		}
	}
}
