using System.Linq;
using UnityEditor;
using UnityEngine;

namespace RefAnalyzer.UI {
	public static class MenuItemsDev {
		const string FileName = "../package.unitypackage";

		[MenuItem("RefAnalyzer/Export Package")]
		static void ExportPackage() {
			var allAssets = AssetDatabase.FindAssets("", new []{"Assets/RefAnalyzer"}).Select(guid => AssetDatabase.GUIDToAssetPath(guid)).ToArray();
			var assetsToExport = allAssets.Where(path => !IsIgnoredPath(path)).ToArray();
			Debug.Log("Exporting assets: \n" + string.Join(",\n", assetsToExport));
			AssetDatabase.ExportPackage(assetsToExport, FileName);
			Debug.LogFormat("Assets exported to '{0}'", FileName);
		}

		static bool IsIgnoredPath(string path) {
			return path.ContainsAny("Scenes", "Examples", "Tests") || path.EndsWith("Dev.cs");
		}

		public static bool ContainsAny(this string str, params string[] parts) {
			return parts.Any(p => str.Contains(p));
		}
	}
}