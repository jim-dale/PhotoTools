namespace PhotoMetadata.Models;

using System.Diagnostics.CodeAnalysis;
using System.IO;

[SuppressMessage("Performance", "CA1815:Override equals and operator equals on value types", Justification = "Not required.")]
public struct MatcherResult
{
    public int Depth { get; set; }
    public string RelativePath { get; set; }
    public DirectoryInfo Directory { get; set; }
    public FileInfo File { get; set; }
}
