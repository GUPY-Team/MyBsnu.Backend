using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Schedules.Queries
{
    public class GetScheduleByIdQuery : IRequest<ScheduleDto>
    {
        public int Id { get; init; }
    }
}