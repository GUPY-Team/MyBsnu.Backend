using System.Collections.Generic;
using MediatR;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Features.Claims.Queries
{
    public class GetClaimsQuery : IRequest<List<ClaimDto>>
    {
    }
}