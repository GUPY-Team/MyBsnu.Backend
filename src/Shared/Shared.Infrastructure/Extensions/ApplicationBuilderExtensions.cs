using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Serilog;
using Shared.Infrastructure.Middleware;

namespace Shared.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSharedMiddleware(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging();

            app.UseRequestLocalization(o =>
            {
                var supportedCultures = new[] { "uk-UA", "en-US", "ru-RU" };

                o.SetDefaultCulture(supportedCultures[0]);
                o.AddSupportedCultures(supportedCultures);
                o.AddSupportedUICultures(supportedCultures);

                o.ApplyCurrentCultureToResponseHeaders = true;
            });

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/ping", context => context.Response.WriteAsync("Pong!"));
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.RoutePrefix = "swagger";
                o.DisplayRequestDuration();
            });

            return app;
        }
    }
}