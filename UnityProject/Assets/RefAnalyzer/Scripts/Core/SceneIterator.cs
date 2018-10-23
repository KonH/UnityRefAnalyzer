using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace RefAnalyzer.Core {
	public class SceneIterator {
		Scene             _scene;
		Action<Component> _visitor;

		public SceneIterator(Scene scene, Action<Component> visitor) {
			Assert.IsTrue(scene.IsValid());
			Assert.IsNotNull(visitor);
			_scene   = scene;
			_visitor = visitor;
		}

		public void Visit() {
			foreach ( var go in _scene.GetRootGameObjects() ) {
				Visit(go);
			}
		}

		void Visit(GameObject go) {
			Visit(go.GetComponents<Component>());
			for ( var i = 0; i < go.transform.childCount; i++ ) {
				Visit(go.transform.GetChild(i).gameObject);
			}
		}

		void Visit(Component[] components) {
			foreach ( var comp in components ) {
				_visitor(comp);
			}
		}
	}
}
