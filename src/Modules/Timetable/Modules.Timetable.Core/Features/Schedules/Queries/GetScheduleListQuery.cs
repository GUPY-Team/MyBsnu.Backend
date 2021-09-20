using MediatR;
using Shared.Core.Constants;
using Shared.Core.Features.Queries;
using Shared.DTO;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Schedules.Queries
{
    public class GetScheduleListQuery : IRequest<PagedList<ScheduleDto>>, IPagedQuery
    {
        public int Page { get; init; } = NumericConstants.PaginationMinPage;
        public int PageSize { get; init; } = NumericConstants.PaginationDefaultPageSize;
    }
}