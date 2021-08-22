using FluentValidation;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Features.User.Validators
{
    public class RegisterUserRequestValidator : AbstractValidator<SignupUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(r => r.Email).EmailAddress(); // TODO: replace with regex-based validation
            RuleFor(r => r.Password).NotEmpty().MinimumLength(6).MaximumLength(256);
            RuleFor(r => r.UserName).NotEmpty().MinimumLength(2).MaximumLength(64);
        }
    }
}