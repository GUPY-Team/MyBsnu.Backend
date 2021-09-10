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
            IRequestHandler<DeleteScheduleCommand, Unit>,
            IRequestHandler<PublishScheduleCommand, ScheduleDto>
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

            var copiedSchedule = new Schedule
            {
                Semester = schedule.Semester,
                Year = schedule.Year,
                Version = ++schedule.Version,
                Classes = schedule.Classes.Select(c =>
                {
                    c.Id = default;
                    return c;
                }).ToList()
            };

            _dbContext.Teachers.AttachRange(schedule.Classes.SelectMany(c => c.Teachers));
            _dbContext.Audiences.AttachRange(schedule.Classes.SelectMany(c => c.Audiences));
            _dbContext.Groups.AttachRange(schedule.Classes.SelectMany(c => c.Groups));
            
            await _dbContext.Schedules.AddAsync(copiedSchedule, cancellationToken);
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

            if (schedule.IsPublished)
            {
                throw new EntityNotValidException("Can't delete published schedule");
            }

            _dbContext.Schedules.Remove(schedule);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        public async Task<ScheduleDto> Handle(PublishScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _dbContext.Schedules.FindAsync(request.Id);
            if (schedule == null)
            {
                throw new EntityNotFoundException(nameof(Schedule));
            }

            if (schedule.IsPublished)
            {
                throw new EntityNotValidException("Schedule is already published");
            }

            var previousPublishedSchedule = await _dbContext.Schedules.SingleOrDefaultAsync(s => s.IsPublished, cancellationToken);
            if (previousPublishedSchedule != null)
            {
                previousPublishedSchedule.IsPublished = false;
            }

            schedule.IsPublished = true;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ScheduleDto>(schedule);
        }
    }
}