using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Identity.Core.Abstractions;
using Modules.Identity.Core.Features.Auth.Commands;
using Shared.DTO.Identity;
using Shared.Infrastructure.Controllers;

namespace Modules.Identity.Controllers
{
    [ApiVersion("1.0")]
    [AllowAnonymous]
    public class AuthController : ApiControllerBase
    {
        [HttpPost("signup")]
        public async Task<ActionResult> Signup([FromBody] SignupUserCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPost("signin")]
        public async Task<ActionResult> Signin([FromBody] SigninUserCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}