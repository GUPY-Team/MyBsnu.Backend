using System.Collections.Generic;
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

namespace Modules.Timetable.Core.Features.Classes.Commands
{
    public class ClassCommandHandler
        : IRequestHandler<CreateClassCommand, ClassDto>,
            IRequestHandler<UpdateClassCommand, ClassDto>,
            IRequestHandler<DeleteClassCommand, Unit>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Locale> _localizer;

        public ClassCommandHandler(IScheduleDbContext dbContext, IMapper mapper, IStringLocalizer<Locale> localizer)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<ClassDto> Handle(CreateClassCommand request, CancellationToken cancellationToken)
        {
            var schedule = await _dbContext.Schedules.FindAsync(request.ScheduleId);
            Guard.RequireEntityNotNull(schedule);

            var @class = _mapper.Map<Class>(request);

            await UpdateRelatedEntities(@class, request.Audiences, request.Teachers, request.Groups, cancellationToken);
            await ValidateClass(@class, cancellationToken);

            await _dbContext.Classes.AddAsync(@class, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ClassDto>(@class);
        }

        public async Task<ClassDto> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
        {
            var @class = await _dbContext.Classes.FindAsync(request.Id);
            Guard.RequireEntityNotNull(@class);

            _mapper.Map(request, @class);

            await UpdateRelatedEntities(@class, request.Audiences, request.Teachers, request.Groups, cancellationToken);
            await ValidateClass(@class, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ClassDto>(@class);
        }

        public async Task<Unit> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
        {
            var @class = await _dbContext.Classes
                .IgnoreAutoIncludes()
                .SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            Guard.RequireEntityNotNull(@class);

            _dbContext.Classes.Remove(@class);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task UpdateRelatedEntities(
            Class @class,
            IEnumerable<int> audiences,
            IEnumerable<int> teachers,
            IEnumerable<int> groups,
            CancellationToken cancellationToken)
        {
            @class.Audiences.Clear();
            @class.Teachers.Clear();
            @class.Groups.Clear();

            @class.Audiences = await _dbContext.Audiences.Where(a => audiences.Contains(a.Id)).ToListAsync(cancellationToken);
            @class.Teachers = await _dbContext.Teachers.Where(t => teachers.Contains(t.Id)).ToListAsync(cancellationToken);
            @class.Groups = await _dbContext.Groups.Where(g => groups.Contains(g.Id)).ToListAsync(cancellationToken);
        }

        private async Task ValidateClass(Class @class, CancellationToken cancellationToken)
        {
            var overlaps = await GetOverlappingClassesWith(@class).ToListAsync(cancellationToken);

            foreach (var overlap in overlaps)
            {
                var teachers = overlap.Teachers.Intersect(@class.Teachers).Select(t => t.ShortName).ToList();
                if (teachers.Any())
                {
                    var message = _localizer.GetString("errors.TeachersBusy", string.Join(',', teachers));
                    throw new EntityNotValidException(message);
                }

                var audiences = overlap.Audiences.Intersect(@class.Audiences).Select(a => a.FullNumber).ToList();
                if (audiences.Any())
                {
                    var message = _localizer.GetString("errors.AudiencesOccupied", string.Join(',', audiences));
                    throw new EntityNotValidException(message);
                }

                var groups = overlap.Groups.Intersect(@class.Groups).Select(g => g.Number).ToList();
                if (groups.Any())
                {
                    var message = _localizer.GetString("errors.GroupsBusy", string.Join(',', groups));
                    throw new EntityNotValidException(message);
                }
            }
        }

        private IQueryable<Class> GetOverlappingClassesWith(Class @class)
        {
            var baseQuery = _dbContext.Classes
                .Where(c => c.Id != @class.Id)
                .Where(c => c.ScheduleId == @class.ScheduleId)
                .Where(c => c.StartTime == @class.StartTime)
                .Where(c => c.WeekDay == @class.WeekDay)
                .Where(c =>
                    c.Teachers.Any(t => @class.Teachers.Select(x => x.Id).Contains(t.Id)) ||
                    c.Audiences.Any(t => @class.Audiences.Select(x => x.Id).Contains(t.Id)) ||
                    c.Groups.Any(t => @class.Groups.Select(x => x.Id).Contains(t.Id))
                );

            if (@class.WeekType != null)
            {
                baseQuery = baseQuery.Where(c => c.WeekType == null || c.WeekType == @class.WeekType);
            }

            return baseQuery;
        }
    }
}