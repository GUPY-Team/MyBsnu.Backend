﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Modules.Timetable.Core.Features.Schedules.Queries;
using Shared.DTO.Schedule;
using Shared.Infrastructure.Controllers;

namespace Modules.Timetable.Controllers
{
    [ApiVersion("1.0")]
    public class ScheduleController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ScheduleDto>>> GetScheduleList()
        {
            var result = await Mediator.Send(new GetScheduleListQuery());
            return Ok(result);
        }

        [HttpGet("{Id:min(1)}")]
        public async Task<ActionResult<ScheduleDto>> GetSchedule([FromRoute] GetScheduleByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("latest")]
        public async Task<ActionResult<GroupScheduleDto>> GetLatestSchedule([FromQuery] GetLatestScheduleQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{ScheduleId:min(1)}/groups/{GroupId:min(1)}")]
        public async Task<ActionResult<GroupScheduleDto>> GetGroupSchedule([FromRoute] GetGroupScheduleQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}