using System.Collections.Generic;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Shared.Core.Behaviors;
using Shared.Infrastructure.Middleware;

namespace Shared.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(ServiceCollectionExtensions).Assembly);
            
            services.AddLogging();

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ReportApiVersions = true;
            });

            services.AddRouting(o => o.LowercaseUrls = true);

            services.AddControllers()
                .AddFluentValidation(o => { o.DisableDataAnnotationsValidation = true; });

            services.AddLocalization(o => o.ResourcesPath = "Resources");

            services.AddSwagger();

            services.AddCorsPolicy();

            services.AddMemoryCache();
            
            services.RegisterDiServices();

            return services;
        }

        public static IServiceCollection AddDatabaseContext<T>(this IServiceCollection services, IConfiguration configuration)
            where T : DbContext
        {
            services.AddDbContext<T>(o => { o.UseNpgsql(configuration.GetConnectionString("postgres")); });
            return services;
        }

        private static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });

                o.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    },
                });
            });
        }

        private static void AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(o => { o.AddDefaultPolicy(pb => { pb.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); }); });
        }

        private static void RegisterDiServices(this IServiceCollection services)
        {
            services.AddTransient<ExceptionHandlerMiddleware>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
        } 
    }
}