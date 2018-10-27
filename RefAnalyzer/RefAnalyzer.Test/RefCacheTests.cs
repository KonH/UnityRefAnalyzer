using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefAnalyzer.Data;

namespace RefAnalyzer.Test {
	[TestClass]
	public class RefCacheTests {

		[TestMethod]
		public void IsCacheContainsRequestedClassAndMethod() {
			var data = new RefData();
			data.AddScene("scene").AddNode(new RefNode("srcPath", "srcType", "srcProp", "tgPath", "tgType", "tgMethod"));
			var cache = new RefCache(data);
			var refs = cache.GetRefs("tgType", "tgMethod");
			Assert.IsNotNull(refs);
		}
	}
}
