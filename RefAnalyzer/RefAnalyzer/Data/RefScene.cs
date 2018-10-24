using System;
using System.Collections.Generic;

namespace RefAnalyzer.Data {
	public class RefScene {
		public string        Path  { get { return path;  } }
		public List<RefNode> Nodes { get { return nodes; } }
		
		string path;
		
		List<RefNode> nodes;

		internal RefScene(string path) {
			this.path = path;
			nodes = new List<RefNode>();
		}

		public void AddNode(RefNode node) {
			if ( node == null ) {
				throw new ArgumentNullException("node");
			}
			nodes.Add(node);
		}
	}
}
