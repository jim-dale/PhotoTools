using System;
using System.Text.RegularExpressions;

namespace PhotoMetadata
{
    public static partial class StringExtensions
    {
        public static (bool success, double lat, double lng) TryParseCoordinates(this string str)
        {
            (bool, double, double) result = default;

            if (string.IsNullOrWhiteSpace(str) == false)
            {
                var parts = str.Split(',', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    if (double.TryParse(parts[0].Trim(), out var lat)
                        && double.TryParse(parts[1].Trim(), out var lng))
                    {
                        result = (true, lat, lng);
                    }
                }
            }
            return result;
        }

        public static string GetFilmIdFromDirectoryName(this string name)
        {
            string result = string.Empty;

            if (name.Length >= 6)
            {
                if (Regex.IsMatch(name, @"^\d{3}-\d{3}"))
                {
                    // APS film
                    result = name.Substring(0, 7);
                }
                else
                {
                    if (Regex.IsMatch(name, @"^\d{4}-\d{2}"))
                    {   // Digital photo [yyyy]-[mm]
                        result = name.Substring(0, 7);
                    }
                    else if (Regex.IsMatch(name, @"^\d{6} \d{6}"))
                    {
                        result = name.Substring(7, 6);
                    }
                    else if (Regex.IsMatch(name, @"^\d{6}"))
                    {
                        result = name.Substring(0, 6);
                    }
                }
            }

            return result;
        }

        public static string GetPhotoIdFromFileName(this string name)
        {
            string result = string.Empty;

            if (name.Length >= 6)
            {
                if (Regex.IsMatch(name, @"^\d{3}-\d{3}_\d{3}"))
                {
                    // APS film
                    result = name.Substring(0, 11);
                }
                else
                {
                    if (Regex.IsMatch(name, @"^\d{4}-\d{2}-\d{2}T\d{2}-\d{2}-\d{2}"))
                    {   // Digital photo [yyyy]-[mm]
                        result = name.Substring(0, 19);
                    }
                    else if (Regex.IsMatch(name, @"^\d{6}_[\d-]{2,3}"))
                    {
                        var length = Math.Min(name.Length, 10);
                        result = name.Substring(0, length);
                    }
                }
            }

            return result;
        }
    }
}
