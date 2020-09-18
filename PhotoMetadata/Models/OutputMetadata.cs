using System;

namespace PhotoMetadata
{
    public class OutputMetadata
    {
        public string DocumentName { get; set; }
        public DateTime? DateTimeOriginal { get; set; }
#pragma warning disable CA1819 // Properties should not return arrays
        public string[] Keywords { get; set; }
#pragma warning restore CA1819 // Properties should not return arrays
        public string Title { get; set; }
        public string Description { get; set; }
        public string Comments { get; set; }
        public double? GPSLatitude { get; set; }
        public string GPSLatitudeRef { get; set; }
        public double? GPSLongitude { get; set; }
        public string GPSLongitudeRef { get; set; }
    }
}
