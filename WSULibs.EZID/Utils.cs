using System;
using System.Text;

namespace WSULibs.EZID
{
	public static class Utils
	{
		public static string Escape(string s)
		{
			return s.Replace("%", "%25").Replace("\n", "%0A").Replace("\r", "%0D").Replace(":", "%3A");
		}

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
