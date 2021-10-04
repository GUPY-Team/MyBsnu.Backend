using System.Collections.Generic;

namespace Shared.DTO.Schedule
{
    public record ClassDto
    {
        public int Id { get; init; }
        public string Format { get; init; }
        public string Type { get; init; }
        public string WeekDay { get; init; }
        public string WeekType { get; init; }
        public string StartTime { get; init; }
        public string EndTime { get; init; }
        public string Duration { get; init; }

        public CourseDto Course { get; init; }

        public int ScheduleId { get; init; }

        public IEnumerable<TeacherDto> Teachers { get; init; } = new List<TeacherDto>();
        public IEnumerable<AudienceDto> Audiences { get; init; } = new List<AudienceDto>();
        public IEnumerable<GroupDto> Groups { get; init; } = new List<GroupDto>();
    }
}