using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Schedules.Queries
{
    public class GetTeacherScheduleQuery : IRequest<TeacherScheduleDto>
    {
        public int ScheduleId { get; init; }
        public int TeacherId { get; init; }
    }
}