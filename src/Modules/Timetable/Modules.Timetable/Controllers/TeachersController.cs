using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Timetable.Core.Features.Teachers.Commands;
using Modules.Timetable.Core.Features.Teachers.Queries;
using Shared.Core.Constants;
using Shared.Core.Models;
using Shared.DTO.Schedule;
using Shared.Infrastructure.Controllers;

namespace Modules.Timetable.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = Permissions.ScheduleEditor)]
    public class TeachersController : ApiControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<TeacherDto>>> GetTeachers([FromQuery] GetTeachersQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{Id:min(1)}")]
        [AllowAnonymous]
        public async Task<ActionResult<TeacherDto>> GetTeacher([FromRoute] GetTeacherByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TeacherDto>> CreateTeacher([FromBody] CreateTeacherCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<TeacherDto>> UpdateTeacher([FromBody] UpdateTeacherCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{Id:min(1)}")]
        public async Task<ActionResult> DeleteTeacher([FromRoute] DeleteTeacherCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}