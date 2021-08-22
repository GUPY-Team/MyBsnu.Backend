using Shared.Core.Domain;

namespace Modules.Timetable.Core.Entities
{
    public class Audience : Entity<int>
    {
        public int Corps { get; set; }
        public int Floor { get; set; }
        public int Room { get; set; }

        public string FullNumber => $"{Corps}-{Floor}{Room.ToString().PadLeft(2, '0')}";
    }
}