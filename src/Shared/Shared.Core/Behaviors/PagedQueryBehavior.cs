using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using Shared.Core.Constants;
using Shared.Core.Domain;
using Shared.Core.Interfaces;
using Shared.Localization;

namespace Shared.Core.Behaviors
{
    public interface IPagedQuery
    {
        public int Page { get; init; }
        public int PageSize { get; init; }
    }

    public class PagedQueryValidator : AbstractValidator<IPagedQuery>
    {
        public PagedQueryValidator()
        {
            RuleFor(q => q.Page).GreaterThanOrEqualTo(CommonConstants.Pagination.MinPage);
            RuleFor(q => q.PageSize)
                .GreaterThanOrEqualTo(CommonConstants.Pagination.MinPageSize)
                .LessThanOrEqualTo(CommonConstants.Pagination.ExtendedMaxPageSize);
        }
    }

    public class PagedQueryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IPagedQuery
    {
        private readonly ICurrentUser _currentUser;
        private readonly IStringLocalizer<Locale> _localizer;

        public PagedQueryBehavior(ICurrentUser currentUser, IStringLocalizer<Locale> localizer)
        {
            _currentUser = currentUser;
            _localizer = localizer;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var pagedQueryValidator = new PagedQueryValidator();
            var validationResult = await pagedQueryValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .GroupBy(f => f.PropertyName)
                    .ToDictionary(g => g.Key, g => g.Select(f => f.ErrorMessage).ToArray());
                throw new EntityNotValidException(_localizer.GetString("errors.InvalidPagination"), errors);
            }

            if (_currentUser.Id == null && request.PageSize >= CommonConstants.Pagination.DefaultMaxPageSize)
            {
                throw new EntityNotFoundException(_localizer.GetString("errors.InvalidPagination"));
            }

            return await next();
        }
    }
}