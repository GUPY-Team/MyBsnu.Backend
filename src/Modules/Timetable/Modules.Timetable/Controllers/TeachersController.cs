using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modules.Timetable.Core.Features.Teachers.Commands;
using Modules.Timetable.Core.Features.Teachers.Queries;
using Shared.DTO.Schedule;
using Shared.Infrastructure.Controllers;

namespace Modules.Timetable.Controllers
{
    [ApiVersion("1.0")]
    public class TeachersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<TeacherDto>>> GetTeachers()
        {
            var result = await Mediator.Send(new GetTeachersQuery());
            return Ok(result);
        }

        [HttpGet("{Id:min(1)}")]
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