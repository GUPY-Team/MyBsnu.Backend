using System.Collections.Generic;
using MediatR;

namespace Modules.Identity.Core.Features.Users.Commands
{
    public class UpdateUserClaimsCommand : IRequest
    {
        public string UserId { get; init; }
        public IEnumerable<string> Claims { get; init; }
    }
}