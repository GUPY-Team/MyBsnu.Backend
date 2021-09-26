using MediatR;

namespace Modules.Identity.Core.Features.Auth.Commands
{
    public class SignupUserCommand : IRequest
    {
        public string UserName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
    }
}