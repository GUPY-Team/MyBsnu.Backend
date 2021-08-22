using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Teachers.Queries
{
    public class GetTeacherByIdQuery : IRequest<TeacherDto>
    {
        public int Id { get; init; }
    }
}