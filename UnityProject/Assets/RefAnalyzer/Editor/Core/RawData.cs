using System.Collections.Generic;
using UnityEngine.Assertions;

namespace RefAnalyzer.Core {
	public class RawData {
		public string            ScenePath { get; }
		public List<RawDataNode> Nodes     { get; }
		
		public RawData(string scenePath) {
			Assert.IsTrue(!string.IsNullOrEmpty(scenePath));
			ScenePath = scenePath;
			Nodes = new List<RawDataNode>();
		}
		
		public void AddRef(RawDataNode node) {
			Assert.IsNotNull(node);
			Nodes.Add(node);
		}
	}
}
