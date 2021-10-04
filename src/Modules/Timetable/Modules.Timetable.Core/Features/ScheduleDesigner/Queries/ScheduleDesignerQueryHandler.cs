using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Timetable.Core.Abstractions;
using Modules.Timetable.Core.Enums;
using Modules.Timetable.Core.Specifications;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.ScheduleDesigner.Queries
{
    public class ScheduleDesignerQueryHandler
        : IRequestHandler<GetIdleTeachersQuery, List<TeacherDto>>,
            IRequestHandler<GetIdleAudiencesQuery, List<AudienceDto>>,
            IRequestHandler<GetIdleGroupsQuery, List<GroupDto>>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;

        public ScheduleDesignerQueryHandler(IScheduleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<TeacherDto>> Handle(GetIdleTeachersQuery request, CancellationToken cancellationToken)
        {
            var startTime = TimeSpan.Parse(request.StartTime);
            var weekDay = _mapper.Map<WeekDay>(request.WeekDay);
            var weekType = _mapper.Map<WeekType>(request.WeekType);

            var busyTeachers = _dbContext.Classes
                .WithSpecification(new ScheduleClassesAtPeriodSpecification(request.ScheduleId, startTime, weekDay, weekType))
                .SelectMany(c => c.Teachers);

            var idleTeachers = await _dbContext.Teachers
                .AsNoTracking()
                .Where(t => !busyTeachers.Contains(t))
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<TeacherDto>>(idleTeachers);
        }

        public async Task<List<AudienceDto>> Handle(GetIdleAudiencesQuery request, CancellationToken cancellationToken)
        {
            var startTime = TimeSpan.Parse(request.StartTime);
            var weekDay = _mapper.Map<WeekDay>(request.WeekDay);
            var weekType = _mapper.Map<WeekType>(request.WeekType);

            var busyAudiences = _dbContext.Classes
                .WithSpecification(new ScheduleClassesAtPeriodSpecification(request.ScheduleId, startTime, weekDay, weekType))
                .SelectMany(c => c.Audiences);

            var idleAudiences = await _dbContext.Audiences
                .AsNoTracking()
                .Where(a => !busyAudiences.Contains(a))
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<AudienceDto>>(idleAudiences);
        }

        public async Task<List<GroupDto>> Handle(GetIdleGroupsQuery request, CancellationToken cancellationToken)
        {
            var startTime = TimeSpan.Parse(request.StartTime);
            var weekDay = _mapper.Map<WeekDay>(request.WeekDay);
            var weekType = _mapper.Map<WeekType>(request.WeekType);

            var busyGroups = _dbContext.Classes
                .WithSpecification(new ScheduleClassesAtPeriodSpecification(request.ScheduleId, startTime, weekDay, weekType))
                .SelectMany(c => c.Groups);

            var idleGroups = await _dbContext.Groups
                .AsNoTracking()
                .Where(g => !busyGroups.Contains(g))
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<GroupDto>>(idleGroups);
        }
    }
}