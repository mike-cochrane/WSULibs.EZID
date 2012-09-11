using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace WSULibs.EZID
{
	public class Response
	{
		public HttpStatusCode StatusCode { get; protected set; }

		public string ResponseBody { get; protected set; }

		public Response(HttpWebResponse httpResponse)
		{
			if (httpResponse == null)
				throw new ArgumentNullException("httpResponse");

			this.StatusCode = httpResponse.StatusCode;

			var reader = new StreamReader(httpResponse.GetResponseStream());
			this.ResponseBody = reader.ReadToEnd();
		}

		public IDictionary<string, string> Parse()
		{
			var map = new Dictionary<string, string>();
			foreach (var s in this.ResponseBody.Split('\n'))
			{
				if (String.IsNullOrWhiteSpace(s))
					continue;

				string[] keyValue = s.Split(new char[] { ':' }, 2);
				map.Add(Utils.Unescape(keyValue[0].Trim()), Utils.Unescape(keyValue[1]).Trim());
			}

			return map;
		}

		public static class ResponseStatusKeys
		{
			public const string Error = "error";
			public const string Success = "success";
		}
	}
}
