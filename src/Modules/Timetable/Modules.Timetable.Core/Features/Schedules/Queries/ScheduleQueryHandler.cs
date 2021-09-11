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
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Schedules.Queries
{
    public class ScheduleCommandHandler
        : IRequestHandler<GetLatestGroupScheduleQuery, GroupScheduleDto>,
            IRequestHandler<GetScheduleListQuery, List<ScheduleDto>>,
            IRequestHandler<GetScheduleByIdQuery, ScheduleDto>,
            IRequestHandler<GetGroupScheduleQuery, GroupScheduleDto>,
            IRequestHandler<GetLatestTeacherScheduleQuery, TeacherScheduleDto>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;

        public ScheduleCommandHandler(IScheduleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GroupScheduleDto> Handle(GetLatestGroupScheduleQuery request, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups
                .AsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == request.GroupId, cancellationToken);
            if (group == null)
            {
                throw new EntityNotFoundException(nameof(Group));
            }

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

        public async Task<List<ScheduleDto>> Handle(GetScheduleListQuery request, CancellationToken cancellationToken)
        {
            var scheduleList = await _dbContext.Schedules
                .AsNoTracking()
                .OrderByDescending(s => s.Year)
                .ThenByDescending(s => s.Semester)
                .ThenByDescending(s => s.Version)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<ScheduleDto>>(scheduleList);
        }

        public async Task<ScheduleDto> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            var schedule = await _dbContext.Schedules
                .AsNoTracking()
                .SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
            if (schedule == null)
            {
                throw new EntityNotFoundException(nameof(Schedule));
            }

            return _mapper.Map<ScheduleDto>(schedule);
        }

        public async Task<GroupScheduleDto> Handle(GetGroupScheduleQuery request, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups
                .AsNoTracking()
                .SingleOrDefaultAsync(g => g.Id == request.GroupId, cancellationToken);
            if (group == null)
            {
                throw new EntityNotFoundException(nameof(Group));
            }

            var schedule = await GetGroupSchedule(request.GroupId)
                .Where(s => s.Id == request.ScheduleId)
                .FirstOrDefaultAsync(cancellationToken);

            if (schedule == null)
            {
                throw new EntityNotFoundException(nameof(Schedule));
            }

            return new GroupScheduleDto
            {
                Year = schedule.Year,
                Semester = schedule.Semester.Name,
                Classes = MapToScheduleClasses(schedule.Classes)
            };
        }

        public async Task<TeacherScheduleDto> Handle(GetLatestTeacherScheduleQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _dbContext.Teachers
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == request.TeacherId, cancellationToken);
            if (teacher == null)
            {
                throw new EntityNotFoundException(nameof(Teacher));
            }

            var schedule = await _dbContext.Schedules
                .AsNoTracking()
                .Include(s => s.Classes
                    .Where(c => c.Teachers
                        .Select(t => t.Id)
                        .Contains(request.TeacherId)
                    )
                ).Where(s => s.IsPublished)
                .FirstOrDefaultAsync(cancellationToken);

            if (schedule == null)
            {
                throw new EntityNotFoundException(nameof(Schedule));
            }

            return new TeacherScheduleDto
            {
                Classes = MapToScheduleClasses(schedule.Classes)
            };
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