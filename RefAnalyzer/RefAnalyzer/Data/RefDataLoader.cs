using System;
using System.IO;

namespace RefAnalyzer.Data {
	public class RefDataLoader {
		readonly string _path;

		public RefDataLoader(string path) {
			if ( string.IsNullOrEmpty(path) ) {
				throw new ArgumentException("path");
			}
			_path = path;
		}

		public string Load() {
			if ( File.Exists(_path) ) {
				return File.ReadAllText(_path);
			}
			return string.Empty;
		}
	}
}
