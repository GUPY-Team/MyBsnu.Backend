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

namespace Modules.Timetable.Core.Features.Schedules.Commands
{
    public class ScheduleCommandHandler
        : IRequestHandler<UpdateScheduleCommand, ScheduleDto>,
            IRequestHandler<CopyScheduleCommand, Unit>,
            IRequestHandler<DeleteScheduleCommand, Unit>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;

        public ScheduleCommandHandler(IScheduleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ScheduleDto> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _dbContext.Schedules.FindAsync(request.Id);
            if (schedule == null)
            {
                throw new EntityNotFoundException(nameof(Schedule));
            }

            _mapper.Map(request, schedule);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ScheduleDto>(schedule);
        }

        public async Task<Unit> Handle(CopyScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _dbContext.Schedules
                .AsNoTrackingWithIdentityResolution()
                .Where(s => s.Id == request.SourceId)
                .Include(s => s.Classes)
                .FirstOrDefaultAsync(cancellationToken);

            if (schedule == null)
            {
                throw new EntityNotFoundException(nameof(Schedule));
            }

            schedule.Id = default;
            schedule.IsPublished = false;
            schedule.Version++;

            foreach (var @class in schedule.Classes)
            {
                @class.Id = default;
            }

            _dbContext.Schedules.Update(schedule);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        public async Task<Unit> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _dbContext.Schedules.FindAsync(request.Id);
            if (schedule == null)
            {
                throw new EntityNotFoundException(nameof(Schedule));
            }

            _dbContext.Schedules.Remove(schedule);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}