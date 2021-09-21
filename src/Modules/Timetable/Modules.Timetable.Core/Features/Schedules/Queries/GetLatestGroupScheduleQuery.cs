using System;
using MediatR;
using Modules.Timetable.Core.Constants;
using Shared.Core.Behaviors;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Schedules.Queries
{
    public class GetLatestGroupScheduleQuery : IRequest<GroupScheduleDto>, ICacheableRequest
    {
        public int GroupId { get; init; }
        public string CacheKey => CacheKeys.LatestGroupSchedule(GroupId);
        public TimeSpan ExpirationTime => TimeSpan.FromMinutes(30);
    }
}