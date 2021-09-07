using Ardalis.SmartEnum;
using AutoMapper;
using Modules.Timetable.Core.Enums;

namespace Modules.Timetable.Core.Mappings
{
    public class EnumConverter<TEnum>
        : ITypeConverter<SmartEnum<TEnum>, string>,
          ITypeConverter<string, TEnum>
        where TEnum : SmartEnum<TEnum>
    {
        public string Convert(SmartEnum<TEnum> source, string destination, ResolutionContext context)
        {
            return source?.Name;
        }

        public TEnum Convert(string source, TEnum destination, ResolutionContext context)
        {
            return source == null ? null : SmartEnum<TEnum>.FromName(source);
        }
    }


    public class EnumsProfile : Profile
    {
        public EnumsProfile()
        {
            CreateMap<EducationFormat, string>().ConvertUsing<EnumConverter<EducationFormat>>();
            CreateMap<string, EducationFormat>().ConvertUsing<EnumConverter<EducationFormat>>();

            CreateMap<ClassType, string>().ConvertUsing<EnumConverter<ClassType>>();
            CreateMap<string, ClassType>().ConvertUsing<EnumConverter<ClassType>>();

            CreateMap<WeekDay, string>().ConvertUsing<EnumConverter<WeekDay>>();
            CreateMap<string, WeekDay>().ConvertUsing<EnumConverter<WeekDay>>();

            CreateMap<WeekType, string>().ConvertUsing<EnumConverter<WeekType>>();
            CreateMap<string, WeekType>().ConvertUsing<EnumConverter<WeekType>>();
        }
    }
}