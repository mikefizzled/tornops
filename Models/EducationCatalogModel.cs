using System.Text.Json.Serialization;

namespace TornOps.Models
{
    public sealed record EducationCatalog(
        [property: JsonPropertyName("education")]       IReadOnlyList<Bachelors> Education
        );

    public sealed record Bachelors(
        [property: JsonPropertyName("id")]              int Id,
        [property: JsonPropertyName("name")]            string Name,
        [property: JsonPropertyName("courses")]         IReadOnlyList<EduCourse> EducationCourse
        );
    public sealed record EduCourse(
        [property: JsonPropertyName("id")]              int Id,
        [property: JsonPropertyName("code")]            string Code,
        [property: JsonPropertyName("name")]            string Name,
        [property: JsonPropertyName("description")]     string Description,
        [property: JsonPropertyName("duration")]        int Duration,
        [property: JsonPropertyName("rewards")]         EduRewards Reward,
        [property: JsonPropertyName("prerequisites")]   EduPrereqs Prerequisites
        );
    public sealed record EduRewards(
        [property: JsonPropertyName("working_stats")]   EduWorkingStats WorkingStats,
        [property: JsonPropertyName("effect")]          string? Effect,
        [property: JsonPropertyName("honor")]           int? Honor
        );
    public sealed record EduWorkingStats(
        [property: JsonPropertyName("manual_labor")]    int? ManualLabour,
        [property: JsonPropertyName("intelligence")]    int? Intelligence,
        [property: JsonPropertyName("endurance")]       int? Endurance
        );
    public sealed record EduPrereqs(
        [property: JsonPropertyName("cost")]            int Cost,
        [property: JsonPropertyName("courses")]         IReadOnlyList<int> Courses
        );
}
