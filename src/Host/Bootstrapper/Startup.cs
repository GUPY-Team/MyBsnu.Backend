using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.Identity.Extensions;
using Modules.Timetable.Extensions;
using Shared.Infrastructure.Extensions;

namespace Bootstrapper
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private IWebHostEnvironment WebHostEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            WebHostEnvironment = webHostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSharedServices(Configuration, WebHostEnvironment)
                .AddIdentityModule(Configuration, WebHostEnvironment)
                .AddTimetableModule(Configuration);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSharedMiddleware(WebHostEnvironment);
        }
    }
}