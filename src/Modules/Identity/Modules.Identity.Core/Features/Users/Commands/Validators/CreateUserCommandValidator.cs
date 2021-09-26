using FluentValidation;

namespace Modules.Identity.Core.Features.Users.Commands.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(c => c.Email).EmailAddress(); // TODO: replace with regex-based validation
            RuleFor(c => c.Password).NotEmpty().MinimumLength(6).MaximumLength(256);
            RuleFor(c => c.UserName).NotEmpty().MinimumLength(2).MaximumLength(64);
        }
    }
}