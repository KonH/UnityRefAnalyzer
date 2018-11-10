using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using RefAnalyzer.Validation;

namespace RefAnalyzer.Core {
	public class SceneIterator {
		Scene             _scene;
		Action<Component> _visitor;

		public SceneIterator(Scene scene, Action<Component> visitor) {
			Guard.IsValid(scene, s => s.IsValid(), "scene");
			Guard.NotNull(visitor);
			_scene   = scene;
			_visitor = visitor;
		}

		public void Visit() {
			foreach ( var go in _scene.GetRootGameObjects() ) {
				Visit(go);
			}
		}

		void Visit(GameObject go) {
			Visit(go, go.GetComponents<Component>());
			for ( var i = 0; i < go.transform.childCount; i++ ) {
				Visit(go.transform.GetChild(i).gameObject);
			}
		}

		void Visit(GameObject root, Component[] components) {
			foreach ( var comp in components ) {
				if ( comp ) {
					_visitor(comp);
				} else {
					Debug.LogWarningFormat("Invalid component at '{0}'", AssetUtils.GetPathTo(root));
				}
			}
		}
	}
}
