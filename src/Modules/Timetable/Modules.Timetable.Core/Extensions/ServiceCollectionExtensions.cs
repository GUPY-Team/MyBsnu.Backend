using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Modules.Timetable.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTimetableCore(this IServiceCollection services)
        {
            services
                .AddMediatR(typeof(ServiceCollectionExtensions).Assembly)
                .AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly)
                .AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
            return services;
        }
    }
}