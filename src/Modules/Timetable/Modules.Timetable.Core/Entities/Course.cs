using Shared.Core.Domain;

namespace Modules.Timetable.Core.Entities
{
    public class Course : Entity<int>
    {
        public string Name { get; set; }
    }
}