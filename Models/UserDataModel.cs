using System.Text.Json.Serialization;
using TornOps.Models;

namespace TornOps.Models
{
    public class UserDataModel
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("player_id")]
        public int? PlayerId { get; set; }

        [JsonPropertyName("level")]
        public int? Level { get; set; }


        [JsonPropertyName("cooldowns")]
        public CooldownsModel? Cooldowns { get; set; }

        [JsonPropertyName("energy")]
        public BarSegment? Energy { get; set; }

        [JsonPropertyName("nerve")]
        public BarSegment? Nerve { get; set; }

        [JsonPropertyName("happy")]
        public BarSegment? Happy { get; set; }

        [JsonPropertyName("life")]
        public BarSegment? Life { get; set; }

        [JsonPropertyName("chain")]
        
        public ChainModel? Chain { get; set; }

        [JsonPropertyName("travel")]
        public TravelModel? Travel { get; set; }

        [JsonPropertyName("status")]
        public StatusModel? Status { get; set; }

        #region Education
        [JsonPropertyName("education_current")]
        public int? EducationCurrent
        {
            get; set;
        }
        [JsonPropertyName("education_timeleft")]
        public int? EducationTimeLeft
        {
            get; set;
        }
        [JsonPropertyName("education_completed")]
        public List<int>? EducationCompleted
        {
            get; set;
        }

        public EducationUserModel EducationUser => new()
        {
            EduCurrent = EducationCurrent,
            EduTimeLeft = EducationTimeLeft,
            EduCourseComplete = EducationCompleted ?? new List<int>()
        };
        #endregion

        #region Money
        [JsonPropertyName("money_onhand")] public long? MoneyOnhand { get; set; }
        [JsonPropertyName("points")] public int? Points { get; set; }
        [JsonPropertyName("cayman_bank")] public long? CaymanBank { get; set; }
        [JsonPropertyName("daily_networth")] public long? DailyNetworth { get; set; }
        [JsonPropertyName("city_bank")] public CityBankModel? CityBank { get; set; }
        #endregion
    }
}
