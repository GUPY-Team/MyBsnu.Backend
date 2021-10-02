using Ardalis.Specification;

namespace Modules.Timetable.Core.Specifications
{
    public class LatestTeacherScheduleSpecification : TeacherScheduleSpecification
    {
        public LatestTeacherScheduleSpecification(int teacherId) : base(teacherId)
        {
            Query.Where(s => s.IsPublished);
        }
    }
}