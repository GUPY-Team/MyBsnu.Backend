using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
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

            await _dbContext.Classes.AddAsync(@class, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ClassDto>(@class);
        }

        public async Task<ClassDto> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
        {
            var @class = await _dbContext.Classes.FindAsync(request.Id);
            if (@class == null)
            {
                throw new EntityNotFoundException("Class not found");
            }

            _mapper.Map(request, @class);
            
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ClassDto>(@class);
        }

        public async Task<Unit> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
        {
            var @class = await _dbContext.Classes.FindAsync(request.Id);
            if (@class == null)
            {
                throw new EntityNotFoundException("Class not found");
            }

            _dbContext.Classes.Remove(@class);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}