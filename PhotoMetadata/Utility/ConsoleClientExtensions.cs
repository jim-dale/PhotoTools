namespace PhotoMetadata.Utility;

using System;

public static partial class ConsoleClientExtensions
{
    public static void AddPhotoMetadata(this ConsoleClient item, string imageFile, string metadataFile)
    {
        ArgumentNullException.ThrowIfNull(item);

        var args = new string[]
        {
            "-overwrite_original",
            "-MakerNotes:all=",
            "-json=\"" + metadataFile + "\"",
            "\"" + imageFile + "\"",
        };

        item.Run(args);
    }

    public static void ResizeAndWriteMainImage(this ConsoleClient item, string inputFile, string outputFile)
    {
        ArgumentNullException.ThrowIfNull(item);

        var args = new string[]
        {
            "convert",
            "\"" + inputFile + "\"" + "[0]",
            "-resize @1500000",
            "-format JPEG",
            "-quality 85",
            "\"" + outputFile + "\"",
        };

        item.Run(args);
    }
}
