using System;

namespace WSULibs.EZID
{
	public class MintRequest : Request
	{
		public const string PATH = "/ezid/shoulder/";

		public string Shoulder { get; set; }

		public Metadata Metadata { get; set; }

		public MintRequest(string shoulder = null, Metadata metadata = null, ApiAuthentication authentication = null)
		{
			if (null != shoulder)
				this.Shoulder = shoulder;

			if (null != metadata)
				this.Metadata = metadata;

			if (null != authentication)
				this.Authentication = authentication;
		}

		/// <summary>
		/// Execute the mint request
		/// </summary>
		/// <returns>ARK identifier</returns>
		/// <exception cref="System.Net.WebException">Thrown when there is a problem with the HTTP request</exception>
		/// <exception cref="WSULibs.EZID.EZIDApiException">Thrown when the EZID API returns an API level error</exception>
		public string ExecuteRequest()
		{
			if (string.IsNullOrWhiteSpace(this.Shoulder))
				throw new InvalidOperationException("Shoulder cannot of empty or null");

			if (this.Metadata == null)
				throw new InvalidOperationException("Metadata must not be null");

			if (this.Authentication == null)
				throw new InvalidOperationException("Authentication must not be null");

			if (String.IsNullOrWhiteSpace(this.Metadata.Target))
				throw new InvalidOperationException("Metadata.Target must not be empty or null");

				var httpResponse = Request.ExecuteRequest(MintRequest.PATH + this.Shoulder, RequestMethod.POST, authentication: this.Authentication, map: this.Metadata.AsDictionary());
				var response = new Response(httpResponse);

			var map = response.Parse();
			if (response.HasError)
				throw new EZIDApiException(response.StatusCode, map[Response.ResponseStatusKeys.Error]);

			return map[Response.ResponseStatusKeys.Success];
		}
	}
}
