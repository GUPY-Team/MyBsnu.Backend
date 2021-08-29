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

namespace Modules.Timetable.Core.Features.Audiences.Queries
{
    public class AudienceQueryHandler
        : IRequestHandler<GetAudienceByIdQuery, AudienceDto>,
            IRequestHandler<GetAudiencesQuery, List<AudienceDto>>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;

        public AudienceQueryHandler(IScheduleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AudienceDto> Handle(GetAudienceByIdQuery request, CancellationToken cancellationToken)
        {
            var audience = await _dbContext.Audiences.FindAsync(request.Id);
            if (audience == null)
            {
                throw new EntityNotFoundException(nameof(Audience));
            }

            return _mapper.Map<AudienceDto>(audience);
        }

        public async Task<List<AudienceDto>> Handle(GetAudiencesQuery request, CancellationToken cancellationToken)
        {
            var audiences = await _dbContext.Audiences
                .OrderBy(a => a.Corps)
                .ThenBy(a => a.Floor)
                .ThenBy(a => a.Room)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<AudienceDto>>(audiences);
        }
    }
}