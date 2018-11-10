using RefAnalyzer.Validation;
using Newtonsoft.Json;

namespace RefAnalyzer.Data {
	public class RefDataSerializer {
		readonly RefData _data;
		
		public RefDataSerializer(RefData data) {
			Guard.NotNull(data);
			_data        = data;
		}

		public string Serialize() {
			return JsonConvert.SerializeObject(_data);
		}
	}
}
