using System.Collections.Generic;

namespace Shared.DTO.Schedule
{
    public class GroupScheduleDto
    {
        public string GroupNumber { get; init; }

        public string HalfYear { get; init; }
        public int Year { get; init; }
        public int Version { get; init; }

        public Dictionary<string, ClassDto[]> Classes { get; init; }
    }
}