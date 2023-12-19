namespace PhotoMetadata;

using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

internal static partial class Helpers
{
    private static readonly JsonSerializerOptions jsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        WriteIndented = true
    };

    internal static async Task WriteToJsonFile<T>(string path, T item)
    {
        string jsonString = JsonSerializer.Serialize(item, jsonSerializerOptions);

        await File.WriteAllTextAsync(path, jsonString).ConfigureAwait(false);
    }
}
