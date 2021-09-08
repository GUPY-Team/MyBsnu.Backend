using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            IRequestHandler<GetGroupScheduleQuery, GroupScheduleDto>
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
            var group = await _dbContext.Groups.FindAsync(request.GroupId);
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
                Classes = MapToClasses(schedule.Classes)
            };
        }

        public async Task<List<ScheduleDto>> Handle(GetScheduleListQuery request, CancellationToken cancellationToken)
        {
            var scheduleList = await _dbContext.Schedules
                .OrderByDescending(s => s.Year)
                .ThenByDescending(s => s.Semester)
                .ThenByDescending(s => s.Version)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<ScheduleDto>>(scheduleList);
        }

        public async Task<ScheduleDto> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
        {
            var schedule = await _dbContext.Schedules.SingleOrDefaultAsync(s => s.Id == request.Id, cancellationToken);
            if (schedule == null)
            {
                throw new EntityNotFoundException(nameof(Schedule));
            }

            return _mapper.Map<ScheduleDto>(schedule);
        }

        public async Task<GroupScheduleDto> Handle(GetGroupScheduleQuery request, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(request.GroupId);
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
                Classes = MapToClasses(schedule.Classes)
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

        private Dictionary<string, ClassDto[]> MapToClasses(IEnumerable<Class> classes)
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