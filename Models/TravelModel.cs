using System.Text.Json.Serialization;

namespace TornOps.Models
{ 
    public class TravelModel
    {
        [JsonPropertyName("destination")] public string? Destination { get; set; }
        [JsonPropertyName("method")] public string? Method { get; set; }
        [JsonPropertyName("timestamp")] public long? Timestamp { get; set; }
        [JsonPropertyName("departed")] public long? Departed { get; set; }
        [JsonPropertyName("time_left")] public int? TimeLeft { get; set; }
    }
}