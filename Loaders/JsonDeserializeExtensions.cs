using System.Text.Json;
using JorgeSerrano.Json;

namespace MessengerDataAnalyzer.Loaders;

public static class JsonDeserializeExtensions
{
    private static readonly JsonSerializerOptions DefaultSerializerSettings = new (JsonSerializerDefaults.General)
    {
        PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy()
    };


    public static T DeserializeUsingDefaultSettings<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json, DefaultSerializerSettings)!;
    }
}