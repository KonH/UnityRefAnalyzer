using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using RefAnalyzer.Core;
using RefAnalyzer.Data;

namespace RefAnalyzer {
	public class RefExporter {
		readonly IEnumerable<string> _scenes;
		readonly string              _exportPath;

		RefData _serializationData = new RefData();

		public RefExporter(IEnumerable<string> scenes, string exportPath) {
			Assert.IsNotNull(scenes);
			Assert.IsTrue(scenes.Count() > 0);
			Assert.IsTrue(!string.IsNullOrEmpty(exportPath));
			_scenes     = scenes;
			_exportPath = exportPath;
		}

		public RefData Prepare() {
			var activeScene = SceneManager.GetActiveScene();
			foreach ( var scenePath in _scenes ) {
				var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
				Assert.IsTrue(scene.IsValid());
				var rawSceneData = new SceneProcessor(scene).Process();
				SaveSceneData(rawSceneData);
				if ( scene.path != activeScene.path ) {
					EditorSceneManager.CloseScene(scene, true);
				}
			}
			return _serializationData;
		}

		public void Export() {
			var jsonData = new RefDataSerializer(_serializationData).Serialize();
			var saver = new RefDataSaver(_exportPath, jsonData);
			saver.Save();
		}

		void SaveSceneData(RawData sceneData) {
			if ( sceneData.Nodes.Count > 0 ) {
				var sceneNode = _serializationData.AddScene(sceneData.ScenePath);
				foreach ( var node in sceneData.Nodes ) {
					string srcPath = GetPathTo(node.SourceObj);
					string srcType = GetTypeName(node.SourceObj);
					string srcProp = node.SourceProperty;
					string tgPath = GetPathTo(node.TargetObj);
					string tgType = GetTypeName(node.TargetObj);
					string tgMethod = node.TargetMethod;
					var refNode = new RefNode(srcPath, srcType, srcProp, tgPath, tgType, tgMethod);
					sceneNode.AddNode(refNode);
				}
			}
		}

		string GetTypeName(Object obj) {
			return obj.GetType().FullName;
		}

		string GetPathTo(Object obj) {
			var component = obj as Component;
			return (component != null) ? GetPathTo(component.gameObject) : string.Empty;
		}

		string GetPathTo(GameObject go) {
			if ( go ) {
				var parentTrans = go.transform.parent;
				var parentPath = parentTrans ? GetPathTo(parentTrans.gameObject) + "/" : string.Empty;
				return parentPath + go.name;
			}
			return string.Empty;
		}
	}
}
