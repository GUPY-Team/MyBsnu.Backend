using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Identity.Core.Features.Users.Commands;
using Shared.Core.Constants;
using Shared.Infrastructure.Controllers;

namespace Modules.Identity.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = Permissions.SuperAdmin)]
    public class UsersController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult> DeleteUser([FromRoute] DeleteUserCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}