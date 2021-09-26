using FluentValidation;

namespace Modules.Identity.Core.Features.Users.Commands.Validators
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
        }
    }
}