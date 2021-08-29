using System.Collections.Generic;
using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Schedules.Queries
{
    public class GetScheduleListQuery : IRequest<List<ScheduleDto>>
    {
    }
}