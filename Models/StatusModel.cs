using System.Net;
using System.Text.Json.Serialization;

namespace TornOps.Models
{
    public class StatusModel
    {
        [JsonPropertyName("description")] public string? Description { get; set; }
        [JsonPropertyName("details")] public string? Details { get; set; }
        [JsonPropertyName("state")] public string? State { get; set; }
        [JsonPropertyName("color")] public string? Color { get; set; }
        [JsonPropertyName("until")] public long? Until { get; set; }

    }
}
