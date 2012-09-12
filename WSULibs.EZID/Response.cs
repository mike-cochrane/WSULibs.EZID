using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace WSULibs.EZID
{
	public class Response
	{
		public HttpStatusCode StatusCode { get; protected set; }

		public string RawResponseBody { get; protected set; }

		public bool HasError
		{
			get
			{
				return this.Parse().ContainsKey(Response.ResponseStatusKeys.Error);
			}
		}

		public Response(HttpWebResponse httpResponse)
		{
			if (httpResponse == null)
				throw new ArgumentNullException("httpResponse");

			this.StatusCode = httpResponse.StatusCode;

			var reader = new StreamReader(httpResponse.GetResponseStream());
			this.RawResponseBody = reader.ReadToEnd();
		}

		/// <summary>
		/// Parse the response body into a dictionary of key/value metadata pairs
		/// </summary>
		/// <returns>Dictionary of key/value metadata pairs</returns>
		public IDictionary<string, string> Parse()
		{
			// only parse the body once
			if (null == this._parsedBody)
			{
				var map = new Dictionary<string, string>();
				foreach (var s in this.RawResponseBody.Split('\n'))
				{
					if (String.IsNullOrWhiteSpace(s))
						continue;

					string[] keyValue = s.Split(new char[] { ':' }, 2);
					map.Add(Utils.Unescape(keyValue[0].Trim()), Utils.Unescape(keyValue[1]).Trim());
				}

				this._parsedBody = map;
			}

			return this._parsedBody;
		}

		public static class ResponseStatusKeys
		{
			public const string Error = "error";
			public const string Success = "success";
		}

		private IDictionary<string, string> _parsedBody = null;
	}
}
