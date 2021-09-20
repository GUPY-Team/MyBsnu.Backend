using FluentValidation;
using Shared.Core.Constants;

namespace Shared.Core.Features.Queries.Validators
{
    public class PagedQueryValidator : AbstractValidator<IPagedQuery>
    {
        public PagedQueryValidator()
        {
            RuleFor(q => q.Page).GreaterThanOrEqualTo(NumericConstants.PaginationMinPage);
            RuleFor(q => q.PageSize).GreaterThan(0).LessThanOrEqualTo(NumericConstants.PaginationMaxPageSize);
        }
    }
}