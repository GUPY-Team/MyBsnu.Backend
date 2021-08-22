using System.Collections.Generic;
using Shared.Core.Domain;

namespace Modules.Timetable.Core.Entities
{
    public class Group : Entity<int>
    {
        public string Number { get; set; }

        public ICollection<Class> Classes { get; set; } = new List<Class>();
    }
}