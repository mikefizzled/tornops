using System.Text.Json.Serialization;

namespace TornOps.Models
{
    public class EducationUserModel
    {
        [JsonPropertyName("education_current")] public int? EduCurrent { get; set; }
        [JsonPropertyName("education_timeleft")] public int? EduTimeLeft { get; set; }
        [JsonPropertyName("education_completed")] public List<int>? EduCourseComplete { get; set; }
    }
}
