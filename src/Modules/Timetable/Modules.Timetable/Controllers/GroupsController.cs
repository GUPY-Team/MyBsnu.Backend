using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modules.Timetable.Core.Features.Groups.Commands;
using Modules.Timetable.Core.Features.Groups.Queries;
using Shared.DTO.Schedule;
using Shared.Infrastructure.Controllers;

namespace Modules.Timetable.Controllers
{
    [ApiVersion("1.0")]
    public class GroupsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<GroupDto>>> GetGroups()
        {
            var result = await Mediator.Send(new GetGroupsQuery());
            return Ok(result);
        }

        [HttpGet("{Id:min(1)}")]
        public async Task<ActionResult<GroupDto>> GetGroup([FromRoute] GetGroupByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<GroupDto>> CreateGroup([FromBody] CreateGroupCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<GroupDto>> UpdateGroup([FromBody] UpdateGroupCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{Id:min(1)}")]
        public async Task<ActionResult> DeleteGroup([FromRoute] DeleteGroupCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
    }
}