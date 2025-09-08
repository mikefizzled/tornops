using System.Text.Json.Serialization;

namespace TornOps.Models
{
    public class CooldownsModel
    {
        [JsonPropertyName("drug")] public int? DrugCooldown { get; set; }
        [JsonPropertyName("medical")] public int MedicalCooldown { get; set; }
        [JsonPropertyName("booster")] public int BoosterCooldown { get; set; }
    }
}
