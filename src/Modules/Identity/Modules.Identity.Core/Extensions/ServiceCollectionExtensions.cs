using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Identity.Core.Settings;

namespace Modules.Identity.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityCore(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .Configure<JwtSettings>(configuration.GetSection("Jwt"))
                .AddMediatR(typeof(ServiceCollectionExtensions).Assembly)
                .AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly)
                .AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
            return services;
        }
    }
}