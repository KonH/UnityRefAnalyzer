using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RefAnalyzer.Data {
	public class RefScene {
		[JsonProperty(PropertyName = "path")]
		public string        Path  { get; private set; }
		[JsonProperty(PropertyName = "nodes")]
		public List<RefNode> Nodes { get; private set; }

		public RefScene() {
			Nodes = new List<RefNode>();
		}

		public RefScene(string path):base() {
			Path = path;
		}

		public void AddNode(RefNode node) {
			if ( node == null ) {
				throw new ArgumentNullException("node");
			}
			Nodes.Add(node);
		}
	}
}
