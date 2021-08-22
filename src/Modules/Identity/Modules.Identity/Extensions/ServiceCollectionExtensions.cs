using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Identity.Core.Extensions;
using Modules.Identity.Infrastructure.Extensions;

namespace Modules.Identity.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddIdentityCore(configuration)
                .AddIdentityInfrastructure(configuration);

            return services;
        }
    }
}