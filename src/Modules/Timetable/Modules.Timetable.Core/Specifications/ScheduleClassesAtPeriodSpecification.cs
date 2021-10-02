using System;
using Ardalis.Specification;
using Modules.Timetable.Core.Entities;
using Modules.Timetable.Core.Enums;

namespace Modules.Timetable.Core.Specifications
{
    public class ScheduleClassesAtPeriodSpecification : Specification<Class>
    {
        public ScheduleClassesAtPeriodSpecification(int scheduleId, TimeSpan startTime, WeekDay weekDay, WeekType weekType)
        {
            Query
                .Where(c => c.ScheduleId == scheduleId)
                .Where(c => c.StartTime == startTime)
                .Where(c => c.WeekDay == weekDay)
                .Where(c => weekType == null || c.WeekType == null || c.WeekType == weekType);
        }
    }
}