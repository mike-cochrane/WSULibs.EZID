using System;

namespace WSULibs.EZID
{
	public class ModifyRequest : Request
	{
		public const string PATH = "/ezid/id/";

		public string Identifier { get; set; }

		public Metadata Metadata { get; set; }

		public ModifyRequest(string identifier = null, Metadata metadata = null, ApiAuthentication authentication = null)
		{
			if (null != identifier)
				this.Identifier = identifier;

			if (null != metadata)
				this.Metadata = metadata;

			if (null != authentication)
				this.Authentication = authentication;
		}

		/// <summary>
		/// Execute the modify request
		/// </summary>
		/// <returns>ARK identifier</returns>
		/// <exception cref="System.Net.WebException">Thrown when there is a problem with the HTTP request</exception>
		/// <exception cref="WSULibs.EZID.EZIDApiException">Thrown when the EZID API returns an API level error</exception>
		public string ExecuteRequest()
		{
			if (string.IsNullOrWhiteSpace(this.Identifier))
				throw new InvalidOperationException("Identifier cannot of empty or null");

			if (this.Metadata == null)
				throw new InvalidOperationException("Metadata must not be null");

			if (this.Authentication == null)
				throw new InvalidOperationException("Authentication must not be null");

			var httpResponse = Request.ExecuteRequest(ModifyRequest.PATH + this.Identifier, RequestMethod.POST, authentication: this.Authentication, map: this.Metadata.AsDictionary());
			var response = new Response(httpResponse);

			var map = response.Parse();
			if (response.HasError)
				throw new EZIDApiException(response.StatusCode, map[Response.ResponseStatusKeys.Error]);

			return map[Response.ResponseStatusKeys.Success];
		}
	}
}
