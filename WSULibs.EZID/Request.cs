using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WSULibs.EZID
{
	abstract public class Request
	{
		public const string HOST = "n2t.net";

		public string RequestPath { get; set; }

		public RequestMethod RequestMethod { get; set; }

		public Uri RequestUri
		{
			get
			{
				return new Uri(String.Format("https://{0}{1}", Request.HOST, this.RequestPath));
			}
		}

		public ApiAuthentication Authentication { get; set; }

		protected Request(string requestPath, RequestMethod requestMethod)
		{
			if (String.IsNullOrWhiteSpace(requestPath))
				throw new ArgumentException("requestPath");

			if (requestMethod == null)
				throw new ArgumentException("requestMethod");

			this.RequestPath = requestPath;
			this.RequestMethod = requestMethod;
		}

		protected Request(string requestPath, RequestMethod requestMethod, ApiAuthentication authentication)
			: this(requestPath, requestMethod)
		{
			this.Authentication = authentication;
		}

		/// <summary>
		/// Execute the request and get the raw HTTP response from the server
		/// </summary>
		/// <param name="map">Dictionary of key/value metadata pairs</param>
		/// <returns>Raw HTTP response returned from service</returns>
		protected HttpWebResponse ExecuteRequest(IDictionary<string, string> map)
		{
			var request = HttpWebRequest.Create(this.RequestUri) as HttpWebRequest;
			request.ProtocolVersion = HttpVersion.Version11;
			request.Method = this.RequestMethod.ToString();
			request.ContentType = "text/plain; charset=UTF-8";

			// set authentication headers
			if (this.Authentication != null)
			{
				request.Headers[HttpRequestHeader.Authorization] = this.Authentication.ToString();
			}

			// add metadata to request body if passed
			if (map != null)
			{
				var b = new StringBuilder();
				foreach (var pair in map)
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

				using (var stream = request.GetRequestStream())
				{
					stream.Write(body, 0, body.Length);
				}
			}

			HttpWebResponse response = null;
			// HttpWebRequest.GetResponse throws WebException for all protocol level errors
			// (ie 401)
			try
			{
				response = request.GetResponse() as HttpWebResponse;
			}
			catch (WebException e)
			{
				if (WebExceptionStatus.ProtocolError != e.Status)
					throw e;

				response = e.Response as HttpWebResponse;
			}

			return response;
		}
	}

	public sealed class RequestMethod
	{
		public static readonly RequestMethod GET = new RequestMethod("GET");
		public static readonly RequestMethod PUT = new RequestMethod("PUT");
		public static readonly RequestMethod POST = new RequestMethod("POST");
		public static readonly RequestMethod DELETE = new RequestMethod("DELETE");

		public string Name { get; private set; }

		private RequestMethod(string name)
		{
			this.Name = name;
		}

		public override string ToString()
		{
			return this.Name;
		}
	}
}
