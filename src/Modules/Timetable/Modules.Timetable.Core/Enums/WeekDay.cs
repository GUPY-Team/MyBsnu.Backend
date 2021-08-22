using Ardalis.SmartEnum;

namespace Modules.Timetable.Core.Enums
{
    public class WeekDay : SmartEnum<WeekDay>
    {
        public static readonly WeekDay Monday = new(nameof(Monday), 0);
        public static readonly WeekDay Tuesday = new(nameof(Tuesday), 1);
        public static readonly WeekDay Wednesday = new(nameof(Wednesday), 2);
        public static readonly WeekDay Thursday = new(nameof(Thursday), 3);
        public static readonly WeekDay Friday = new(nameof(Friday), 4);
        public static readonly WeekDay Saturday = new(nameof(Saturday), 5);
        public static readonly WeekDay Sunday = new(nameof(Sunday), 6);

        private WeekDay(string name, int value) : base(name, value)
        {
        }
    }
}