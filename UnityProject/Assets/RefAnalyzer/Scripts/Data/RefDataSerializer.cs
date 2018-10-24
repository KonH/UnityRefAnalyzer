using UnityEngine;
using UnityEngine.Assertions;

namespace RefAnalyzer.Data {
	public class RefDataSerializer {
		readonly RefData _data;
		readonly bool    _prettyPrint;
		
		public RefDataSerializer(RefData data, bool prettyPrint = true) {
			Assert.IsNotNull(data);
			_data        = data;
			_prettyPrint = prettyPrint;
		}

		public string Serialize() {
			return JsonUtility.ToJson(_data, _prettyPrint);
		}
	}
}
