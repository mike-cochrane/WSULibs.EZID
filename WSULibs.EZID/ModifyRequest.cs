using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSULibs.EZID
{
	public class ModifyRequest : Request
	{
		public const string PATH = "/ezid/id/";

		public string Identifier { get; set; }

		public ModifyRequest(string identifier)
		{
			this.Identifier = identifier;
			throw new NotImplementedException();
		}
	}
}
