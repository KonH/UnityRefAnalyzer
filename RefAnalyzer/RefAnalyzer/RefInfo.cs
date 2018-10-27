using System;

namespace RefAnalyzer {
	public class RefInfo {
		public string SourcePath     { get; }
		public string SourceType     { get; }
		public string SourceProperty { get; }
		public string TargetPath     { get; }
		public string ScenePath      { get; }

		public RefInfo(string srcPath, string srcType, string srcProperty, string tgPath, string scenePath) {
			if ( string.IsNullOrEmpty(srcPath) ) {
				throw new ArgumentException(nameof(srcPath));
			}
			if ( string.IsNullOrEmpty(srcType) ) {
				throw new ArgumentException(nameof(srcType));
			}
			if ( string.IsNullOrEmpty(srcProperty) ) {
				throw new ArgumentException(nameof(srcProperty));
			}
			if ( string.IsNullOrEmpty(tgPath) ) {
				throw new ArgumentException(nameof(tgPath));
			}
			SourcePath     = srcPath;
			SourceType     = srcType;
			SourceProperty = srcProperty;
			TargetPath     = tgPath;
			ScenePath      = scenePath;
		}
	}
}
