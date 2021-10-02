using System.Linq;
using Ardalis.Specification;
using Modules.Timetable.Core.Entities;

namespace Modules.Timetable.Core.Specifications
{
    public class OverlappingScheduleClassesSpecification : ScheduleClassesAtPeriodSpecification
    {
        public OverlappingScheduleClassesSpecification(Class @class)
            : base(@class.ScheduleId, @class.StartTime, @class.WeekDay, @class.WeekType)
        {
            Query
                .Where(c => c.Id != @class.Id)
                .Where(c =>
                    c.Teachers.Any(t => @class.Teachers.Select(x => x.Id).Contains(t.Id)) ||
                    c.Audiences.Any(t => @class.Audiences.Select(x => x.Id).Contains(t.Id)) ||
                    c.Groups.Any(t => @class.Groups.Select(x => x.Id).Contains(t.Id))
                );
        }
    }
}