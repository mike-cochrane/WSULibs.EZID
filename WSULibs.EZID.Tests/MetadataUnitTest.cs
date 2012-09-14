using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WSULibs.EZID.Tests
{
	[TestClass]
	public class MetadataUnitTest
	{
		[TestMethod]
		public void TestConstructFromDictionary1()
		{
			string owner = "Washington State University Libraries";
			string ownerGroup = "Washington State University";
			string[] coOwners = { "CoOwner1", "CoOwner2" };
			long created = DateTime.Now.ToBinary();
			long updated = DateTime.Now.AddDays(1).ToBinary();
			string target = "http://wsulibs.wsu.edu/";
			string status = Metadata.MetadataStatus.Public;

			IDictionary<string, string> map = new Dictionary<string, string>();
			map.Add(Metadata.MetadataKeys.Owner, owner);
			map.Add(Metadata.MetadataKeys.OwnerGroup, ownerGroup);
			map.Add(Metadata.MetadataKeys.CoOwners, string.Join(";", coOwners));
			map.Add(Metadata.MetadataKeys.Created, created.ToString());
			map.Add(Metadata.MetadataKeys.Updated, updated.ToString());
			map.Add(Metadata.MetadataKeys.Target, target);
			map.Add(Metadata.MetadataKeys.Status, status);

			Metadata metadata = new Metadata(map);

			Assert.IsTrue(metadata.Owner == owner);
			Assert.IsTrue(metadata.OwnerGroup == ownerGroup);
			Assert.IsTrue(metadata.CoOwners.Count == coOwners.Length);
			Assert.IsTrue(metadata.CoOwners[0] == coOwners[0]);
			Assert.IsTrue(metadata.CoOwners[1] == coOwners[1]);
			Assert.IsTrue(metadata.Created == created);
			Assert.IsTrue(metadata.Updated == updated);
			Assert.IsTrue(metadata.Target == target);
			Assert.IsTrue(metadata.Status == status);
		}

		[TestMethod]
		public void TestConstructFromDictionary2()
		{
			IDictionary<string, string> map = new Dictionary<string, string>();

			Metadata metadata = new Metadata(map);

			Assert.IsTrue(metadata.Owner == null);
			Assert.IsTrue(metadata.OwnerGroup == null);
			Assert.IsTrue(metadata.CoOwners == null);
			Assert.IsTrue(metadata.Created == default(long));
			Assert.IsTrue(metadata.Updated == default(long));
			Assert.IsTrue(metadata.Target == null);
			Assert.IsTrue(metadata.Status == null);
		}

		[TestMethod]
		public void TestConstructAsDictionary1()
		{
			string owner = "Washington State University Libraries";
			string ownerGroup = "Washington State University";
			string[] coOwners = { "CoOwner1", "CoOwner2" };
			long created = DateTime.Now.ToBinary();
			long updated = DateTime.Now.AddDays(1).ToBinary();
			string target = "http://wsulibs.wsu.edu/";
			string status = Metadata.MetadataStatus.Public;

			IDictionary<string, string> map = new Dictionary<string, string>();
			map.Add(Metadata.MetadataKeys.Owner, owner);
			map.Add(Metadata.MetadataKeys.OwnerGroup, ownerGroup);
			map.Add(Metadata.MetadataKeys.CoOwners, string.Join(";", coOwners));
			map.Add(Metadata.MetadataKeys.Created, created.ToString());
			map.Add(Metadata.MetadataKeys.Updated, updated.ToString());
			map.Add(Metadata.MetadataKeys.Target, target);
			map.Add(Metadata.MetadataKeys.Status, status);

			Metadata metadata = new Metadata(map);
			foreach (var x in metadata.AsDictionary())
				Assert.IsTrue(x.Value == map[x.Key]);
		}
	}
}
