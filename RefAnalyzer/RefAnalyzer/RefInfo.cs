using RefAnalyzer.Validation;

namespace RefAnalyzer
{
    public class RefInfo
    {
        public RefInfo(string sourcePath, string sourceType, string sourceProperty,
                       string targetPath, string scenePath)
        {
            Guard.NotNullOrEmpty(sourcePath);
            Guard.NotNullOrEmpty(sourceType);
            Guard.NotNullOrEmpty(sourceProperty);
            Guard.NotNullOrEmpty(targetPath);

            SourcePath = sourcePath;
            SourceType = sourceType;
            SourceProperty = sourceProperty;
            TargetPath = targetPath;
            ScenePath = scenePath;
        }

        public string SourcePath { get; }
        public string SourceType { get; }
        public string SourceProperty { get; }
        public string TargetPath { get; }
        public string ScenePath { get; }
    }
}