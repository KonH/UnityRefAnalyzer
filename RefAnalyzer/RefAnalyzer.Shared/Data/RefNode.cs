using RefAnalyzer.Validation;
using Newtonsoft.Json;

namespace RefAnalyzer.Data {
	public class RefNode {
		[JsonProperty(PropertyName = "srcPath")]
		public string SourcePath { get; private set; }

		[JsonProperty(PropertyName = "srcType")]
		public string SourceType { get; private set; }

		[JsonProperty(PropertyName = "srcProp")]
		public string SourceProperty { get; private set; }

		[JsonProperty(PropertyName = "tgPath")]
		public string TargetPath { get; private set; }

		[JsonProperty(PropertyName = "tgType")]
		public string TargetType { get; private set; }

		[JsonProperty(PropertyName = "tgMethod")]
		public string TargetMethod { get; private set; }

		public RefNode(string srcPath, string srcType, string srcProp,
					   string tgPath, string tgType, string tgMethod) {
			Guard.NotNullOrEmpty(srcPath);
			Guard.NotNullOrEmpty(srcType);
			Guard.NotNullOrEmpty(srcProp);
			Guard.NotNullOrEmpty(tgPath);
			Guard.NotNullOrEmpty(tgType);
			Guard.NotNullOrEmpty(tgMethod);

			SourcePath     = srcPath;
			SourceType     = srcType;
			SourceProperty = srcProp;
			TargetPath     = tgPath;
			TargetType     = tgType;
			TargetMethod   = tgMethod;
		}
	}
}