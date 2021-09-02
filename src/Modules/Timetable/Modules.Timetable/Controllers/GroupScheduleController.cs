using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Modules.Timetable.Core.Features.Schedules.Queries;
using Shared.DTO.Schedule;
using Shared.Infrastructure.Controllers;

namespace Modules.Timetable.Controllers
{
    [ApiVersion("1.0")]
    public class GroupScheduleController : ApiControllerBase
    {
        [HttpGet("latest")]
        public async Task<ActionResult<GroupScheduleDto>> GetLatestGroupSchedule([FromQuery] GetLatestGroupScheduleQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{scheduleId:min(1)}")]
        public async Task<ActionResult<GroupScheduleDto>> GetGroupSchedule([FromRoute] int scheduleId, [FromQuery, BindRequired] int groupId)
        {
            var query = new GetGroupScheduleQuery
            {
                GroupId = groupId,
                ScheduleId = scheduleId
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}