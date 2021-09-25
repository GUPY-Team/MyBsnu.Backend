using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Modules.Identity.Core.Extensions;
using Modules.Identity.Infrastructure.Extensions;

namespace Modules.Identity.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityModule(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            services
                .AddIdentityCore(configuration)
                .AddIdentityInfrastructure(configuration, environment);

            return services;
        }
    }
}