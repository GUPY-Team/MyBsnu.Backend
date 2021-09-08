using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modules.Timetable.Core.Features.Schedules.Commands;
using Modules.Timetable.Core.Features.Schedules.Queries;
using Shared.DTO.Schedule;
using Shared.Infrastructure.Controllers;

namespace Modules.Timetable.Controllers
{
    [ApiVersion("1.0")]
    public class ScheduleController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ScheduleDto>>> GetScheduleList()
        {
            var result = await Mediator.Send(new GetScheduleListQuery());
            return Ok(result);
        }

        [HttpGet("{Id:min(1)}")]
        public async Task<ActionResult<ScheduleDto>> GetSchedule([FromRoute] GetScheduleByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ScheduleDto>> UpdateSchedule([FromBody] UpdateScheduleCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CopySchedule([FromQuery] CopyScheduleCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{Id:min(1)}")]
        public async Task<ActionResult> DeleteSchedule([FromRoute] DeleteScheduleCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}