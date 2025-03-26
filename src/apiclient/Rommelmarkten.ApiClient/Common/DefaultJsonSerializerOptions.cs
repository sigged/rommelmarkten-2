using System.Text.Json;
using System.Text.Json.Serialization;

namespace Rommelmarkten.ApiClient.Common
{
    internal static class ApiSerializerOptions
    {
        internal static JsonSerializerOptions Default => new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };
    }
}
