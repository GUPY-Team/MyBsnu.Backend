using System;
using System.Collections.Generic;
using Modules.Timetable.Core.Enums;
using Shared.Core.Domain;

namespace Modules.Timetable.Core.Entities
{
    public class Class : Entity<int>
    {
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public TimeSpan Duration => EndTime - StartTime;

        public EducationFormat Format { get; set; }
        public ClassType Type { get; set; }
        public WeekDay WeekDay { get; set; }
        public WeekType WeekType { get; set; }

        public Course Course { get; set; }
        public int CourseId { get; set; }

        public Schedule Schedule { get; set; }
        public int ScheduleId { get; set; }

        public ICollection<Group> Groups { get; set; } = new List<Group>();
        public ICollection<Audience> Audiences { get; set; } = new List<Audience>();
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}