using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RefAnalyzer.Data {
	public class RefData {
		[JsonProperty(PropertyName = "scenes")]
		public List<RefScene> Scenes { get; private set; }
		
		public RefData() {
			Scenes = new List<RefScene>();
		}

		public RefScene AddScene(string path) {
			if ( string.IsNullOrEmpty(path) ) {
				throw new ArgumentNullException("path");
			}
			if ( Scenes.Find(s => s.Path == path) != null ) {
				throw new ArgumentException("path");
			}
			var scene = new RefScene(path);
			Scenes.Add(scene);
			return scene;
		}
	}
}