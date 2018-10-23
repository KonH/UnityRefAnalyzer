using System;
using System.Collections.Generic;
using UnityEngine;

namespace RefAnalyzer.Data {
	[Serializable]
	public class RefScene {
		public string        Path  { get { return path;  } }
		public List<RefNode> Nodes { get { return nodes; } }

		[SerializeField]
		string path;

		[SerializeField]
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
