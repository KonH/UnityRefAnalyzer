﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using RefAnalyzer.Core;
using RefAnalyzer.Data;
using RefAnalyzer.Validation;

namespace RefAnalyzer {
	public class RefExporter {
		readonly IEnumerable<string> _scenes;
		readonly string              _exportPath;

		RefData _serializationData = new RefData();

		public RefExporter(IEnumerable<string> scenes, string exportPath) {
			Guard.NotNull(scenes);
			Guard.IsValid(scenes, s => s.Count() > 0, "scenes");
			Guard.NotNullOrWhiteSpace(exportPath);
			_scenes     = scenes;
			_exportPath = exportPath;
		}

		public RefData Prepare() {
			var activeScene = SceneManager.GetActiveScene();
			foreach ( var scenePath in _scenes ) {
				var scene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
				Guard.IsValid(scene, s => s.IsValid(), "scene");
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
	}
}
