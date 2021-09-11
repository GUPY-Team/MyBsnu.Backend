using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modules.Timetable.Core.Features.Schedules.Queries;
using Shared.DTO.Schedule;
using Shared.Infrastructure.Controllers;

namespace Modules.Timetable.Controllers
{
    [ApiVersion("1.0")]
    public class TeacherScheduleController : ApiControllerBase
    {
        [HttpGet("latest")]
        public async Task<ActionResult<TeacherScheduleDto>> GetLatestTeacherSchedule([FromQuery] GetLatestTeacherScheduleQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}