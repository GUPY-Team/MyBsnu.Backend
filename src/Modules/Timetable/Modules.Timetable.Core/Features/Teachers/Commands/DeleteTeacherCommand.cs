using MediatR;

namespace Modules.Timetable.Core.Features.Teachers.Commands
{
    public class DeleteTeacherCommand : IRequest
    {
        public int Id { get; init; }
    }
}