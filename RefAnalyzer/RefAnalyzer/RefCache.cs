using System;
using System.Collections.Generic;
using RefAnalyzer.Data;
using MethodDict =
	System.Collections.Generic.Dictionary
	<string,System.Collections.Generic.List<RefAnalyzer.RefInfo>>;
using ClassToMethodDict =
	System.Collections.Generic.Dictionary
	<string, System.Collections.Generic.Dictionary
		<string, System.Collections.Generic.List<RefAnalyzer.RefInfo>>>;

namespace RefAnalyzer {
	public class RefCache {
		readonly ClassToMethodDict _classNameToInfo = new ClassToMethodDict();

		public RefCache(RefData data) {
			if ( data == null ) {
				throw new ArgumentNullException(nameof(data));
			}
			Fill(data);
		}

		void Fill(RefData data) {
			foreach ( var scene in data.Scenes ) {
				foreach ( var node in scene.Nodes ) {
					MethodDict methods;
					if ( !_classNameToInfo.TryGetValue(node.TargetType, out methods) ) {
						methods = new MethodDict();
						_classNameToInfo.Add(node.TargetType, methods);
					}
					List<RefInfo> refs;
					if ( !methods.TryGetValue(node.TargetMethod, out refs) ) {
						refs = new List<RefInfo>();
						methods.Add(node.TargetMethod, refs);
					}
					refs.Add(new RefInfo(node.SourcePath, node.SourceType, node.SourceProperty, node.TargetPath, scene.Path));
				}
			}
		}

		public List<RefInfo> GetRefs(string className, string methodName) {
			if ( _classNameToInfo.TryGetValue(className, out var infos) ) {
				if ( infos.TryGetValue(methodName, out var refs) ) {
					return refs;
				}
			}
			return null;
		}
	}
}
