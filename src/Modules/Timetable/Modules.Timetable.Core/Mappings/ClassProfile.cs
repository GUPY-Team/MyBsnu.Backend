using AutoMapper;
using Modules.Timetable.Core.Entities;
using Modules.Timetable.Core.Features.Classes.Commands;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Mappings
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<CreateClassCommand, Class>();
            CreateMap<UpdateClassCommand, Class>();
            CreateMap<Class, ClassDto>().ConstructUsing(c => new ClassDto
            {
                TeacherName = c.Teacher.FullName,
                CourseName = c.Course.Name,
                AudienceNumber = c.Audience == null ? null : c.Audience.FullNumber,
                Format = c.Format.Name,
                WeekDay = c.WeekDay.Name,
                WeekType = c.WeekType.Name,
                Type = c.Type.Name
            });
        }
    }
}