using Ardalis.SmartEnum;

namespace Modules.Timetable.Core.Enums
{
    public class ClassType : SmartEnum<ClassType>
    {
        public static readonly ClassType Lecture = new(nameof(Lecture), 0);
        public static readonly ClassType Practice = new(nameof(Practice), 1);

        private ClassType(string name, int value) : base(name, value)
        {
        }
    }
}