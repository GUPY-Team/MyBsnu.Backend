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
            CreateMap<CreateClassCommand, Class>()
                .ForMember(c => c.Teachers, o => o.Ignore())
                .ForMember(c => c.Audiences, o => o.Ignore())
                .ForMember(c => c.Groups, o => o.Ignore());
            CreateMap<UpdateClassCommand, Class>()
                .ForMember(c => c.Teachers, o => o.Ignore())
                .ForMember(c => c.Audiences, o => o.Ignore())
                .ForMember(c => c.Groups, o => o.Ignore());
            CreateMap<Class, ClassDto>();
        }
    }
}