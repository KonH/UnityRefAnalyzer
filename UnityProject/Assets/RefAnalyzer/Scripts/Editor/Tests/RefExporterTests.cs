using System.IO;
using NUnit.Framework;

namespace RefAnalyzer.Tests {
	public class RefExporterTests {
		string[] _scenePathes = new string[] { TestSettings.ButtonClickScene };
		string _exportPath    = TestSettings.TempFilePath;

		[Test]
		public void IsFileCreated() {
			if ( File.Exists(_exportPath) ) {
				File.Delete(_exportPath);
			}
			var exporter = new RefExporter(_scenePathes, _exportPath);
			exporter.Prepare();
			exporter.Export();
			if ( File.Exists(_exportPath) ) {
				File.Delete(_exportPath);
			}
		}

		[Test]
		public void IsDataContainsScene() {
			var data = new RefExporter(_scenePathes, _exportPath).Prepare();
			Assert.AreEqual(1, data.Scenes.Count);
			Assert.AreEqual(_scenePathes[0], data.Scenes[0].Path);
		}

		[Test]
		public void IsDataContainsExpectedNodes() {
			var data = new RefExporter(_scenePathes, _exportPath).Prepare();
			var scene = data.Scenes[0];
			Assert.AreEqual(1, scene.Nodes.Count);
			var node = scene.Nodes[0];
			Assert.AreEqual("UnityEngine.UI.Button", node.SourceType);
			Assert.AreEqual("onClick", node.SourceProprty);
			Assert.AreEqual("TestComponent", node.TargetType);
			Assert.AreEqual("OnClick", node.TargetMethod);
		}

		[Test]
		public void IsDataContainsValidPathes() {
			var data = new RefExporter(_scenePathes, _exportPath).Prepare();
			var node = data.Scenes[0].Nodes[0];
			Assert.AreEqual("Canvas/ButtonObject", node.SourcePath);
			Assert.AreEqual("TestObject", node.TargetPath);
		}
	}
}