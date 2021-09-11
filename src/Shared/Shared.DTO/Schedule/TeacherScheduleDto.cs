using System.Collections.Generic;

namespace Shared.DTO.Schedule
{
    public class TeacherScheduleDto
    {
        public Dictionary<string, ClassDto[]> Classes { get; init; }
    }
}