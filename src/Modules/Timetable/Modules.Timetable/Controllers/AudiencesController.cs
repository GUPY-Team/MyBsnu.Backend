using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Timetable.Core.Features.Audiences.Commands;
using Modules.Timetable.Core.Features.Audiences.Queries;
using Shared.Core.Constants;
using Shared.Core.Models;
using Shared.DTO.Schedule;
using Shared.Infrastructure.Controllers;

namespace Modules.Timetable.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = Permissions.ScheduleEditor)]
    public class AudiencesController : ApiControllerBase
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<PagedList<AudienceDto>>> GetAudiences([FromQuery] GetAudiencesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{Id:min(1)}")]
        [AllowAnonymous]
        public async Task<ActionResult<AudienceDto>> GetAudienceById([FromRoute] GetAudienceByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AudienceDto>> CreateAudience([FromBody] CreateAudienceCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<AudienceDto>> UpdateAudience([FromBody] UpdateAudienceCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{Id:min(1)}")]
        public async Task<ActionResult<AudienceDto>> DeleteAudience([FromRoute] DeleteAudienceCommand command)
        {
            await  Mediator.Send(command);
            return NoContent();
        }
    }
}