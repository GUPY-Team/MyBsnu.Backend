using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Modules.Timetable.Core.Abstractions;
using Modules.Timetable.Core.Entities;
using Shared.Core.Domain;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Teachers.Commands
{
    public class TeacherCommandHandler
        : IRequestHandler<CreateTeacherCommand, TeacherDto>,
            IRequestHandler<UpdateTeacherCommand, TeacherDto>,
            IRequestHandler<DeleteTeacherCommand, Unit>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;

        public TeacherCommandHandler(IScheduleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TeacherDto> Handle(CreateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = _mapper.Map<Teacher>(request);

            await _dbContext.Teachers.AddAsync(teacher, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<TeacherDto> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _dbContext.Teachers.FindAsync(request.Id);
            if (teacher == null)
            {
                throw new EntityNotFoundException(nameof(Teacher));
            }

            _mapper.Map(request, teacher);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<Unit> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _dbContext.Teachers.FindAsync(request.Id);
            if (teacher == null)
            {
                throw new EntityNotFoundException(nameof(Teacher));
            }

            _dbContext.Teachers.Remove(teacher);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}