using Ardalis.SmartEnum;

namespace Modules.Timetable.Core.Enums
{
    public class HalfYear : SmartEnum<HalfYear>
    {
        public static readonly HalfYear First = new(nameof(First), 0);
        public static readonly HalfYear Second = new(nameof(Second), 1);

        private HalfYear(string name, int value) : base(name, value)
        {
        }
    }
}