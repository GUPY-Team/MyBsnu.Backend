using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Timetable.Core.Features.Courses.Commands;
using Modules.Timetable.Core.Features.Courses.Queries;
using Shared.Core.Constants;
using Shared.DTO.Schedule;
using Shared.Infrastructure.Controllers;

namespace Modules.Timetable.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = Permissions.CanManageCourses)]
    public class CoursesController : ApiControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<List<CourseDto>>> GetCourses()
        {
            var result = await Mediator.Send(new GetCoursesQuery());
            return Ok(result);
        }

        [HttpGet("{Id:min(1)}")]
        [AllowAnonymous]
        public async Task<ActionResult<CourseDto>> GetCourseById([FromRoute] GetCourseByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<CourseDto>> CreateCourse([FromBody] CreateCourseCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        
        [HttpPut]
        public async Task<ActionResult<CourseDto>> UpdateCourse([FromBody] UpdateCourseCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
        
        [HttpDelete("{Id:min(1)}")]
        public async Task<ActionResult> DeleteCourse([FromRoute] DeleteCourseCommand command)
        {
            await  Mediator.Send(command);
            return NoContent();
        }
    }
}