using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSULibs.EZID
{
	public class Authentication
	{
		public string Username { get; set; }

		public string Password { get; set; }

		public Authentication(string username, string password)
		{
			this.Username = username;
			this.Password = password;
		}

		public string EncodeBase64()
		{
			var byteArray = UTF8Encoding.UTF8.GetBytes(String.Format("{0}:{1}", this.Username, this.Password));
			return Convert.ToBase64String(byteArray);
		}
	}
}
