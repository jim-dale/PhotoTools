using System.Diagnostics.CodeAnalysis;

namespace PhotoMetadata
{
    public class AppOptions
    {
        public const string Name = nameof(AppOptions);

        [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "DTO")]
        public string[] ExcelFiles { get; set; }
        public string MetadataStorePath { get; set; } = "photo-metadata.json";
        public string InputPath { get; set; }
        public string OutputPath { get; set; }
        public string Includes { get; set; } = "*.tif";
        public string Excludes { get; set; }
        public string ExiftoolPath { get; set; } = "exiftool.exe";
        public string ImageMagickPath { get; set; } = @"magick.exe";
        public bool Initialise { get; set; }
        public bool Force { get; set; }
    }
}
