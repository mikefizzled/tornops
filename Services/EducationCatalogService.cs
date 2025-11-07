using System.Text.Json;

using TornOps.Models;

namespace TornOps.Services
{


    public sealed class EducationCatalogService
    {
        private EducationCatalog? _catalog;

        // lookup tables
        // course id -> course
        private readonly Dictionary<int, EduCourse> _byCourseId = new();
        // course id -> discipline
        private readonly Dictionary<int, string> _disciplineByCourseId = new();

        // stop race conditions on loading
        private readonly SemaphoreSlim _gate = new(1, 1);

        public async Task EnsureLoadedAsync()
        {
            if (_catalog != null)
            {
                return;
            }

            await _gate.WaitAsync();
            try
            {
                if (_catalog != null) return;

                using var stream = await FileSystem.OpenAppPackageFileAsync("education_data.json");

                _catalog = await JsonSerializer.DeserializeAsync<EducationCatalog>(stream)
                            ?? new EducationCatalog(Array.Empty<Bachelors>());

                foreach (var discipline in _catalog.Education)
                {
                    foreach (var course in discipline.EducationCourse)
                    {
                        _byCourseId[course.Id] = course;
                        _disciplineByCourseId[course.Id] = discipline.Name;
                    }
                }
            }
            finally
            {
                _gate.Release();
            }
        }

        /// <summary>Get a course by id</summary>
        /// <returns>Education course or null if not found</returns>
        public EduCourse? GetCourse(int id) =>
            _byCourseId.TryGetValue(id, out var c) ? c : null;

        /// <summary>Get the discipline name (e.g., "Biology") for a course id.</summary>
        public string? GetDisciplineName(int courseId) =>
            _disciplineByCourseId.TryGetValue(courseId, out var n) ? n : null;

        ///<summary>Total number of courses in the same discipline as the given course.</summary>
        public int TotalCoursesInDisciplineOf(int courseId)
        {
            if (_catalog is null) return 0;
            var name = GetDisciplineName(courseId);
            if (name is null) return 0;

            var d = _catalog.Education.FirstOrDefault(x => x.Name == name);
            return d?.EducationCourse.Count ?? 0;
        }
    }
}
