using System.Collections.Generic;
using MediatR;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.ScheduleDesigner.Queries
{
    public class GetIdleAudiencesQuery : IRequest<List<AudienceDto>>, IIdleEntityQuery
    {
        public int ScheduleId { get; init; }
        public string WeekDay { get; init; }
        public string WeekType { get; init; }
        public string StartTime { get; init; }
    }
}