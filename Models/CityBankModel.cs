using System.Text.Json.Serialization;

namespace TornOps.Models
{
    public class CityBankModel
    {
        [JsonPropertyName("amount")] public long Amount { get; set; }
        [JsonPropertyName("time_left")] public int Time_Left { get; set; }
    }
}