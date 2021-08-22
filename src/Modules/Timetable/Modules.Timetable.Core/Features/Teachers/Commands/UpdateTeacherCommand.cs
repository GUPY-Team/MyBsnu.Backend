using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Teachers.Commands
{
    public class UpdateTeacherCommand : IRequest<TeacherDto>
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string ThirdName { get; init; }
    }
}