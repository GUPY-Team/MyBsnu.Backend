using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Schedules.Queries
{
    public class GetLatestTeacherScheduleQuery : IRequest<TeacherScheduleDto>
    {
        public int TeacherId { get; init; }
    }
}