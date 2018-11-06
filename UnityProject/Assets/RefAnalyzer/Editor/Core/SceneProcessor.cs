using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace RefAnalyzer.Core {
	public class SceneProcessor {
		Scene _scene;

		public SceneProcessor(Scene scene) {
			Assert.IsTrue(scene.IsValid());
			_scene = scene;
		}

		public RawData Process() {
			var crawler = new SceneCrawler(_scene.path);
			var iterator = new SceneIterator(_scene, crawler.Process);
			iterator.Visit();
			return crawler.Data;
		}
	}
}
