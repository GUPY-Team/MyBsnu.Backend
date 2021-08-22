using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Identity.Core.Abstractions;
using Modules.Identity.Core.Features.User;
using Modules.Identity.Core.Settings;

namespace Modules.Identity.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityCore(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .Configure<JwtSettings>(configuration.GetSection("Jwt"))
                .AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly)
                .AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly)
                .AddScoped<IUserService, UserService>()
                .AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}