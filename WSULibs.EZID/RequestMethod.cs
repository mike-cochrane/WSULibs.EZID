using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSULibs.EZID
{
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
