using System;
using Newtonsoft.Json;

namespace RefAnalyzer.Data {
	public class RefNode {
		[JsonProperty(PropertyName = "srcPath")]
		public string SourcePath     { get; private set; }
		[JsonProperty(PropertyName = "srcType")]
		public string SourceType     { get; private set; }
		[JsonProperty(PropertyName = "srcProp")]
		public string SourceProperty { get; private set; }
		[JsonProperty(PropertyName = "tgPath")]
		public string TargetPath     { get; private set; }
		[JsonProperty(PropertyName = "tgType")]
		public string TargetType     { get; private set; }
		[JsonProperty(PropertyName = "tgMethod")]
		public string TargetMethod   { get; private set; }

		public RefNode(string srcPath, string srcType, string srcProp, string tgPath, string tgType, string tgMethod) {
			if ( string.IsNullOrEmpty(srcPath) ) {
				throw new ArgumentException("srcPath");
			}
			if ( string.IsNullOrEmpty(srcType) ) {
				throw new ArgumentException("srcType");
			}
			if ( string.IsNullOrEmpty(srcProp) ) {
				throw new ArgumentException("srcProp");
			}
			if ( string.IsNullOrEmpty(tgPath) ) {
				throw new ArgumentException("tgPath");
			}
			if ( string.IsNullOrEmpty(tgType) ) {
				throw new ArgumentException("tgType");
			}
			if ( string.IsNullOrEmpty(tgMethod) ) {
				throw new ArgumentException("tgMethod");
			}
			SourcePath     = srcPath;
			SourceType     = srcType;
			SourceProperty = srcProp;
			TargetPath     = tgPath;
			TargetType     = tgType;
			TargetMethod   = tgMethod;
		}
	}
}
