using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Modules.Timetable.Core.Abstractions;
using Modules.Timetable.Core.Entities;
using Shared.Core.Domain;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Courses.Commands
{
    public class CourseCommandHandler
        : IRequestHandler<CreateCourseCommand, CourseDto>,
            IRequestHandler<UpdateCourseCommand, CourseDto>,
            IRequestHandler<DeleteCourseCommand, Unit>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;

        public CourseCommandHandler(IScheduleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CourseDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = _mapper.Map<Course>(request);

            await _dbContext.Courses.AddAsync(course, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CourseDto>(course);
        }

        public async Task<CourseDto> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _dbContext.Courses.FindAsync(request.Id);
            if (course == null)
            {
                throw new EntityNotFoundException("Course not found");
            }

            _mapper.Map(request, course);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CourseDto>(course);
        }

        public async Task<Unit> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _dbContext.Courses.FindAsync(request.Id);
            if (course == null)
            {
                throw new EntityNotFoundException("Course not found");
            }

            _dbContext.Courses.Remove(course);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}