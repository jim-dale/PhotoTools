namespace PhotoMetadata.Models;

using System;
using System.Diagnostics.CodeAnalysis;

public class OutputMetadata
{
    public string DocumentName { get; set; }
    public DateTime? DateTimeOriginal { get; set; }
    [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "Simple data transfer object.")]
    public string[] Keywords { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Comments { get; set; }
    public double? GPSLatitude { get; set; }
    public string GPSLatitudeRef { get; set; }
    public double? GPSLongitude { get; set; }
    public string GPSLongitudeRef { get; set; }
}
