using System.Text.Json.Serialization;

namespace TornOps.Models
{

    public class StatusModel
    {
        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("details")]
        public string? Details { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;

        [JsonPropertyName("color")]
        public string Color { get; set; } = string.Empty;

        [JsonPropertyName("until")]
        public long? Until { get; set; }  // Unix timestamp
    }
}