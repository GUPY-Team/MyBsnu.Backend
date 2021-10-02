using Ardalis.Specification;

namespace Modules.Timetable.Core.Specifications
{
    public class LatestGroupScheduleSpecification : GroupScheduleSpecification
    {
        public LatestGroupScheduleSpecification(int groupId) : base(groupId)
        {
            Query.Where(s => s.IsPublished);
        }
    }
}