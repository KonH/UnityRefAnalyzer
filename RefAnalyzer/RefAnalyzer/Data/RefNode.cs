using System;

namespace RefAnalyzer.Data {
	public class RefNode {
		public string SourcePath    { get { return srcPath ; } }
		public string SourceType    { get { return srcType ; } }
		public string SourceProprty { get { return srcProp ; } }
		public string TargetPath    { get { return tgPath  ; } }
		public string TargetType    { get { return tgType  ; } }
		public string TargetMethod  { get { return tgMethod; } }
		
		string srcPath;
		string srcType;
		string srcProp;
		string tgPath;
		string tgType;
		string tgMethod;

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
			this.srcPath  = srcPath;
			this.srcType  = srcType;
			this.srcProp  = srcProp;
			this.tgPath   = tgPath;
			this.tgType   = tgType;
			this.tgMethod = tgMethod;
		}
	}
}
