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

namespace Modules.Timetable.Core.Features.Classes.Commands
{
    public class ClassCommandHandler
        : IRequestHandler<CreateClassCommand, ClassDto>,
            IRequestHandler<UpdateClassCommand, ClassDto>,
            IRequestHandler<DeleteClassCommand, Unit>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;

        public ClassCommandHandler(IScheduleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ClassDto> Handle(CreateClassCommand request, CancellationToken cancellationToken)
        {
            // TODO: Add related entities check

            var @class = _mapper.Map<Class>(request);

            await UpdateRelatedEntities(@class, request.Audiences, request.Teachers, request.Groups, cancellationToken);

            await _dbContext.Classes.AddAsync(@class, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ClassDto>(@class);
        }

        public async Task<ClassDto> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
        {
            var @class = await _dbContext.Classes.FindAsync(request.Id);
            if (@class == null)
            {
                throw new EntityNotFoundException(nameof(Class));
            }

            _mapper.Map(request, @class);

            await UpdateRelatedEntities(@class, request.Audiences, request.Teachers, request.Groups, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ClassDto>(@class);
        }

        public async Task<Unit> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
        {
            var @class = await _dbContext.Classes
                .IgnoreAutoIncludes()
                .SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (@class == null)
            {
                throw new EntityNotFoundException(nameof(Class));
            }

            _dbContext.Classes.Remove(@class);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task UpdateRelatedEntities(
            Class @class,
            IEnumerable<int> audiences,
            IEnumerable<int> teachers,
            IEnumerable<int> groups,
            CancellationToken cancellationToken = default)
        {
            @class.Audiences.Clear();
            @class.Teachers.Clear();
            @class.Groups.Clear();

            @class.Audiences = await _dbContext.Audiences.Where(a => audiences.Contains(a.Id)).ToListAsync(cancellationToken);
            @class.Teachers = await _dbContext.Teachers.Where(t => teachers.Contains(t.Id)).ToListAsync(cancellationToken);
            @class.Groups = await _dbContext.Groups.Where(g => groups.Contains(g.Id)).ToListAsync(cancellationToken);
        }
    }
}