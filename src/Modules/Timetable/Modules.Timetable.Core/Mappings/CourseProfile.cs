using AutoMapper;
using Modules.Timetable.Core.Entities;
using Modules.Timetable.Core.Features.Courses.Commands;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Mappings
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CreateCourseCommand, Course>();
            CreateMap<UpdateCourseCommand, Course>();
            CreateMap<Course, CourseDto>();
        }
    }
}