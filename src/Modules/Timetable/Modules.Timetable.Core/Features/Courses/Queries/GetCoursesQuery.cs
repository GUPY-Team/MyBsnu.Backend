using System.Collections.Generic;
using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Courses.Queries
{
    public class GetCoursesQuery : IRequest<List<CourseDto>>
    {
    }
}