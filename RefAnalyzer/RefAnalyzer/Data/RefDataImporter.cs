using Newtonsoft.Json;

namespace RefAnalyzer.Data
{
    public class RefDataImporter
    {
        private readonly string _contents;

        public RefDataImporter(string contents)
        {
            _contents = contents;
        }

        public RefData Import()
        {
            return JsonConvert.DeserializeObject<RefData>(_contents);
        }
    }
}