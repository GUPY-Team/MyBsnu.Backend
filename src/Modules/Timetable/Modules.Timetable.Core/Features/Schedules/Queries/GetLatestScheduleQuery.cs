using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Schedules.Queries
{
    public class GetLatestScheduleQuery : IRequest<GroupScheduleDto>
    {
        public int GroupId { get; init; }
    }
}