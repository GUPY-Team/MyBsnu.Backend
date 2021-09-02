using System.Collections.Generic;

namespace Shared.DTO.Schedule
{
    public class GroupScheduleDto
    {
        public int Year { get; init; }
        public string Semester { get; init; }

        public Dictionary<string, ClassDto[]> Classes { get; init; }
    }
}