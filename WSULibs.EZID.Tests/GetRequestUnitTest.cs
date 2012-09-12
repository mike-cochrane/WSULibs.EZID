using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WSULibs.EZID.Tests
{
	[TestClass]
	public class GetRequestUnitTest
	{
		[TestMethod]
		public void GetRequestInvalidIdentifierTest()
		{
			GetRequest request = new GetRequest();
			request.Identifier = "ark:/99999/madeup";

			try
			{
				var map = request.ExecuteRequest();
				throw new Exception("Shouldn't reach this point");
			}
			catch (EZIDApiException) { }
		}

		[TestMethod]
		public void GetRequestValidIdentifierTest1()
		{
			GetRequest request = new GetRequest();
			request.Identifier = "ark:/87273/q8000015";

			var map = request.ExecuteRequest();
			Assert.IsNotNull(map);
			Assert.IsTrue(map.ContainsKey(Metadata.MetadataKeys.Profile));
			Assert.IsTrue(map[Metadata.MetadataKeys.Profile] == Metadata.MetadataTypes.DublinCore);
		}

		[TestMethod]
		public void GetRequestValidIdentifierTest2()
		{
			GetRequest request = new GetRequest();
			request.Identifier = "ark:/87273/q800002m";

			var map = request.ExecuteRequest();
			Assert.IsNotNull(map);
			Assert.IsTrue(map.ContainsKey(Metadata.MetadataKeys.Profile));
			Assert.IsTrue(map[Metadata.MetadataKeys.Profile] == Metadata.MetadataTypes.DublinCore);
		}
	}
}
