using System.Linq;
using UnityEngine;
using UnityEditor;

namespace RefAnalyzer.UI {
	class ExporterWindow : EditorWindow {
		const string ExportPath = "refs.json";
		const float  Margin     = 10;

		GUIStyle    _style    = GUIStyle.none;
		RefExporter _exporter = null;

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

		void OnInspectorUpdate() {
			UpdateProgress();
		}

		void OnRefreshData() {
			var scenes = EditorBuildSettings.scenes.Select(scene => scene.path);
			_exporter = new RefExporter(scenes, ExportPath);
		}

		void UpdateProgress() {
			if ( _exporter == null ) {
				return;
			}
			EditorUtility.DisplayProgressBar(
				string.Format("Data Export ({0}/{1})", _exporter.CurrentProgress, _exporter.TotalProgress),
				string.Format("Scene: '{0}'", _exporter.CurrentScenePath),
				Mathf.Clamp01((float)_exporter.CurrentProgress / _exporter.TotalProgress)
			);
			if ( _exporter.PrepareNextScene() ) {
				_exporter.Export();
				_exporter = null;
				EditorUtility.ClearProgressBar();
				Debug.LogFormat("Data exported to '{0}'", ExportPath);
			}
		}
	}
}
