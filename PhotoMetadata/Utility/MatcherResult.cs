using System.IO;

namespace PhotoMetadata
{
    public struct MatcherResult
    {
        public int Depth { get; set; }
        public string RelativePath { get; set; }
        public DirectoryInfo Directory { get; set; }
        public FileInfo File { get; set; }
    }
}
