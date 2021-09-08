using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Schedules.Commands
{
    public class PublishScheduleCommand : IRequest<ScheduleDto>
    {
        public int Id { get; init; }
    }
}