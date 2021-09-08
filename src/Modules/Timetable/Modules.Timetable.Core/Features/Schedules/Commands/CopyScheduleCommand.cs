using MediatR;

namespace Modules.Timetable.Core.Features.Schedules.Commands
{
    public class CopyScheduleCommand : IRequest
    {
        public int SourceId { get; init; }
    }
}