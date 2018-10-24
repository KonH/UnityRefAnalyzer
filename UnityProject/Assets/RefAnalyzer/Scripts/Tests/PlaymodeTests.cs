using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using RefAnalyzer.Core;
using NUnit.Framework;

namespace RefAnalyzer.Tests {
	public class PlaymodeTests {
		
		[UnityTest]
		public IEnumerator PlaymodeTestsWithEnumeratorPasses() {
			var scenePath = TestSettings.ButtonClickScene;
			SceneManager.LoadScene(scenePath);
			yield return null;
			var processor = new SceneProcessor(SceneManager.GetActiveScene());
			var data      = processor.Process();

			Assert.AreEqual(scenePath, data.ScenePath);
			Assert.IsTrue(data.Nodes.Count == 1);

			var expectedSource   = GameObject.Find("ButtonObject").GetComponent<Button>();
			var expectedProperty = "onClick";
			var expectedTarget   = GameObject.Find("TestObject").GetComponent<TestComponent>();
			var expectedMethod   = "OnClick";

			var node = data.Nodes[0];
			Assert.AreEqual(expectedSource,   node.SourceObj);
			Assert.AreEqual(expectedProperty, node.SourceProperty);
			Assert.AreEqual(expectedTarget,   node.TargetObj);
			Assert.AreEqual(expectedMethod,   node.TargetMethod);
		}
	}
}
