using System;
using System.Security.Claims;

namespace Shared.Core.Interfaces
{
    public interface ICurrentUser
    {
        public string Id { get; }
        public Claim[] Claims { get; }
        public bool HasClaim(Predicate<Claim> claim);
        public bool HasClaim(string type, string value);
        public bool HasPermission(string permission);
    }
}