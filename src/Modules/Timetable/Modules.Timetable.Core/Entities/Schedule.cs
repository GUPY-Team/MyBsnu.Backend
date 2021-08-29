using System.Collections.Generic;
using Modules.Timetable.Core.Enums;
using Shared.Core.Domain;

namespace Modules.Timetable.Core.Entities
{
    public class Schedule : Entity<int>
    {
        public Semester Semester { get; set; }
        public int Year { get; set; }
        public int Version { get; set; }
        public bool IsPublished { get; set; }

        public ICollection<Class> Classes { get; set; }
    }
}