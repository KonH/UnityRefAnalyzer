using System.Collections.Generic;
using RefAnalyzer.Validation;

namespace RefAnalyzer.Core {
	public class RawData {
		public string            ScenePath { get; }
		public List<RawDataNode> Nodes     { get; }
		
		public RawData(string scenePath) {
			Guard.NotNullOrWhiteSpace(scenePath);
			ScenePath = scenePath;
			Nodes = new List<RawDataNode>();
		}
		
		public void AddRef(RawDataNode node) {
			Guard.NotNull(node);
			Nodes.Add(node);
		}
	}
}
