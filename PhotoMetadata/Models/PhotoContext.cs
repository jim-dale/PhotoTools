namespace PhotoMetadata.Models;
public class PhotoContext
{
    public PhotoFileInfo Info { get; set; }
    public InputMetadata Current { get; set; }
    public InputMetadata Previous { get; set; }
    public InputMetadata Changed { get; internal set; }
    public OutputMetadata Output { get; set; }
}
