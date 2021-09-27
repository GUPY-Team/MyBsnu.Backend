using MediatR;

namespace Modules.Identity.Core.Features.Users.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public string Id { get; init; }
    }
}