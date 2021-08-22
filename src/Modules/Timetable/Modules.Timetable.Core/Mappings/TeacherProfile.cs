using AutoMapper;
using Modules.Timetable.Core.Entities;
using Modules.Timetable.Core.Features.Teachers.Commands;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Mappings
{
    public class TeacherProfile : Profile
    {
        public TeacherProfile()
        {
            CreateMap<CreateTeacherCommand, Teacher>();
            CreateMap<UpdateTeacherCommand, Teacher>();
            CreateMap<Teacher, TeacherDto>();
        }
    }
}