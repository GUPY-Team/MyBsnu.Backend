using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Identity.Core.Features.Claims.Queries;
using Shared.Core.Constants;
using Shared.DTO.Identity;
using Shared.Infrastructure.Controllers;

namespace Modules.Identity.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = Permissions.SuperAdmin)]
    public class ClaimsController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ClaimDto>>> GetClaims()
        {
            var result = await Mediator.Send(new GetClaimsQuery());
            return Ok(result);
        }
    }
}