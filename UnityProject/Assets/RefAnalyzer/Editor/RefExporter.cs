using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using RefAnalyzer.Core;
using RefAnalyzer.Data;
using RefAnalyzer.Validation;

namespace RefAnalyzer {
	public class RefExporter {
		public int    TotalProgress    { get; }
		public int    CurrentProgress  { get; private set; }
		public string CurrentScenePath { get; private set; }
		
		public bool IsDone => CurrentProgress == TotalProgress;
		
		readonly List<string> _scenes;
		readonly string       _exportPath;

		RefData _serializationData = new RefData();

		public RefExporter(IEnumerable<string> scenes, string exportPath) {
			Guard.NotNull(scenes);
			Guard.IsValid(scenes, s => s.Count() > 0, "scenes");
			Guard.NotNullOrWhiteSpace(exportPath);
			_scenes     = new List<string>(scenes);
			_exportPath = exportPath;
			TotalProgress = _scenes.Count;
			CurrentScenePath = _scenes[0];
		}

		public RefData PrepareAll() {
			CurrentProgress  = 0;
			CurrentScenePath = string.Empty;
			foreach ( var scenePath in _scenes ) {
				PrepareScene(scenePath);
			}
			return _serializationData;
		}

		public bool PrepareNextScene() {
			var index = CurrentProgress;
			if ( index < _scenes.Count ) {
				var scene = _scenes[index];
				PrepareScene(scene);
			}
			if ( !IsDone ) {
				CurrentScenePath = _scenes[CurrentProgress];
			}
			return IsDone;
		}

		void PrepareScene(string scenePath) {
			var activeScene = SceneManager.GetActiveScene();
			var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
			Guard.IsValid(scene, s => s.IsValid(), "scene");
			var rawSceneData = new SceneProcessor(scene).Process();
			SaveSceneData(rawSceneData);
			if ( scene.path != activeScene.path ) {
				EditorSceneManager.CloseScene(scene, true);
			}
			System.Threading.Thread.Sleep(1000);
			CurrentProgress++;
		}
		
		void SaveSceneData(RawData sceneData) {
			if ( sceneData.Nodes.Count > 0 ) {
				var sceneNode = _serializationData.AddScene(sceneData.ScenePath);
				foreach ( var node in sceneData.Nodes ) {
					var srcPath = AssetUtils.GetPathTo(node.SourceObj);
					var srcType = AssetUtils.GetTypeName(node.SourceObj);
					var srcProp = node.SourceProperty;
					var tgPath = AssetUtils.GetPathTo(node.TargetObj);
					var tgType = AssetUtils.GetTypeName(node.TargetObj);
					var tgMethod = node.TargetMethod;
					var refNode = new RefNode(srcPath, srcType, srcProp, tgPath, tgType, tgMethod);
					sceneNode.AddNode(refNode);
				}
			}
		}
		
		public void Export() {
			var jsonData = new RefDataSerializer(_serializationData).Serialize();
			var saver    = new RefDataSaver(_exportPath, jsonData);
			saver.Save();
		}
	}
}
