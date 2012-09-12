using System.Net;

namespace WSULibs.EZID
{
	/// <summary>
	/// EZIDApiException is thrown when the EZID API returns an error
	/// </summary>
	public class EZIDApiException : EZIDException
	{
		public HttpStatusCode StatusCode { get; set; }

		public EZIDApiException(HttpStatusCode statusCode, string errorMessage)
			: base(errorMessage)
		{
			this.StatusCode = statusCode;
		}
	}
}
