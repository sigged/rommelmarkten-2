using System.Text.Json.Serialization;

namespace Rommelmarkten.ApiClient.Common.Payloads
{
    public class ProblemDetails :IApiPayload
    {
        public string? Type { get; set; }

        public string? Title { get; set; }

        public int Status { get; set; }

        public string? Detail { get; set; }

        public string? Instance { get; }

        private IDictionary<string, object>? _additionalProperties;

        [JsonExtensionData]
        public IDictionary<string, object> AdditionalProperties
        {
            get { return _additionalProperties ?? (_additionalProperties = new Dictionary<string, object>()); }
            set { _additionalProperties = value; }
        }

    }
}
