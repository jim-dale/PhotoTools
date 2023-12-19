namespace PhotoMetadata.Utility;

using System;
using PhotoMetadata.Models;

public static class InputMetadataExtensions
{
    public static OutputMetadata TryConvertToOutputMetadata(this InputMetadata item)
    {
        ArgumentNullException.ThrowIfNull(item);

        OutputMetadata result = default;

        if (item.IsEmpty() == false)
        {
            var title = item.Caption ?? item.Description ?? item.Caption;
            var description = item.Description;
            var comments = item.Comments;
            var keywords = item.Keywords?.Split(';', StringSplitOptions.RemoveEmptyEntries);
            var (success, lat, lng) = item.GpsCoordinates.TryParseCoordinates();

            if (string.Equals(title, description, StringComparison.OrdinalIgnoreCase))
            {
                description = null;
            }
            if (string.Equals(title, comments, StringComparison.OrdinalIgnoreCase))
            {
                comments = null;
            }
            if (keywords?.Length == 0)
            {
                keywords = null;
            }

            result = new OutputMetadata
            {
                DocumentName = item.PhotoId,
                DateTimeOriginal = item.DateTaken == default ? default(DateTime?) : item.DateTaken,
                Keywords = keywords,
                Title = title,
                Description = description,
                Comments = comments
            };

            if (success)
            {
                result.GPSLatitude = lat;
                result.GPSLatitudeRef = lat < 0 ? "S" : "N";
                result.GPSLongitude = lng;
                result.GPSLongitudeRef = lng < 0 ? "W" : "E";
            }
        }

        return result;
    }
}
