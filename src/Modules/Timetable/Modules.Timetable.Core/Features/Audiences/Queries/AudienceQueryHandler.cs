using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Timetable.Core.Abstractions;
using Shared.Core.Extensions;
using Shared.Core.Helpers;
using Shared.Core.Models;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Audiences.Queries
{
    public class AudienceQueryHandler
        : IRequestHandler<GetAudienceByIdQuery, AudienceDto>,
            IRequestHandler<GetAudiencesQuery, PagedList<AudienceDto>>
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
            Guard.RequireEntityNotNull(audience);

            return _mapper.Map<AudienceDto>(audience);
        }

        public async Task<PagedList<AudienceDto>> Handle(GetAudiencesQuery request, CancellationToken cancellationToken)
        {
            var audiences = await _dbContext.Audiences
                .AsNoTracking()
                .OrderBy(a => a.Corps)
                .ThenBy(a => a.Floor)
                .ThenBy(a => a.Room)
                .Paginate(request.Page, request.PageSize)
                .ToListAsync(cancellationToken);
            var totalCount = await _dbContext.Audiences.CountAsync(cancellationToken);
            
            var mappedItems = _mapper.Map<List<AudienceDto>>(audiences);
            return new PagedList<AudienceDto>(mappedItems, request.Page, request.PageSize, totalCount);
        }
    }
}