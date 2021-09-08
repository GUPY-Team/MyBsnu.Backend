using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Schedules.Commands
{
    public class UpdateScheduleCommand : IRequest<ScheduleDto>
    {
        public int Id { get; init; }
        public int Year { get; init; }
        public string Semester { get; init; }
    }
}