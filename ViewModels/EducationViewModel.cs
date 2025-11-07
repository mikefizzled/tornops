using CommunityToolkit.Mvvm.ComponentModel;
using TornOps.Models;
using TornOps.Services;
using TornOps.Helpers;

namespace TornOps.ViewModels
{
    public partial class EducationViewModel : ObservableObject
    {
        // Data from User Data API
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsStudying))]
        [NotifyPropertyChangedFor(nameof(DisciplineProgressText))]
        public partial int? CourseId
        {
            get; private set;
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(TimeRemaining))]
        [NotifyPropertyChangedFor(nameof(IsStudying))]
        public partial int? TimeLeftSeconds
        {
            get; private set;
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CompletedCount))]
        [NotifyPropertyChangedFor(nameof(DisciplineProgressText))]
        public partial List<int> CompletedIds { get; private set; } = new();

        // Datafrom from Catalog
        [ObservableProperty]
        public partial string? CourseName
        {
            get; private set;
        }

        [ObservableProperty]
        public partial string? CourseCode
        {
            get; private set;
        }

        [ObservableProperty]
        public partial string? Discipline
        {
            get; private set;
        }

        [ObservableProperty]
        public partial int? CourseDurationSeconds
        {
            get; private set;
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DisciplineProgressText))]
        public partial int TotalInDiscipline
        {
            get; private set;
        }

        public string TimeRemaining => StatFormatter.FormatCooldownOrNone(TimeLeftSeconds);
        public bool IsStudying => (CourseId ?? 0) > 0 && (TimeLeftSeconds ?? 0) > 0;
        public int CompletedCount => CompletedIds?.Count ?? 0;

        public string? DisciplineProgressText =>
            $"{CompletedCount} / 131 courses";

        public async Task UpdateFromAsync(EducationUserModel? userEdu, EducationCatalogService catalog)
        {
            await catalog.EnsureLoadedAsync();

            CourseId = userEdu?.EduCurrent;
            TimeLeftSeconds = userEdu?.EduTimeLeft;
            CompletedIds = userEdu?.EduCourseComplete ?? new List<int>();

            if (CourseId is > 0)
            {
                var c = catalog.GetCourse(CourseId.Value);
                CourseName = c?.Name;
                CourseCode = c?.Code;
                CourseDurationSeconds = c?.Duration;
                Discipline = c is null ? null : catalog.GetDisciplineName(c.Id);
            }
            else
            {
                CourseName = CourseCode = Discipline = null;
                CourseDurationSeconds = null;
                TotalInDiscipline = 0;
            }
        }
    }
}
