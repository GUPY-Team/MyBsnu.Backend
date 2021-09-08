using MediatR;

namespace Modules.Timetable.Core.Features.Schedules.Commands
{
    public class DeleteScheduleCommand : IRequest
    {
        public int Id { get; init; }
    }
}