using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Modules.Timetable.Core.Abstractions;
using Modules.Timetable.Core.Entities;
using Shared.Core.Domain;
using Shared.Core.Helpers;
using Shared.DTO.Schedule;
using Shared.Localization;

namespace Modules.Timetable.Core.Features.Schedules.Commands
{
    public class ScheduleCommandHandler
        : IRequestHandler<CreateScheduleCommand, ScheduleDto>,
            IRequestHandler<UpdateScheduleCommand, ScheduleDto>,
            IRequestHandler<DeleteScheduleCommand, Unit>,
            IRequestHandler<CopyScheduleCommand, Unit>,
            IRequestHandler<PublishScheduleCommand, ScheduleDto>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Locale> _localizer;

        public ScheduleCommandHandler(IScheduleDbContext dbContext, IMapper mapper, IStringLocalizer<Locale> localizer)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _localizer = localizer;
        }

        /// <summary>
        /// Create Schedule 
        /// </summary>
        public async Task<ScheduleDto> Handle(CreateScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = _mapper.Map<Schedule>(request);

            schedule.Version = await GenerateScheduleVersion(schedule, cancellationToken);

            await _dbContext.Schedules.AddAsync(schedule, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<ScheduleDto>(schedule);
        }

        /// <summary>
        /// Update Schedule
        /// </summary>
        public async Task<ScheduleDto> Handle(UpdateScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _dbContext.Schedules.FindAsync(request.Id);
            Guard.RequireEntityNotNull(schedule);

            _mapper.Map(request, schedule);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ScheduleDto>(schedule);
        }

        /// <summary>
        /// Delete Schedule
        /// </summary>
        public async Task<Unit> Handle(DeleteScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _dbContext.Schedules.FindAsync(request.Id);
            Guard.RequireEntityNotNull(schedule);

            if (schedule.IsPublished)
            {
                throw new EntityNotValidException(_localizer.GetString("errors.DeletePublishedSchedule"));
            }

            _dbContext.Schedules.Remove(schedule);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        /// <summary>
        /// Copy Schedule
        /// </summary>
        public async Task<Unit> Handle(CopyScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _dbContext.Schedules
                .AsNoTrackingWithIdentityResolution()
                .Where(s => s.Id == request.SourceId)
                .Include(s => s.Classes)
                .FirstOrDefaultAsync(cancellationToken);
            Guard.RequireEntityNotNull(schedule);

            var scheduleVersion = await GenerateScheduleVersion(schedule, cancellationToken);
            var copiedSchedule = new Schedule
            {
                Semester = schedule.Semester,
                Year = schedule.Year,
                Version = scheduleVersion,
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

        /// <summary>
        /// Publish Schedule
        /// </summary>
        public async Task<ScheduleDto> Handle(PublishScheduleCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _dbContext.Schedules.FindAsync(request.Id);
            Guard.RequireEntityNotNull(schedule);

            if (schedule.IsPublished)
            {
                throw new EntityNotValidException(_localizer.GetString("errors.SchedulePublished"));
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

        private async Task<int> GenerateScheduleVersion(Schedule schedule, CancellationToken cancellationToken)
        {
            var latestVersion = await _dbContext.Schedules
                .Where(s => s.Year == schedule.Year && s.Semester == schedule.Semester)
                .OrderByDescending(s => s.Version)
                .Select(s => s.Version)
                .FirstOrDefaultAsync(cancellationToken);

            return latestVersion == 0 ? 1 : ++latestVersion;
        }
    }
}