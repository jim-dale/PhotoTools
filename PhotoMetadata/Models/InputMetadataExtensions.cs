using System;

namespace PhotoMetadata
{
    public static class InputMetadataExtensions
    {
        public static OutputMetadata TryConvertToOutputMetadata(this InputMetadata item)
        {
            OutputMetadata result = default;

            if (item.IsEmpty() == false)
            {
                var title = item.Caption ?? item.Description ?? item.Caption;
                var description = item.Description;
                var comments = item.Comments;
                var keywords = item.Keywords?.Split(';', StringSplitOptions.RemoveEmptyEntries);
                var coordinates = item.GpsCoordinates.TryParseCoordinates();

                if (string.Equals(title, description, StringComparison.CurrentCultureIgnoreCase))
                {
                    description = null;
                }
                if (string.Equals(title, comments, StringComparison.CurrentCultureIgnoreCase))
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
                    DateTimeOriginal = (item.DateTaken == default) ? default(DateTime?) : item.DateTaken,
                    Keywords = keywords,
                    Title = title,
                    Description = description,
                    Comments = comments
                };

                if (coordinates.success)
                {
                    result.GPSLatitude = coordinates.lat;
                    result.GPSLatitudeRef = (coordinates.lat < 0) ? "S" : "N";
                    result.GPSLongitude = coordinates.lng;
                    result.GPSLongitudeRef = (coordinates.lng < 0) ? "W" : "E";
                }
            }

            return result;
        }
    }
}
