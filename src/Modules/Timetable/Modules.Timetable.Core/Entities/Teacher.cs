using Shared.Core.Domain;

namespace Modules.Timetable.Core.Entities
{
    public class Teacher : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ThirdName { get; set; }

        public string FullName => $"{FirstName} {LastName} {LastName}";
    }
}