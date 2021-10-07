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
using Shared.Core.Extensions;
using Shared.Core.Helpers;
using Shared.Core.Models;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Groups.Queries
{
    public class GroupQueryHandler 
        : IRequestHandler<GetGroupByIdQuery, GroupDto>,
            IRequestHandler<GetGroupsQuery, PagedList<GroupDto>>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;

        public GroupQueryHandler(IScheduleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GroupDto> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(request.Id);
            Guard.RequireEntityNotNull(group);

            return _mapper.Map<GroupDto>(group);
        }

        public async Task<PagedList<GroupDto>> Handle(GetGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = await _dbContext.Groups
                .AsNoTracking()
                .OrderBy(g => g.Number)
                .Paginate(request.Page, request.PageSize)
                .ToListAsync(cancellationToken);
            var totalCount = await _dbContext.Groups.CountAsync(cancellationToken);

            var mappedItems = _mapper.Map<List<GroupDto>>(groups);
            return new PagedList<GroupDto>(mappedItems, request.Page, request.PageSize, totalCount);
        }
    }
}