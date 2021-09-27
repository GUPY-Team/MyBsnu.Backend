﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Modules.Identity.Core.Features.Users.Commands;
using Modules.Identity.Core.Features.Users.Queries;
using Shared.Core.Constants;
using Shared.DTO.Identity;
using Shared.Infrastructure.Controllers;

namespace Modules.Identity.Controllers
{
    [ApiVersion("1.0")]
    [Authorize(Policy = Permissions.SuperAdmin)]
    public class UsersController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<AppUserListDto>>> GetUsers()
        {
            var result = await Mediator.Send(new GetUsersQuery());
            return Ok(result);
        }

        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<AppUserDto>> GetUser([FromRoute] GetUserByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

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