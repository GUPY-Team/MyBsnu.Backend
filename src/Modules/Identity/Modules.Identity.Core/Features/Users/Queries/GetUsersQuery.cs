using MediatR;
using Shared.Core.Constants;
using Shared.Core.Features.Queries;
using Shared.DTO;
using Shared.DTO.Identity;

namespace Modules.Identity.Core.Features.Users.Queries
{
    public class GetUsersQuery : IRequest<PagedList<AppUserListDto>>, IPagedQuery
    {
        public int Page { get; init; } = NumericConstants.PaginationMinPage;
        public int PageSize { get; init; } = NumericConstants.PaginationDefaultPageSize;
    }
}