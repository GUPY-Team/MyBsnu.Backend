using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Timetable.Core.Abstractions;
using Modules.Timetable.Core.Entities;
using Shared.Core.Domain;
using Shared.Core.Extensions;
using Shared.Core.Helpers;
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

            var schedule = await GetGroupSchedule(request.GroupId)
                .Where(s => s.IsPublished)
                .FirstOrDefaultAsync(cancellationToken);

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
                .OrderByDescending(s => s.Year)
                .ThenByDescending(s => s.Semester)
                .ThenByDescending(s => s.Version)
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

            var schedule = await GetGroupSchedule(request.GroupId)
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

            var schedule = await GetTeacherSchedule(teacher.Id)
                .Where(s => s.IsPublished)
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

            var schedule = await GetTeacherSchedule(teacher.Id)
                .Where(s => s.Id == request.ScheduleId)
                .FirstOrDefaultAsync(cancellationToken);
            Guard.RequireEntityNotNull(schedule);

            return new TeacherScheduleDto
            {
                Classes = MapToScheduleClasses(schedule.Classes)
            };
        }

        private IQueryable<Schedule> GetTeacherSchedule(int teacherId)
        {
            return _dbContext.Schedules
                .AsNoTracking()
                .Include(s => s.Classes
                    .Where(c => c.Teachers
                        .Select(t => t.Id)
                        .Contains(teacherId)
                    )
                );
        }

        private IQueryable<Schedule> GetGroupSchedule(int groupId)
        {
            return _dbContext.Schedules
                .AsNoTracking()
                .Include(s => s.Classes
                    .Where(c => c.Groups
                        .Any(cg => cg.Id == groupId)
                    )
                );
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