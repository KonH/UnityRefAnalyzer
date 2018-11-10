using UnityEngine.SceneManagement;
using RefAnalyzer.Validation;

namespace RefAnalyzer.Core {
	public class SceneProcessor {
		Scene _scene;

		public SceneProcessor(Scene scene) {
			Guard.IsValid(scene, s => s.IsValid(), "scene");
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
