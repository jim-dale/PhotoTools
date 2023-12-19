namespace PhotoMetadata;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.FileSystemGlobbing;
using PhotoMetadata.Models;
using PhotoMetadata.Utility;

internal static partial class Helpers
{
    public static List<PhotoFileInfo> GetPhotoContextsFromImageFiles(string path, string includes, string excludes)
    {
        var matcher = new Matcher()
            .AddIncludePatterns(includes)
            .AddExcludePatterns(excludes);

        var matches = matcher.GetMatcherResultsInFullPath(path);
        var query = from match in matches
                    let fileName = Path.GetFileNameWithoutExtension(match.File.Name)
                    select new PhotoFileInfo
                    {
                        FileName = fileName,
                        PhotoId = fileName.GetPhotoIdFromFileName(),
                        FilmId = match.Directory.Name.GetFilmIdFromDirectoryName(),
                        RelativePath = match.RelativePath,
                        PhotoPath = match.File.FullName
                    };

        return query.ToList();
    }
}
