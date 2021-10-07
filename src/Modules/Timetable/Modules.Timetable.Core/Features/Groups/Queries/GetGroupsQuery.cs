using MediatR;
using Shared.Core.Behaviors;
using Shared.Core.Constants;
using Shared.Core.Models;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Groups.Queries
{
    public class GetGroupsQuery : IRequest<PagedList<GroupDto>>, IPagedQuery
    {
        public int Page { get; init; } = CommonConstants.Pagination.DefaultPage;
        public int PageSize { get; init; } = CommonConstants.Pagination.DefaultPageSize;
    }
}