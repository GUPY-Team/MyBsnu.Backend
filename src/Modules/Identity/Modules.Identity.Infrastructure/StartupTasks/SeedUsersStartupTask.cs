using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Modules.Identity.Core.Entities;
using Shared.Infrastructure.Interfaces;
using Permission = Shared.Core.Constants.Permissions;

namespace Modules.Identity.Infrastructure.StartupTasks
{
    public class SeedUsersStartupTask : IStartupTask
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SeedUsersStartupTask> _logger;

        public SeedUsersStartupTask(IServiceProvider serviceProvider, ILogger<SeedUsersStartupTask> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
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

            var result = await userManager.CreateAsync(user, "P@assword#12345");
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => $"{e.Code}: {e.Description}");
                _logger.LogError("Can't create default admin user. Errors: {0}", string.Join('\n', errors));
            }
            
            await userManager.AddClaimsAsync(user, new[]
            {
                new Claim("email", user.Email),
                new Claim("userName", user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(Permission.PermissionClaimType,
                    Permission.SuperAdmin.Replace($"{Permission.PermissionsPrefix}.", ""))
            });
        }
    }
}