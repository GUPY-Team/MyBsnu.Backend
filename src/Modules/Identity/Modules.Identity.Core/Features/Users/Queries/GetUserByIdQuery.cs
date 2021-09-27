using MediatR;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Features.Users.Queries
{
    public class GetUserByIdQuery : IRequest<AppUserDto>
    {
        public string Id { get; init; }
    }
}