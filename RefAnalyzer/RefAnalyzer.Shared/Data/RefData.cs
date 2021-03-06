﻿using System.Linq;
using System.Collections.Generic;
using RefAnalyzer.Validation;
using Newtonsoft.Json;

namespace RefAnalyzer.Data {
	public class RefData {
		[JsonProperty(PropertyName = "scenes")]
		public List<RefScene> Scenes { get; private set; }

		public RefData() {
			Scenes = new List<RefScene>();
		}

		public RefScene AddScene(string path) {
			Guard.NotNullOrEmpty(path);
			Guard.IsValid(path, p => Scenes.All(s => s.Path != null), "Scenes do not contain given path.");

			var scene = new RefScene(path);
			Scenes.Add(scene);
			return scene;
		}
	}
}