using MediatR;
using Shared.Core.Behaviors;
using Shared.Core.Constants;
using Shared.Core.Models;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Features.Users.Queries
{
    public class GetUsersQuery : IRequest<PagedList<AppUserListDto>>, IPagedQuery
    {
        public int Page { get; init; } = CommonConstants.Pagination.DefaultPage;
        public int PageSize { get; init; } = CommonConstants.Pagination.DefaultPageSize;
    }
}