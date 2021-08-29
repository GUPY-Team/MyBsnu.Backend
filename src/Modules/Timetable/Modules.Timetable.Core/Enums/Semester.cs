using Ardalis.SmartEnum;

namespace Modules.Timetable.Core.Enums
{
    public class Semester : SmartEnum<Semester>
    {
        public static readonly Semester First = new(nameof(First), 0);
        public static readonly Semester Second = new(nameof(Second), 1);

        private Semester(string name, int value) : base(name, value)
        {
        }
    }
}