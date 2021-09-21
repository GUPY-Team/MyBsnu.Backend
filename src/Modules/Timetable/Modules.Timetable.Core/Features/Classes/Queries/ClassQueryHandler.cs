using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Timetable.Core.Abstractions;
using Modules.Timetable.Core.Entities;
using Shared.Core.Domain;
using Shared.Core.Helpers;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Classes.Queries
{
    public class ClassQueryHandler
        : IRequestHandler<GetClassByIdQuery, ClassDto>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;

        public ClassQueryHandler(IScheduleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ClassDto> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
        {
            var @class = await _dbContext.Classes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            Guard.RequireEntityNotNull(@class);

            return _mapper.Map<ClassDto>(@class);
        }
    }
}