using System.Linq;
using UnityEngine;
using UnityEditor;

namespace RefAnalyzer.UI {
	class ExporterWindow : EditorWindow {
		const string ExportPath = "refs.json";

		[MenuItem("Window/RefAnalyzer")]
		static void Init() {
			var window = GetWindow<ExporterWindow>();
			window.Show();
		}

		void OnGUI() {
			if ( GUILayout.Button("RefreshData") ) {
				var scenes = EditorBuildSettings.scenes.Select(scene => scene.path);
				var exporter = new RefExporter(scenes, ExportPath);
				exporter.Prepare();
				exporter.Export();
				Debug.LogFormat("Data exported to '{0}'", ExportPath);
			}
		}
	}
}
