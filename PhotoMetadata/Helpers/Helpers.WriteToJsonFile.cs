using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace PhotoMetadata
{
    internal static partial class Helpers
    {
        internal static async Task WriteToJsonFile<T>(string path, T item)
        {
            var jsonString = JsonSerializer.Serialize(item, new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = true
            });

            await File.WriteAllTextAsync(path, jsonString);
        }
    }
}
