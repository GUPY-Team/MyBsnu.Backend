using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Timetable.Core.Features.ScheduleDesigner.Queries;
using Shared.Core.Constants;
using Shared.DTO.Schedule;
using Shared.Infrastructure.Controllers;

namespace Modules.Timetable.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = Permissions.CanManageSchedule)]
    public class DesignerAutocompleteController : ApiControllerBase
    {
        [HttpGet("idleTeachers")]
        public async Task<ActionResult<List<TeacherDto>>> GetIdleTeachers([FromQuery] GetIdleTeachersQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("idleAudiences")]
        public async Task<ActionResult<List<AudienceDto>>> GetIdleAudiences([FromQuery] GetIdleAudiencesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("idleGroups")]
        public async Task<ActionResult<List<GroupDto>>> GetIdleGroups([FromQuery] GetIdleGroupsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}