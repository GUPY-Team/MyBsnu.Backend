using FluentValidation;

namespace Modules.Identity.Core.Features.Auth.Commands.Validators
{
    public class LoginUserCommandValidator : AbstractValidator<SigninUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c => c.Password).NotEmpty();
        }
    }
}