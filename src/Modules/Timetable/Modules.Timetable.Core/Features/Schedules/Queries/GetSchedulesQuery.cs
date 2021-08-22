using System.Collections.Generic;
using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Schedules.Queries
{
    public class GetSchedulesQuery : IRequest<List<ScheduleDto>>
    {
    }
}