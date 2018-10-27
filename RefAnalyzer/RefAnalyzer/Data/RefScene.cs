using System.Collections.Generic;
using Newtonsoft.Json;
using RefAnalyzer.Validation;

namespace RefAnalyzer.Data
{
    public class RefScene
    {
        public RefScene()
        {
            Nodes = new List<RefNode>();
        }

        public RefScene(string path) : this()
        {
            Path = path;
        }

        [JsonProperty(PropertyName = "path")]
        public string Path { get; private set; }

        [JsonProperty(PropertyName = "nodes")]
        public List<RefNode> Nodes { get; private set; }

        public void AddNode(RefNode node)
        {
            Guard.NotNull(node);
            Nodes.Add(node);
        }
    }
}