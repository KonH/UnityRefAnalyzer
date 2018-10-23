using UnityEngine;
using UnityEngine.Assertions;

namespace RefAnalyzer.Data {
	public class RefDataSerializer {
		readonly RefData _data;

		public RefDataSerializer(RefData data) {
			Assert.IsNotNull(data);
			_data = data;
		}

		public string Serialize() {
			return JsonUtility.ToJson(_data);
		}
	}
}
