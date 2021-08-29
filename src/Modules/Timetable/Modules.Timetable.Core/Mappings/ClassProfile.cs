using AutoMapper;
using Modules.Timetable.Core.Entities;
using Modules.Timetable.Core.Enums;
using Modules.Timetable.Core.Features.Classes.Commands;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Mappings
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<CreateClassCommand, Class>().ConstructUsing(c => new Class
            {
                Format = EducationFormat.FromName(c.Format, true),
                Type = ClassType.FromName(c.Type, true),
                WeekDay = WeekDay.FromName(c.WeekDay, true),
                WeekType = WeekType.FromName(c.WeekType, true)
            });
            CreateMap<UpdateClassCommand, Class>().ConstructUsing(c => new Class
            {
                Format = EducationFormat.FromName(c.Format, true),
                Type = ClassType.FromName(c.Type, true),
                WeekDay = WeekDay.FromName(c.WeekDay, true),
                WeekType = WeekType.FromName(c.WeekType, true)
            });
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