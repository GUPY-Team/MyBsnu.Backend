using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Courses.Commands
{
    public class CreateCourseCommand : IRequest<CourseDto>
    {
        public string Name { get; init; }
    }
}