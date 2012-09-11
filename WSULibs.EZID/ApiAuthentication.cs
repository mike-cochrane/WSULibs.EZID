using System;
using System.Text;

namespace WSULibs.EZID
{
	/// <summary>
	/// API Authentication
	/// </summary>
	public class ApiAuthentication
	{
		public string Username { get; set; }

		public string Password { get; set; }

		public ApiAuthentication(string username, string password)
		{
			if (username == null)
				throw new ArgumentNullException("username");

			if (password == null)
				throw new ArgumentNullException("password");

			this.Username = username;
			this.Password = password;
		}

		/// <summary>
		/// Calculate the Base64 encoded representation of the username:password for Basic HTTP Authentication
		/// </summary>
		/// <returns>Base64 encoded username:password string</returns>
		public string EncodeBase64()
		{
			if (this.Username == null)
				throw new ArgumentNullException("username");

			if (this.Password == null)
				throw new ArgumentNullException("password");

			var byteArray = UTF8Encoding.UTF8.GetBytes(String.Format("{0}:{1}", this.Username, this.Password));
			return Convert.ToBase64String(byteArray);
		}

		public override string ToString()
		{
			return "Basic " + this.EncodeBase64();
		}
	}
}
