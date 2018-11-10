using System.IO;
using RefAnalyzer.Validation;

namespace RefAnalyzer.Data {
	public class RefDataSaver {
		readonly string _path;
		readonly string _contents;

		public RefDataSaver(string path, string contents) {
			Guard.NotNull(path);
			Guard.NotNull(contents);
			_path = path;
			_contents = contents;
		}

		public void Save() {
			File.WriteAllText(_path, _contents);
		}
	}
}
