using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using PermissionConstants = Shared.Core.Constants.Permissions;

namespace Modules.Identity.Infrastructure.Permissions
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var policySatisfied = context.User.Claims
                .Where(c => c.Type == PermissionConstants.PermissionClaimType)
                .Select(c => $"{PermissionConstants.PermissionsPrefix}.{c.Value}")
                .Any(value => value == requirement.Permission || value == PermissionConstants.SuperAdmin);

            if (policySatisfied)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}