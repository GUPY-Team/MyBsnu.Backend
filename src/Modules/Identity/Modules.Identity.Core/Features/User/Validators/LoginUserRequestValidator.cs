using FluentValidation;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Features.User.Validators
{
    public class LoginUserRequestValidator : AbstractValidator<SigninUserRequest>
    {
        public LoginUserRequestValidator()
        {
            RuleFor(r => r.Email).EmailAddress();
            RuleFor(r => r.Password).NotEmpty();
        }
    }
}