using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Shared.Core.Constants;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Features.Claims.Queries
{
    public class ClaimsQueryHandler : IRequestHandler<GetClaimsQuery, List<ClaimDto>>
    {
        public Task<List<ClaimDto>> Handle(GetClaimsQuery request, CancellationToken cancellationToken)
        {
            var claims = Permissions.All.Select(c => new ClaimDto
            {
                Type = Permissions.PermissionsClaimType,
                Value = c.Replace($"{Permissions.PermissionsPrefix}.", "")
            }).ToList();

            return Task.FromResult(claims);
        }
    }
}