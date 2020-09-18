using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.FileSystemGlobbing;

namespace PhotoMetadata
{
    public static partial class MatcherExtensions
    {
        public static IEnumerable<MatcherResult> GetMatcherResultsInFullPath(this Matcher item, string path)
        {
            var results = EnumerateFiles(item, new DirectoryInfo(path), 0, string.Empty);
            foreach (var result in results)
            {
                yield return result;
            }
        }

        public static Matcher AddIncludePatterns(this Matcher result, string includes)
        {
            if (string.IsNullOrWhiteSpace(includes) == false)
            {
                var patterns = includes.Split(';', StringSplitOptions.RemoveEmptyEntries);
                result.AddIncludePatterns(patterns);
            }

            return result;
        }

        public static Matcher AddExcludePatterns(this Matcher result, string excludes)
        {
            if (string.IsNullOrWhiteSpace(excludes) == false)
            {
                var patterns = excludes.Split(';', StringSplitOptions.RemoveEmptyEntries);
                result.AddExcludePatterns(patterns);
            }

            return result;
        }

        private static IEnumerable<MatcherResult> EnumerateFiles(Matcher matcher, DirectoryInfo directory, int depth, string relativePath)
        {
            var files = from file in directory.EnumerateFiles()
                        where matcher.Match(file.Name).HasMatches
                        select file;

            foreach (var file in files)
            {
                yield return new MatcherResult
                {
                    Depth = depth,
                    RelativePath = relativePath,
                    Directory = directory,
                    File = file
                };
            }

            var subDirectories = directory.EnumerateDirectories();
            foreach (var subDirectory in subDirectories)
            {
                var subFiles = EnumerateFiles(matcher, subDirectory, depth + 1, Path.Combine(relativePath, subDirectory.Name));
                foreach (var subFile in subFiles)
                {
                    yield return subFile;
                }
            }
        }
    }
}
