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
    public class UserClaimsController : ApiControllerBase
    {
        [HttpPut]
        public async Task<ActionResult> UpdateClaims([FromBody] UpdateUserClaimsCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}