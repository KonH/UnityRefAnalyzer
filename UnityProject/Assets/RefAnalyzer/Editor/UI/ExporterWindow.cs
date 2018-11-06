using System.Linq;
using UnityEngine;
using UnityEditor;

namespace RefAnalyzer.UI {
	class ExporterWindow : EditorWindow {
		const string _exportPath = "refs.json";

		[MenuItem("Window/RefAnalyzer")]
		static void Init() {
			var window = GetWindow<ExporterWindow>() as ExporterWindow;
			window.Show();
		}

		void OnGUI() {
			if ( GUILayout.Button("RefreshData") ) {
				var scenes = EditorBuildSettings.scenes.Select(scene => scene.path);
				var exporter = new RefExporter(scenes, _exportPath);
				exporter.Prepare();
				exporter.Export();
			}
		}
	}
}
