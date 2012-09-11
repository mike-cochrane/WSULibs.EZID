using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSULibs.EZID
{
	public class DeleteRequest : Request
	{
		public const string PATH = "/ezid/id/";

		public DeleteRequest(string identifier)
			: base(DeleteRequest.PATH + identifier, RequestMethod.POST)
		{
			throw new NotImplementedException();
		}
	}
}
