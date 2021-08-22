using System.Collections.Generic;
using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Teachers.Queries
{
    public class GetTeachersQuery : IRequest<List<TeacherDto>>
    {
    }
}