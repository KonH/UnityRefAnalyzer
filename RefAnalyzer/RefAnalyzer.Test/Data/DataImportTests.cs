using Microsoft.VisualStudio.TestTools.UnitTesting;
using RefAnalyzer.Data;

namespace RefAnalyzer.Test.Data {
	[TestClass]
	public class DataImportTests {
		RefData Import(string json) {
			return new RefDataImporter(json).Import();
		}

		[TestMethod]
		public void IsDataContainsScene() {
			var data = Import("{ \"scenes\": [ { \"path\": \"scenePath\" } ] }");
			Assert.AreEqual(1, data.Scenes.Count);
			Assert.AreEqual("scenePath", data.Scenes[0].Path);
		}

		[TestMethod]
		public void IsDataContainsAllNodeInfo() {
			var data = Import("{ \"scenes\": [ { \"path\": \"scenePath\", \"nodes\": [ { " +
				"\"srcPath\": \"_srcPath\", \"srcType\": \"_srcType\", \"srcProp\": \"_srcProp\", \"tgPath\": \"_tgPath\", \"tgType\": \"_tgType\", \"tgMethod\": \"_tgMethod\"" +
				" } ] } ] }");
			Assert.AreEqual(1, data.Scenes[0].Nodes.Count);
			var node = data.Scenes[0].Nodes[0];
			Assert.AreEqual("_srcPath",  node.SourcePath);
			Assert.AreEqual("_srcType",  node.SourceType);
			Assert.AreEqual("_srcProp",  node.SourceProperty);
			Assert.AreEqual("_tgPath",   node.TargetPath);
			Assert.AreEqual("_tgMethod", node.TargetMethod);
		}
	}
}
