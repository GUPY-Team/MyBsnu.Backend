using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Schedules.Queries
{
    public class GetGroupScheduleQuery : IRequest<GroupScheduleDto>
    {
        public int ScheduleId { get; init; }
        public int GroupId { get; init; }
    }
}