using System.Linq;
using Ardalis.Specification;
using Modules.Timetable.Core.Entities;

namespace Modules.Timetable.Core.Specifications
{
    public class GroupScheduleSpecification : Specification<Schedule>
    {
        public GroupScheduleSpecification(int groupId)
        {
            Query
                .Include(s => s.Classes
                    .Where(c => c.Groups
                        .Any(cg => cg.Id == groupId)
                    )
                );
        }
    }
}