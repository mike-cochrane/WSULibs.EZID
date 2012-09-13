using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WSULibs.EZID
{
	abstract public class Request
	{
		public const string HOST = "n2t.net";

		/// <summary>
		/// Execute the request and get the raw HTTP response from the server
		/// </summary>
		/// <param name="metadataDictionary">Dictionary of key/value metadata pairs</param>
		/// <returns>Raw HTTP response returned from service</returns>
		protected static HttpWebResponse ExecuteRequest(string requestPath, RequestMethod requestMethod, ApiAuthentication authentication = null, IDictionary<string, string> metadataDictionary = null)
		{
			var request = HttpWebRequest.Create(String.Format("https://{0}{1}", Request.HOST, requestPath)) as HttpWebRequest;
			request.ProtocolVersion = HttpVersion.Version11;
			request.Method = requestMethod.ToString();
			request.ContentType = "text/plain; charset=UTF-8";

			// set authentication headers
			if (authentication != null)
			{
				request.Headers[HttpRequestHeader.Authorization] = authentication.ToString();
			}

			// add metadata to request body if passed
			if (metadataDictionary != null)
			{
				var b = new StringBuilder();
				foreach (var pair in metadataDictionary)
				{
					// target shouldn't be encoded
					if (Metadata.MetadataKeys.Target == pair.Key)
					{
						b.Append(pair.Key + ": " + Uri.UnescapeDataString(pair.Value) + "\n");
					}
					else
					{
						b.Append(Utils.Escape(pair.Key) + ": " + Utils.Escape(pair.Value) + "\n");
					}
				}

				var body = UTF8Encoding.UTF8.GetBytes(b.ToString());
				request.ContentLength = body.Length;

				// write request body to request stream
				using (var stream = request.GetRequestStream())
				{
					stream.Write(body, 0, body.Length);
				}
			}

			HttpWebResponse response = null;
			// HttpWebRequest.GetResponse throws WebException for protocol level errors (ie 401)
			// We want to capture those and persist the rest
			try
			{
				response = request.GetResponse() as HttpWebResponse;
			}
			catch (WebException e)
			{
				// only throw non protocol error exceptions
				if (WebExceptionStatus.ProtocolError != e.Status)
					throw e;

				response = e.Response as HttpWebResponse;
			}

			return response;
		}

		public ApiAuthentication Authentication { get; set; }
	}
}
