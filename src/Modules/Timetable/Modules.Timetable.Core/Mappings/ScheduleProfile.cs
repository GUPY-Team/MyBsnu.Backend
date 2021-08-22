using AutoMapper;
using Modules.Timetable.Core.Entities;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Mappings
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Schedule, ScheduleDto>();
        }
    }
}