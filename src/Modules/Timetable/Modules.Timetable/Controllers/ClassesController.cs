using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modules.Timetable.Core.Features.Classes.Commands;
using Modules.Timetable.Core.Features.Classes.Queries;
using Shared.DTO.Schedule;
using Shared.Infrastructure.Controllers;

namespace Modules.Timetable.Controllers
{
    [ApiVersion("1.0")]
    public class ClassesController : ApiControllerBase
    {
        [HttpGet("{Id:min(1)}")]
        public async Task<IActionResult> GetClass([FromRoute] GetClassByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ClassDto>> CreateClass([FromBody] CreateClassCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClass([FromBody] UpdateClassCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id:min(1)}")]
        public async Task<IActionResult> DeleteClass([FromRoute] DeleteClassCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}