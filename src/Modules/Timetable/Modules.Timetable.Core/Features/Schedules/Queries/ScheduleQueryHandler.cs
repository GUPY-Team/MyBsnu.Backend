using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Timetable.Core.Abstractions;
using Modules.Timetable.Core.Entities;
using Modules.Timetable.Core.Specifications;
using Shared.Core.Extensions;
using Shared.Core.Helpers;
using Shared.Core.Models;
using Shared.DTO;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Schedules.Queries
{
    public class ScheduleCommandHandler
        : IRequestHandler<GetLatestGroupScheduleQuery, GroupScheduleDto>,
            IRequestHandler<GetScheduleListQuery, PagedList<ScheduleDto>>,
            IRequestHandler<GetScheduleByIdQuery, ScheduleDto>,
            IRequestHandler<GetGroupScheduleQuery, GroupScheduleDto>,
            IRequestHandler<GetLatestTeacherScheduleQuery, TeacherScheduleDto>,
            IRequestHandler<GetTeacherScheduleQuery, TeacherScheduleDto>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;

        public ScheduleCommandHandler(IScheduleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        /// <summary>
        /// Get Latest Group Schedule
        /// </summary>
        public async Task<GroupScheduleDto> Handle(GetLatestGroupScheduleQuery request, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(request.GroupId);
            Guard.RequireEntityNotNull(group);

            var schedule = await _dbContext.Schedules
                .AsNoTracking()
                .WithSpecification(new LatestGroupScheduleSpecification(request.GroupId))
                .FirstOrDefaultAsync(cancellationToken);
            Guard.RequireEntityNotNull(schedule);

            return new GroupScheduleDto
            {
                Year = schedule.Year,
                Semester = schedule.Semester.Name,
                Classes = MapToScheduleClasses(schedule.Classes)
            };
        }

        /// <summary>
        /// Get Schedule List 
        /// </summary>
        public async Task<PagedList<ScheduleDto>> Handle(GetScheduleListQuery request, CancellationToken cancellationToken)
        {
            var scheduleList = await _dbContext.Schedules
                .AsNoTracking()
                .WithSpecification(new ScheduleListSpecification())
                .Paginate(request.Page, request.PageSize)
                .ToListAsync(cancellationToken);
            var totalItems = await _dbContext.Schedules.CountAsync(cancellationToken);

            var mappedItems = _mapper.Map<List<ScheduleDto>>(scheduleList);
            return new PagedList<ScheduleDto>(mappedItems, request.Page, request.PageSize, totalItems);
        }

        /// <summary>
        /// Get Schedule By Id
        /// </summary>
        public async Task<ScheduleDto> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            var schedule = await _dbContext.Schedules.FindAsync(request.Id);
            Guard.RequireEntityNotNull(schedule);

            return _mapper.Map<ScheduleDto>(schedule);
        }

        /// <summary>
        /// Get Group Schedule
        /// </summary>
        public async Task<GroupScheduleDto> Handle(GetGroupScheduleQuery request, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(request.GroupId);
            Guard.RequireEntityNotNull(group);

            var schedule = await _dbContext.Schedules
                .AsNoTracking()
                .WithSpecification(new GroupScheduleSpecification(request.GroupId))
                .Where(s => s.Id == request.ScheduleId)
                .FirstOrDefaultAsync(cancellationToken);
            Guard.RequireEntityNotNull(schedule);

            return new GroupScheduleDto
            {
                Year = schedule.Year,
                Semester = schedule.Semester.Name,
                Classes = MapToScheduleClasses(schedule.Classes)
            };
        }

        /// <summary>
        /// Get Latest Teacher Schedule
        /// </summary>
        public async Task<TeacherScheduleDto> Handle(GetLatestTeacherScheduleQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _dbContext.Teachers.FindAsync(request.TeacherId);
            Guard.RequireEntityNotNull(teacher);

            var schedule = await _dbContext.Schedules
                .AsNoTracking()
                .WithSpecification(new LatestTeacherScheduleSpecification(request.TeacherId))
                .FirstOrDefaultAsync(cancellationToken);
            Guard.RequireEntityNotNull(schedule);

            return new TeacherScheduleDto
            {
                Classes = MapToScheduleClasses(schedule.Classes)
            };
        }

        /// <summary>
        /// Get Teacher Schedule
        /// </summary>
        public async Task<TeacherScheduleDto> Handle(GetTeacherScheduleQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _dbContext.Teachers
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == request.TeacherId, cancellationToken);
            Guard.RequireEntityNotNull(teacher);

            var schedule = await _dbContext.Schedules
                .AsNoTracking()
                .WithSpecification(new TeacherScheduleSpecification(request.TeacherId))
                .Where(s => s.Id == request.ScheduleId)
                .FirstOrDefaultAsync(cancellationToken);
            Guard.RequireEntityNotNull(schedule);

            return new TeacherScheduleDto
            {
                Classes = MapToScheduleClasses(schedule.Classes)
            };
        }

        private Dictionary<string, ClassDto[]> MapToScheduleClasses(IEnumerable<Class> classes)
        {
            return classes.GroupBy(c => c.WeekDay)
                .OrderBy(g => g.Key)
                .ToDictionary(
                    g => g.Key.Name,
                    g => _mapper.Map<ClassDto[]>(g.Select(c => c).OrderBy(c => c.StartTime))
                );
        }
    }
}