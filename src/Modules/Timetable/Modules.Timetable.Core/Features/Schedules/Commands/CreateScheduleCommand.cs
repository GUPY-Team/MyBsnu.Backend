using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Schedules.Commands
{
    public class CreateScheduleCommand : IRequest<ScheduleDto>
    {
        public string Semester { get; init; }
        public int Year { get; init; }
    }
}