using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Modules.Timetable.Core.Abstractions;
using Modules.Timetable.Core.Entities;
using Shared.Core.Domain;
using Shared.Core.Helpers;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Groups.Commands
{
    public class GroupCommandHandler 
        : IRequestHandler<CreateGroupCommand, GroupDto>,
            IRequestHandler<UpdateGroupCommand, GroupDto>,
            IRequestHandler<DeleteGroupCommand, Unit>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;

        public GroupCommandHandler(IScheduleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<GroupDto> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = _mapper.Map<Group>(request);

            await _dbContext.Groups.AddAsync(group, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GroupDto>(group);
        }

        public async Task<GroupDto> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(request.Id);
            Guard.RequireEntityNotNull(group);

            _mapper.Map(request, group);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<GroupDto>(group);
        }

        public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(request.Id);
            Guard.RequireEntityNotNull(group);

            _dbContext.Groups.Remove(group);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}