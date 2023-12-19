namespace PhotoMetadata.Utility;

using System;
using System.Text.RegularExpressions;

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

        if (name is not null && name.Length >= 6)
        {
            if (ApsFilmIdPattern().IsMatch(name))
            {
                // APS film
                result = name[..7];
            }
            else
            {
                if (DigitalFilmIdPattern().IsMatch(name))
                {   // Digital photo [yyyy]-[mm]
                    result = name[..7];
                }
                else if (Classic35mmDateFilmIdPattern().IsMatch(name))
                {
                    result = name.Substring(7, 6);
                }
                else if (Classic35mmFilmIdPattern().IsMatch(name))
                {
                    result = name[..6];
                }
            }
        }

        return result;
    }

    public static string GetPhotoIdFromFileName(this string name)
    {
        string result = string.Empty;

        if (name is not null && name.Length >= 6)
        {
            if (ApsPhotoIdPattern().IsMatch(name))
            {
                
                result = name[..11];
            }
            else
            {
                if (DigitalPhotoIdPattern().IsMatch(name))
                {   
                    result = name[..19];
                }
                else if (Classic35mmPhotoIdPattern().IsMatch(name))
                {
                    var length = Math.Min(name.Length, 10);
                    result = name[..length];
                }
            }
        }

        return result;
    }

    // APS film id [six digit film id]
    [GeneratedRegex(@"^\d{3}-\d{3}")]
    private static partial Regex ApsFilmIdPattern();

    // Digital film id [yyyy]-[mm]
    [GeneratedRegex(@"^\d{4}-\d{2}")]
    private static partial Regex DigitalFilmIdPattern();

    // 35mm film id [film id]
    [GeneratedRegex(@"^\d{6}")]
    private static partial Regex Classic35mmFilmIdPattern();

    // 35mm film id [yyyyMM] [film id]
    [GeneratedRegex(@"^\d{6} \d{6}")]
    private static partial Regex Classic35mmDateFilmIdPattern();

    // APS photos [six digit film id]_[photo num]
    [GeneratedRegex(@"^\d{3}-\d{3}_\d{3}")]
    private static partial Regex ApsPhotoIdPattern();

    // Digital photos (date and time) [yyyy]-[MM]-[dd]T[hh]-[mm]-[ss]
    [GeneratedRegex(@"^\d{4}-\d{2}-\d{2}T\d{2}-\d{2}-\d{2}")]
    private static partial Regex DigitalPhotoIdPattern();

    // 35mm photos [film id]_[photo num]
    [GeneratedRegex(@"^\d{6}_[\d-]{2,3}")]
    private static partial Regex Classic35mmPhotoIdPattern();
}
