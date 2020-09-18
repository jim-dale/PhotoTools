using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PhotoMetadata
{
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
                result = new List<InputMetadata>();
            }

            return result;
        }
    }
}
