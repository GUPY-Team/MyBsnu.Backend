using AutoMapper;
using Modules.Timetable.Core.Entities;
using Modules.Timetable.Core.Features.Audiences.Commands;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Mappings
{
    public class AudienceProfile : Profile
    {
        public AudienceProfile()
        {
            CreateMap<CreateAudienceCommand, Audience>();
            CreateMap<UpdateAudienceCommand, Audience>();
            CreateMap<Audience, AudienceDto>();
        }
    }
}