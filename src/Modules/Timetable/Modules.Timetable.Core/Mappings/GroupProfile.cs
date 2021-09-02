using AutoMapper;
using Modules.Timetable.Core.Entities;
using Modules.Timetable.Core.Features.Groups.Commands;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Mappings
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<CreateGroupCommand, Group>();
            CreateMap<UpdateGroupCommand, Group>();
            CreateMap<Group, GroupDto>().ReverseMap();
        }
    }
}