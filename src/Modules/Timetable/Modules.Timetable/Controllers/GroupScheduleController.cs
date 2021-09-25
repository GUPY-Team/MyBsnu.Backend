using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Timetable.Core.Features.Schedules.Queries;
using Shared.Core.Constants;
using Shared.DTO.Schedule;
using Shared.Infrastructure.Controllers;

namespace Modules.Timetable.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = Permissions.CanManageSchedule)]
    public class GroupScheduleController : ApiControllerBase
    {
        [HttpGet("latest")]
        [AllowAnonymous]
        public async Task<ActionResult<GroupScheduleDto>> GetLatestGroupSchedule([FromQuery] GetLatestGroupScheduleQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GroupScheduleDto>> GetGroupSchedule([FromQuery] GetGroupScheduleQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}