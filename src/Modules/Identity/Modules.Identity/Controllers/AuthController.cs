using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modules.Identity.Core.Abstractions;
using Shared.DTO.Identity;
using Shared.Infrastructure.Controllers;

namespace Modules.Identity.Controllers
{
    [ApiVersion("1.0")]
    public class AuthController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> Signup([FromBody] SignupUserRequest request)
        {
            await _userService.SignupUser(request);
            return Ok();
        }

        [HttpPost("signin")]
        public async Task<ActionResult> Signin([FromBody] SigninUserRequest request)
        {
            var result = await _userService.SigninUser(request);
            return Ok(result);
        }
    }
}