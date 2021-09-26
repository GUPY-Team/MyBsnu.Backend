using MediatR;

namespace Modules.Identity.Core.Features.Users.Commands
{
    public class CreateUserCommand : IRequest
    {
        public string UserName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
    }
}