using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WSULibs.EZID
{
	public class EZIDException : Exception
	{
		public EZIDException()
			: base()
		{
		}

		public EZIDException(string message)
			: base(message)
		{
		}

		public EZIDException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
