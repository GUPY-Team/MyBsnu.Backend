using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Timetable.Core.Features.Schedules.Commands;
using Modules.Timetable.Core.Features.Schedules.Queries;
using Shared.Core.Constants;
using Shared.DTO;
using Shared.DTO.Schedule;
using Shared.Infrastructure.Controllers;

namespace Modules.Timetable.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = Permissions.CanManageSchedule)]
    public class ScheduleController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PagedList<ScheduleDto>>> GetScheduleList([FromQuery] GetScheduleListQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{Id:min(1)}")]
        public async Task<ActionResult<ScheduleDto>> GetSchedule([FromRoute] GetScheduleByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ScheduleDto>> CreateSchedule([FromBody] CreateScheduleCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ScheduleDto>> UpdateSchedule([FromBody] UpdateScheduleCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{Id:min(1)}")]
        public async Task<ActionResult> DeleteSchedule([FromRoute] DeleteScheduleCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{SourceId:min(1)}/copy")]
        public async Task<ActionResult> CopySchedule([FromRoute] CopyScheduleCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("{Id:min(1)}/publish")]
        public async Task<ActionResult<ScheduleDto>> Publish([FromRoute] PublishScheduleCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

    }
}