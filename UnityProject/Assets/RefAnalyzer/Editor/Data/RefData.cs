using System;
using System.Collections.Generic;
using UnityEngine;

namespace RefAnalyzer.Data {
	[Serializable]
	public class RefData {
		public List<RefScene> Scenes { get { return scenes; } }

		[SerializeField]
		List<RefScene> scenes;
		
		public RefData() {
			scenes = new List<RefScene>();
		}

		public RefScene AddScene(string path) {
			if ( string.IsNullOrEmpty(path) ) {
				throw new ArgumentNullException("path");
			}
			if ( scenes.Find(s => s.Path == path) != null ) {
				throw new ArgumentException("path");
			}
			var scene = new RefScene(path);
			scenes.Add(scene);
			return scene;
		}
	}
}