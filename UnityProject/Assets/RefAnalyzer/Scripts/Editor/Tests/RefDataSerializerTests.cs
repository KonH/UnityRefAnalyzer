using System;
using RefAnalyzer.Core;
using NUnit.Framework;

namespace RefAnalyzer.Tests {
	public class RefDataSerializerTests {
		void CheckSerializedJsonContains(string expectedPart, Action<RefData> init) {
			var data = new RefData();
			init(data);
			var jsonStr = new RefDataSerializer(data).Serialize();
			Assert.True(jsonStr.Contains(expectedPart), jsonStr);
		}

		void CheckSerializedSceneContains(string expectedPart, Action<RefScene> init) {
			CheckSerializedJsonContains(expectedPart, d => {
				init(d.AddScene("scene"));
			});
		}

		void CheckSerializedNodeContains
		(
			string expectedName, string expectedPart,
			string srcPath = "_", string srcType = "_", string srcProp = "_", string tgPath = "_", string tgType = "_", string tgMethod = "_"
		) {
			CheckSerializedJsonContains(string.Format("\"{0}\":\"{1}\"", expectedName, expectedPart), d => {
				var scene = d.AddScene("scene");
				scene.AddNode(new RefNode(srcPath, srcType, srcProp, tgPath, tgType, tgMethod));
			});
		}

		[Test]
		public void IsSceneSerialized() {
			var scenePath = "path/to/sceneName.unity";
			CheckSerializedJsonContains(scenePath, d => {
				d.AddScene(scenePath);
			});
		}

		[Test]
		public void IsPropertySerialized() {
			var propertyName = "onClick";
			CheckSerializedNodeContains("srcProp", propertyName, srcProp: propertyName);
		}

		[Test]
		public void IsSourcePathSerialized() {
			var sourcePath = "root/GameObject";
			CheckSerializedNodeContains("srcPath", sourcePath, srcPath: sourcePath);
		}

		[Test]
		public void IsSourceTypeSerialized() {
			var sourceType = "Button";
			CheckSerializedNodeContains("srcType", sourceType, srcType: sourceType);
		}

		[Test]
		public void IsTargetTypeSerialized() {
			var targetType = "TestComponent";
			CheckSerializedNodeContains("tgType", targetType, tgType: targetType);
		}

		[Test]
		public void IsTargetPathSerialized() {
			var targetPath = "root/GameObject(0)";
			CheckSerializedNodeContains("tgPath", targetPath, tgPath: targetPath);
		}

		[Test]
		public void IsMethodNameSerialized() {
			var methodName = "OnClick";
			CheckSerializedNodeContains("tgMethod", methodName, tgMethod: methodName);
		}
	}
}