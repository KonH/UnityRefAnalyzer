using System.IO;
using UnityEngine.Assertions;

namespace RefAnalyzer.Data {
	public class RefDataSaver {
		readonly string _path;
		readonly string _contents;

		public RefDataSaver(string path, string contents) {
			Assert.IsNotNull(path);
			Assert.IsNotNull(contents);
			_path = path;
			_contents = contents;
		}

		public void Save() {
			File.WriteAllText(_path, _contents);
		}
	}
}
