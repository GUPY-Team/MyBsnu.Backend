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

namespace Modules.Timetable.Core.Features.Teachers.Queries
{
    public class TeacherQueryHandler
        : IRequestHandler<GetTeacherByIdQuery, TeacherDto>,
            IRequestHandler<GetTeachersQuery, PagedList<TeacherDto>>
    {
        private readonly IScheduleDbContext _dbContext;
        private readonly IMapper _mapper;

        public TeacherQueryHandler(IScheduleDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TeacherDto> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _dbContext.Teachers.FindAsync(request.Id);
            Guard.RequireEntityNotNull(teacher);

            return _mapper.Map<TeacherDto>(teacher);
        }

        public async Task<PagedList<TeacherDto>> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
        {
            var teachers = await _dbContext.Teachers
                .AsNoTracking()
                .OrderBy(t => t.Id)
                .Paginate(request.Page, request.PageSize)
                .ToListAsync(cancellationToken);
            var totalCount = await _dbContext.Teachers.CountAsync(cancellationToken);

            var mappedItems = _mapper.Map<List<TeacherDto>>(teachers);
            return new PagedList<TeacherDto>(mappedItems, request.Page, request.PageSize, totalCount);
        }
    }
}