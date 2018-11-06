using UnityEngine;

namespace RefAnalyzer {
	public static class AssetUtils {
		public static string GetTypeName(Object obj) {
			return obj ? obj.GetType().FullName : "null";
		}
		
		public static string GetPathTo(Object obj) {
			var component = obj as Component;
			return (component != null) ? GetPathTo(component.gameObject) : string.Empty;
		}

		public static string GetPathTo(GameObject go) {
			if ( go ) {
				var parentTrans = go.transform.parent;
				var parentPath = parentTrans ? GetPathTo(parentTrans.gameObject) + "/" : string.Empty;
				return parentPath + go.name;
			}
			return string.Empty;
		}
	}
}
