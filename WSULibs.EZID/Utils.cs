using System;
using System.Text;

namespace WSULibs.EZID
{
	/// <summary>
	/// Collection of general purpose utility functions
	/// </summary>
	public static class Utils
	{
		/// <summary>
		/// Percent encode a string
		/// </summary>
		/// <remarks>This method was ported from the API docs referecned. (http://n2t.net/ezid/doc/apidoc.html#request-response-bodies)</remarks>
		/// <param name="s">String to encode</param>
		/// <returns>Percent encoded string</returns>
		public static string Escape(string s)
		{
			return s.Replace("%", "%25").Replace("\n", "%0A").Replace("\r", "%0D").Replace(":", "%3A");
		}

		/// <summary>
		/// Unencode a percent encoded string
		/// </summary>
		/// <remarks>This method was ported from the API docs referecned. (http://n2t.net/ezid/doc/apidoc.html#request-response-bodies)</remarks>
		/// <param name="s">Precent encoded string</param>
		/// <returns>Unencoded string</returns>
		public static string Unescape(string s)
		{
			StringBuilder b = new StringBuilder();
			int i;
			while ((i = s.IndexOf("%")) >= 0)
			{
				b.Append(s.Substring(0, i));
				b.Append((char)Convert.ToInt32(s.Substring(i + 1, i + 3), 16));
				s = s.Substring(i + 3);
			}
			b.Append(s);
			return b.ToString();
		}
	}
}
