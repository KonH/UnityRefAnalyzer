using UnityEngine;
using RefAnalyzer.Validation;

namespace RefAnalyzer.Core {
	public class RawDataNode {
		public Object SourceObj      { get; }
		public string SourceProperty { get; }
		public Object TargetObj      { get; }
		public string TargetMethod   { get; }

		public RawDataNode(Object sourceObj, string sourceProperty, Object targetObj, string targetMethod) {
			Guard.NotNull(sourceObj);
			Guard.NotNullOrEmpty(sourceProperty);
			Guard.NotNull(targetObj);
			Guard.NotNullOrEmpty(targetMethod);
			SourceObj      = sourceObj;
			SourceProperty = sourceProperty;
			TargetObj      = targetObj;
			TargetMethod   = targetMethod;
		}
	}
}
