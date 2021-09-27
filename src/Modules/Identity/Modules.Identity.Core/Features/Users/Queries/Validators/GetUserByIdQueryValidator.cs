using FluentValidation;

namespace Modules.Identity.Core.Features.Users.Queries.Validators
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(q => q.Id).NotEmpty();
        }
    }
}