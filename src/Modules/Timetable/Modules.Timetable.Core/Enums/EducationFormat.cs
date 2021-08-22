using Ardalis.SmartEnum;

namespace Modules.Timetable.Core.Enums
{
    public class EducationFormat : SmartEnum<EducationFormat>
    {
        public static readonly EducationFormat Online = new(nameof(Online), 0);
        public static readonly EducationFormat Offline = new(nameof(Offline), 1);
        public static readonly EducationFormat Mixed = new(nameof(Mixed), 2);

        private EducationFormat(string name, int value) : base(name, value)
        {
        }
    }
}