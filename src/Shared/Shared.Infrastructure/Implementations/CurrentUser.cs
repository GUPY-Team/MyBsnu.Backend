using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Shared.Core.Constants;
using Shared.Core.Interfaces;

namespace Shared.Infrastructure.Implementations
{
    public class CurrentUser : ICurrentUser
    {
        private readonly ClaimsPrincipal _user;

        public string Id { get; }
        public Claim[] Claims { get; }

        public CurrentUser(IHttpContextAccessor contextAccessor)
        {
            _user = contextAccessor.HttpContext?.User;

            Id = _user?.FindFirstValue(ClaimTypes.NameIdentifier);
            Claims = _user?.Claims.ToArray();
        }

        public bool HasClaim(Predicate<Claim> claim) => _user.HasClaim(claim);

        public bool HasClaim(string type, string value) => _user.HasClaim(type, value);

        public bool HasPermission(string permission) => _user.HasClaim(Permissions.PermissionClaimType, permission);
    }
}