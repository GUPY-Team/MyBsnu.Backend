using MediatR;

namespace Modules.Timetable.Core.Features.Courses.Commands
{
    public class DeleteCourseCommand : IRequest
    {
        public int Id { get; init; }
    }
}