using System.Collections.Generic;

namespace PhotoMetadata
{
    public class Context
    {
        public AppOptions Options { get; } = new AppOptions();
        public List<InputMetadata> CurrentEntries { get; set; }
        public List<InputMetadata> PreviousEntries { get; set; }
        public List<PhotoFileInfo> Items { get; set; }
        public List<InputMetadata> Changes { get; set; }
        public List<OutputMetadata> ToUpdate { get; set; }
    }
}
