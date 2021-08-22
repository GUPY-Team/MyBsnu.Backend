using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Courses.Queries
{
    public class GetCourseByIdQuery : IRequest<CourseDto>
    {
        public int Id { get; init; }
    }
}