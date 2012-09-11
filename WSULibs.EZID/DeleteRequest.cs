using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSULibs.EZID
{
	public class DeleteRequest : Request
	{
		public const string PATH = "/ezid/id/";

		public string Identifier { get; set; }

		public DeleteRequest(string identifier)
		{
			this.Identifier = identifier;
			throw new NotImplementedException();
		}
	}
}
