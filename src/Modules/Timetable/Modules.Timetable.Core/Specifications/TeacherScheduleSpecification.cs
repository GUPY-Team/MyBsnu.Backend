using System.Linq;
using Ardalis.Specification;
using Modules.Timetable.Core.Entities;

namespace Modules.Timetable.Core.Specifications
{
    public class TeacherScheduleSpecification : Specification<Schedule>
    {
        public TeacherScheduleSpecification(int teacherId)
        {
            Query
                .Include(s => s.Classes
                    .Where(c => c.Teachers
                        .Select(t => t.Id)
                        .Contains(teacherId)
                    )
                );
        }
    }
}