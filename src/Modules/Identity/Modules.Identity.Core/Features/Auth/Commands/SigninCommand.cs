using MediatR;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Features.Auth.Commands
{
    public class SigninUserCommand : IRequest<UserSignedInResponse>
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}