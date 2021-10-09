using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Modules.Identity.Core.Entities;

namespace Modules.Identity.Infrastructure.BackgroundServices
{
    public class UserSeedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<UserSeedService> _logger;

        public UserSeedService(IServiceProvider serviceProvider, ILogger<UserSeedService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var adminUser = await userManager.FindByEmailAsync("lain@gmail.com");
            if (adminUser != null)
            {
                return;
            }

            var user = new AppUser
            {
                Email = "lain@gmail.com",
                UserName = "Lain"
            };

            var result = await userManager.CreateAsync(user, "12345678");
            if (result.Succeeded)
            {
                return;
            }

            var errors = result.Errors.Select(e => $"{e.Code}: {e.Description}");
            _logger.LogError("Can't create default admin user. Errors: {0}", string.Join('\n', errors));
        }
    }
}