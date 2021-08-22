using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Teachers.Commands
{
    public class CreateTeacherCommand : IRequest<TeacherDto>
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Patronymic { get; init; }
    }
}