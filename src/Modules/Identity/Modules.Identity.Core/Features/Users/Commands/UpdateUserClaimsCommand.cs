using System.Collections.Generic;
using MediatR;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Features.Users.Commands
{
    public class UpdateUserClaimsCommand : IRequest
    {
        public string UserId { get; init; }
        public IEnumerable<ClaimDto> Claims { get; init; }
    }
}