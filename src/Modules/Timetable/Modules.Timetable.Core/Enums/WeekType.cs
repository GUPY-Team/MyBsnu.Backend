using Ardalis.SmartEnum;

namespace Modules.Timetable.Core.Enums
{
    public class WeekType : SmartEnum<WeekType>
    {
        public static readonly WeekType Odd = new(nameof(Odd), 0);
        public static readonly WeekType Even = new(nameof(Even), 1);

        private WeekType(string name, int value) : base(name, value)
        {
        }
    }
}