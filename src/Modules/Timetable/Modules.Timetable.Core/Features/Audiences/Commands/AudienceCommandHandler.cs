using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Modules.Timetable.Core.Abstractions;
using Modules.Timetable.Core.Entities;
using Shared.Core.Domain;
using Shared.Core.Helpers;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Audiences.Commands
{
    public class AudienceCommandHandler
        : IRequestHandler<CreateAudienceCommand, AudienceDto>,
            IRequestHandler<UpdateAudienceCommand, AudienceDto>,
            IRequestHandler<DeleteAudienceCommand, Unit>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;

        public AudienceCommandHandler(IScheduleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AudienceDto> Handle(CreateAudienceCommand request, CancellationToken cancellationToken)
        {
            var audience = _mapper.Map<Audience>(request);

            await _dbContext.Audiences.AddAsync(audience, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AudienceDto>(audience);
        }

        public async Task<AudienceDto> Handle(UpdateAudienceCommand request, CancellationToken cancellationToken)
        {
            var audience = await _dbContext.Audiences.FindAsync(request.Id);
            Guard.RequireEntityNotNull(audience);

            _mapper.Map(request, audience);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<AudienceDto>(audience);
        }

        public async Task<Unit> Handle(DeleteAudienceCommand request, CancellationToken cancellationToken)
        {
            var audience = await _dbContext.Audiences.FindAsync(request.Id);
            Guard.RequireEntityNotNull(audience);

            _dbContext.Audiences.Remove(audience);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}