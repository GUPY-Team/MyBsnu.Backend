using MediatR;
using Shared.DTO;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Classes.Commands
{
    public class CreateClassCommand : IRequest<ClassDto>
    {
        public int Format { get; init; }
        public int Type { get; init; }
        public int DayOfWeek { get; init; }
        public int WeekType { get; init; }
        public TimeDto StartTime { get; init; }
        public TimeDto EndTime { get; init; }
        public int CourseId { get; init; }
        public int TeacherId { get; init; }
        public int? AudienceId { get; init; }
        public int ScheduleId { get; init; }
    }
}