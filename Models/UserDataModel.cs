using System.Text.Json.Serialization;

namespace TornOps.Models
{
    public class UserDataModel
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }

        [JsonPropertyName("player_id")]
        public required int PlayerId { get; set; }

        [JsonPropertyName("level")]
        public required int Level { get; set; }

        [JsonPropertyName("status")]
        public required StatusModel Status { get; set; }

        [JsonPropertyName("money_onhand")]
        public required long MoneyOnhand { get; set; }

        [JsonPropertyName("daily_networth")]
        public required long DailyNetworth { get; set; }

        [JsonPropertyName("cooldowns")]
        public required CooldownsModel Cooldowns { get; set; }

        [JsonPropertyName("energy")]
        public required BarSegment Energy { get; set; }

        [JsonPropertyName("nerve")]
        public required BarSegment Nerve { get; set; }

        [JsonPropertyName("happy")]
        public required BarSegment Happy { get; set; }

        [JsonPropertyName("life")]
        public required BarSegment Life { get; set; }

        [JsonPropertyName("chain")]
        
        public required ChainModel Chain { get; set; }
    }
}
