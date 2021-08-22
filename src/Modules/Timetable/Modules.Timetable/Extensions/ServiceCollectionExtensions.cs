using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Timetable.Core.Extensions;
using Modules.Timetable.Infrastructure.Extensions;

namespace Modules.Timetable.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTimetableModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTimetableCore()
                .AddTimetableInfrastructure(configuration);
            return services;
        }
    }
}