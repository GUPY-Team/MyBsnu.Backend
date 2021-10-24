using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Modules.Identity.Core.Abstractions;
using Modules.Identity.Core.Entities;
using Modules.Identity.Core.Settings;
using Modules.Identity.Infrastructure.Permissions;
using Modules.Identity.Infrastructure.Services;
using Modules.Identity.Infrastructure.StartupTasks;
using Shared.Infrastructure.Extensions;
using IdentityDbContext = Modules.Identity.Infrastructure.Persistence.IdentityDbContext;

namespace Modules.Identity.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            services
                .AddDatabaseContext<IdentityDbContext>(configuration)
                .AddScoped<IIdentityDbContext>(p => p.GetService<IdentityDbContext>())
                .AddIdentity<AppUser, AppRole>(o =>
                {
                    if (environment.IsDevelopment())
                    {
                        o.Password.RequireDigit = false;
                        o.Password.RequireLowercase = false;
                        o.Password.RequireNonAlphanumeric = false;
                        o.Password.RequireUppercase = false;
                    }

                    o.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddStartupTask<SeedUsersStartupTask>();

            services.AddPermissions();
            services.AddJwtAuth(configuration, environment);

            return services;
        }

        private static void AddPermissions(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        }

        private static void AddJwtAuth(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostEnvironment environment)
        {
            var jwtSettings = configuration.GetSection("Jwt").Get<JwtSettings>();

            services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    if (environment.IsDevelopment())
                    {
                        o.RequireHttpsMetadata = false;
                    }

                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}