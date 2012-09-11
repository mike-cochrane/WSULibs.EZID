using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSULibs.EZID
{
	public class ModifyRequest : Request
	{
		public const string PATH = "/ezid/id/";

		public ModifyRequest(string identifier)
			: base(ModifyRequest.PATH + identifier, RequestMethod.POST)
		{
			throw new NotImplementedException();
		}
	}
}
