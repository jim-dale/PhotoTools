namespace PhotoMetadata.Models;

public class PhotoFileInfo
{
    public string FileName { get; set; }
    public string PhotoId { get; set; }
    public string FilmId { get; set; }
    public string PhotoPath { get; set; }
    public string RelativePath { get; internal set; }
}
