using System.IO;
using RefAnalyzer.Validation;

namespace RefAnalyzer.Data
{
    public class RefDataLoader
    {
        private readonly string _path;

        public RefDataLoader(string path)
        {
            Guard.NotNullOrEmpty(path);
            _path = path;
        }

        public string Load()
        {
            return File.Exists(_path)
                ? File.ReadAllText(_path)
                : string.Empty;
        }
    }
}