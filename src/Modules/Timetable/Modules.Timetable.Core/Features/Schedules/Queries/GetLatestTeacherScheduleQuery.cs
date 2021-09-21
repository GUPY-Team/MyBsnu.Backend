using System;
using MediatR;
using Modules.Timetable.Core.Constants;
using Shared.Core.Behaviors;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Schedules.Queries
{
    public class GetLatestTeacherScheduleQuery : IRequest<TeacherScheduleDto>, ICacheableRequest
    {
        public int TeacherId { get; init; }
        public string CacheKey => CacheKeys.LatestTeacherSchedule(TeacherId);
        public TimeSpan ExpirationTime => TimeSpan.FromMinutes(30);
    }
}