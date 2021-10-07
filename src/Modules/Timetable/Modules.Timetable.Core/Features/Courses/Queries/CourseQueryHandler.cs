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

namespace Modules.Timetable.Core.Features.Courses.Queries
{
    public class CourseQueryHandler 
        : IRequestHandler<GetCourseByIdQuery, CourseDto>,
            IRequestHandler<GetCoursesQuery, PagedList<CourseDto>>
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

        public async Task<PagedList<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await _dbContext.Courses
                .AsNoTracking()
                .OrderBy(q => q.Id)
                .Paginate(request.Page, request.PageSize)
                .ToListAsync(cancellationToken);
            var totalCount = await _dbContext.Courses.CountAsync(cancellationToken);

            var mappedItems = _mapper.Map<List<CourseDto>>(courses);
            return new PagedList<CourseDto>(mappedItems, request.Page, request.PageSize, totalCount);
        }
    }
}