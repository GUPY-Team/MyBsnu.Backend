using System.Collections.Generic;
using System.Security.Claims;

namespace Modules.Identity.Core.Abstractions
{
    public interface IJwtTokenService
    {
        string CreateToken(IEnumerable<Claim> userClaims);
    }
}