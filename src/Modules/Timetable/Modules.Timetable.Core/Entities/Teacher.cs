using System.Collections.Generic;
using Shared.Core.Domain;

namespace Modules.Timetable.Core.Entities
{
    public class Teacher : Entity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ThirdName { get; set; }

        public string FullName => $"{FirstName} {LastName} {LastName}";
        public string ShortName => $"{LastName} {FirstName[0]}.{ThirdName[0]}";

        public ICollection<Class> Classes { get; set; }
    }
}