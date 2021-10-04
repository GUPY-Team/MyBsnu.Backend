using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Modules.Timetable.Core.Abstractions;
using Shared.Core.Helpers;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Teachers.Queries
{
    public class TeacherQueryHandler 
        : IRequestHandler<GetTeacherByIdQuery, TeacherDto>,
            IRequestHandler<GetTeachersQuery, List<TeacherDto>>
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

        public async Task<List<TeacherDto>> Handle(GetTeachersQuery request, CancellationToken cancellationToken)
        {
            var teachers = await _dbContext.Teachers
                .AsNoTracking()
                .OrderBy(t => t.Id)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<TeacherDto>>(teachers);
        }
    }
}