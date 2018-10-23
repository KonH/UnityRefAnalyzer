using UnityEngine;
using UnityEngine.Assertions;

namespace RefAnalyzer.Core {
	public class RawDataNode {
		public Object SourceObj      { get; }
		public string SourceProperty { get; }
		public Object TargetObj      { get; }
		public string TargetMethod   { get; }

		public RawDataNode(Object sourceObj, string sourceProperty, Object targetObj, string targetMethod) {
			Assert.IsNotNull(sourceObj);
			Assert.IsTrue(!string.IsNullOrEmpty(sourceProperty));
			Assert.IsNotNull(targetObj);
			Assert.IsTrue(!string.IsNullOrEmpty(targetMethod));
			SourceObj      = sourceObj;
			SourceProperty = sourceProperty;
			TargetObj      = targetObj;
			TargetMethod   = targetMethod;
		}
	}
}
