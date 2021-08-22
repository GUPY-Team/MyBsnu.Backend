using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Courses.Commands
{
    public class UpdateCourseCommand : IRequest<CourseDto>
    {
        public int Id { get; init; }
        
        public string Name { get; init; }
    }
}