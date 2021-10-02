using FluentValidation;
using Shared.Core.Features.Queries.Validators;

namespace Modules.Identity.Core.Features.Users.Queries.Validators
{
    public class GetUsersQueryValidator : AbstractValidator<GetUsersQuery>
    {
        public GetUsersQueryValidator()
        {
            Include(new PagedQueryValidator());
        }
    }
}