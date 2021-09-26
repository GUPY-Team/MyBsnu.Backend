using FluentValidation;

namespace Modules.Identity.Core.Features.Users.Commands.Validators
{
    public class UpdateUserClaimsCommandValidator : AbstractValidator<UpdateUserClaimsCommand>
    {
        public UpdateUserClaimsCommandValidator()
        {
            RuleFor(c => c.UserId).NotEmpty();
        }
    }
}