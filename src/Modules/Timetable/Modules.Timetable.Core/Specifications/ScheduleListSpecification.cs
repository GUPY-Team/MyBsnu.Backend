using Ardalis.Specification;
using Modules.Timetable.Core.Entities;

namespace Modules.Timetable.Core.Specifications
{
    public class ScheduleListSpecification : Specification<Schedule>
    {
        public ScheduleListSpecification()
        {
            Query
                .OrderByDescending(s => s.Year)
                .ThenByDescending(s => s.Semester)
                .ThenByDescending(s => s.Version);
        }
    }
}