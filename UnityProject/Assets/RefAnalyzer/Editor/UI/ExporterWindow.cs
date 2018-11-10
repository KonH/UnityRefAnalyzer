using System.Linq;
using UnityEngine;
using UnityEditor;

namespace RefAnalyzer.UI {
	class ExporterWindow : EditorWindow {
		const string ExportPath = "refs.json";
		const float  Margin     = 10;

		GUIStyle _style = GUIStyle.none;
		
		[MenuItem("Window/RefAnalyzer")]
		static void Init() {
			var window = GetWindow<ExporterWindow>("RefExporter");
			window.Show();
		}

		void OnGUI() {
			_style.wordWrap = true;
			var size = position.size;
			size.x -= Margin;
			size.y -= Margin;
			GUILayout.BeginArea(new Rect(new Vector2(Margin, Margin), size));
			GUILayout.Label(
				"'Refresh Data' below updates refs.json in project root, which contains references from all scenes in Build Settings.",
				_style);
			if ( GUILayout.Button("Refresh Data") ) {
				OnRefreshData();
			}
			GUILayout.EndArea();
		}

		void OnRefreshData() {
			var scenes   = EditorBuildSettings.scenes.Select(scene => scene.path);
			var exporter = new RefExporter(scenes, ExportPath);
			exporter.Prepare();
			exporter.Export();
			Debug.LogFormat("Data exported to '{0}'", ExportPath);
		}
	}
}
