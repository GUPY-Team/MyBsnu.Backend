using System;
using AutoMapper;
using Shared.Core.Constants;
using Shared.DTO;

namespace Shared.Infrastructure.Mappings
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<TimeDto, TimeSpan>().ConvertUsing(t => t.AsTimeSpan());
            CreateMap<TimeSpan, TimeDto>();
            CreateMap<TimeSpan, string>().ConvertUsing(t => t.ToString(CommonConstants.Formatting.TimeSpan));
        }
    }
}