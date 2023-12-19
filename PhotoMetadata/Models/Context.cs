namespace PhotoMetadata.Models;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public class Context
{
    public AppOptions Options { get; } = new AppOptions();

    [SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "Simple data transfer object.")]
    public List<InputMetadata> CurrentEntries { get; set; }

    [SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "Simple data transfer object.")]
    public List<InputMetadata> PreviousEntries { get; set; }

    [SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "Simple data transfer object.")]
    public List<PhotoFileInfo> Items { get; set; }

    [SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "Simple data transfer object.")]
    public List<InputMetadata> Changes { get; set; }

    [SuppressMessage("Design", "CA1002:Do not expose generic lists", Justification = "Simple data transfer object.")]
    public List<OutputMetadata> ToUpdate { get; set; }
}
