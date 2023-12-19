namespace PhotoMetadata;

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using PhotoMetadata.Models;

internal static partial class Helpers
{
    public static List<InputMetadata> LoadAllMetadataFromJsonFile(string storeFileName)
    {
        List<InputMetadata> result;

        if (File.Exists(storeFileName))
        {
            var jsonString = File.ReadAllText(storeFileName);
            result = JsonSerializer.Deserialize<List<InputMetadata>>(jsonString);
        }
        else
        {
            result = [];
        }

        return result;
    }
}
