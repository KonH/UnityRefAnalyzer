using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestAnalyzer : EditorWindow {

	// Add menu named "My Window" to the Window menu
	[MenuItem("Window/My Window")]
	static void Init() {
		TestAnalyzer window = GetWindow(typeof(TestAnalyzer)) as TestAnalyzer;
		window.Show();
	}

	void OnGUI() {
		if ( GUILayout.Button("Do magic") ) {
			DoMagic();
		}
	}

	void DoMagic() {
		var scene = EditorSceneManager.OpenScene("Assets/Scene.unity");
		Debug.Log("is valid? " + scene.IsValid());
		var roots = scene.GetRootGameObjects();
		foreach ( var root in roots ) {
			Inspect(root);
		}
	}

	void Inspect(GameObject go) {
		var bt = go.GetComponent<Button>();
		if ( bt ) {
			var events = bt.onClick.GetPersistentEventCount();
			for ( var i = 0; i < events; i++ ) {
				var target = bt.onClick.GetPersistentTarget(i);
				var methodName = bt.onClick.GetPersistentMethodName(i);
				Debug.Log("target:  '" + target.GetType().FullName + "' methodName: '" + methodName + "'");
			}
		}
		for ( var i = 0; i < go.transform.childCount; i++ ) {
			Inspect(go.transform.GetChild(i).gameObject);
		}
	}
}
