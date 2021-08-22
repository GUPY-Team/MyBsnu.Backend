using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modules.Timetable.Core.Features.Audiences.Commands;
using Modules.Timetable.Core.Features.Audiences.Queries;
using Shared.DTO.Schedule;
using Shared.Infrastructure.Controllers;

namespace Modules.Timetable.Controllers
{
    [ApiVersion("1.0")]
    public class AudiencesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<AudienceDto>>> GetAudiences()
        {
            var result = await Mediator.Send(new GetAudiencesQuery());
            return Ok(result);
        }

        [HttpGet("{Id:min(1)}")]
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