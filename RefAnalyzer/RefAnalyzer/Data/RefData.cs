using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using RefAnalyzer.Validation;

namespace RefAnalyzer.Data
{
    public class RefData
    {
        public RefData()
        {
            Scenes = new List<RefScene>();
        }

        [JsonProperty(PropertyName = "scenes")]
        public List<RefScene> Scenes { get; private set; }

        public RefScene AddScene(string path)
        {
            Guard.NotNullOrEmpty(path);
            Guard.IsValid(path, p => Scenes.All(s => s.Path != null), "Scenes do not contain given path.");

            var scene = new RefScene(path);
            Scenes.Add(scene);
            return scene;
        }
    }
}