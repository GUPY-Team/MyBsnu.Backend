using Microsoft.EntityFrameworkCore;
using Modules.Timetable.Core.Abstractions;
using Modules.Timetable.Core.Entities;
using Shared.Infrastructure.Persistence;

namespace Modules.Timetable.Infrastructure.Persistence
{
    public class ScheduleDbContext : ModuleDbContext, IScheduleDbContext
    {
        protected override string SchemaName => "Schedule";

        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Audience> Audiences { get; set; }

        public ScheduleDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}