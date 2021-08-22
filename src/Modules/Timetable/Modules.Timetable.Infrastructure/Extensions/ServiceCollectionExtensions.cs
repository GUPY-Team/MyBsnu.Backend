using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Timetable.Core.Abstractions;
using Modules.Timetable.Infrastructure.Persistence;
using Shared.Infrastructure.Extensions;

namespace Modules.Timetable.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTimetableInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDatabaseContext<ScheduleDbContext>(configuration)
                .AddScoped<IScheduleDbContext, ScheduleDbContext>(p => p.GetService<ScheduleDbContext>());
            return services;
        }
    }
}