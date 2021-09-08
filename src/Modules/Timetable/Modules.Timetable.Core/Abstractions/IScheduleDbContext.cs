using Microsoft.EntityFrameworkCore;
using Modules.Timetable.Core.Entities;
using Shared.Core.Interfaces;

namespace Modules.Timetable.Core.Abstractions
{
    public interface IScheduleDbContext : IDbContext
    {
        DbSet<Schedule> Schedules { get; set; }
        DbSet<Class> Classes { get; set; }
        DbSet<Teacher> Teachers { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<Course> Courses { get; set; }
        DbSet<Audience> Audiences { get; set; }
    }
}