namespace PhotoMetadata;

using System;
using ImageMagick;

internal static partial class Helpers
{
    [Obsolete]
    public static void ResizeAndWriteMainImage(string input, string output)
    {
        var geometry = new MagickGeometry("@1500000");

        using var collection = new MagickImageCollection(input);
        int maxWidth = 0;
        int index = int.MinValue;
        for (int i = 0; i < collection.Count; i++)
        { 
            if (collection[i].Width > maxWidth)
            {
                maxWidth = collection[i].Width;
                index = i;
            }
        }

        if (index > int.MinValue)
        {
            var image = collection[index];

            if (image != null)
            {
                image.FilterType = FilterType.Quadratic;
                image.Resize(geometry);

                image.Format = MagickFormat.Jpeg;
                image.Quality = 85;

                image.Write(output);
            }
        }
    }
}
