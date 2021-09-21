using System.Collections.Generic;
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

namespace Modules.Timetable.Core.Features.Courses.Queries
{
    public class CourseQueryHandler 
        : IRequestHandler<GetCourseByIdQuery, CourseDto>,
            IRequestHandler<GetCoursesQuery, List<CourseDto>>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;

        public CourseQueryHandler(IScheduleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CourseDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await _dbContext.Courses.FindAsync(request.Id);
            Guard.RequireEntityNotNull(course);

            return _mapper.Map<CourseDto>(course);
        }

        public async Task<List<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _dbContext.Courses.AsNoTracking().ToListAsync(cancellationToken);
            return _mapper.Map<List<CourseDto>>(courses);
        }
    }
}