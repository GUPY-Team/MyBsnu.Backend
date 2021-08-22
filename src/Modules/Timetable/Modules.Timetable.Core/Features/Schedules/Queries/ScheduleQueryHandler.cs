using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Timetable.Core.Abstractions;
using Shared.Core.Domain;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Schedules.Queries
{
    public class ScheduleCommandHandler
        : IRequestHandler<GetLatestScheduleQuery, GroupScheduleDto>,
            IRequestHandler<GetSchedulesQuery, List<ScheduleDto>>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;

        public ScheduleCommandHandler(IScheduleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GroupScheduleDto> Handle(GetLatestScheduleQuery request, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(request.GroupId);
            if (group == null)
            {
                throw new EntityNotFoundException("Group doesn't exist");
            }

            var schedule = await _dbContext.Schedules
                .AsNoTracking()
                .OrderByDescending(t => t.Year)
                .ThenByDescending(t => t.HalfYear)
                .ThenByDescending(t => t.Version)
                .Where(t => t.IsPublished)
                .Include(t => t.Classes
                    .Where(c => c.Groups
                        .Any(cg => cg.Id == request.GroupId)
                    )
                )
                .FirstOrDefaultAsync(cancellationToken);

            var classes = schedule.Classes
                .GroupBy(c => c.WeekDay)
                .OrderBy(g => g.Key)
                .ToDictionary(
                    g => g.Key.Name,
                    g => _mapper.Map<ClassDto[]>(g.Select(c => c).OrderBy(c => c.StartTime))
                );

            return new GroupScheduleDto
            {
                GroupNumber = group.Number,
                Version = schedule.Version,
                HalfYear = schedule.HalfYear.Name,
                Year = schedule.Year,
                Classes = classes
            };
        }

        public async Task<List<ScheduleDto>> Handle(GetSchedulesQuery request, CancellationToken cancellationToken)
        {
            var schedules = await _dbContext.Schedules
                .AsNoTracking()
                .ProjectTo<ScheduleDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return schedules;
        }
    }
}