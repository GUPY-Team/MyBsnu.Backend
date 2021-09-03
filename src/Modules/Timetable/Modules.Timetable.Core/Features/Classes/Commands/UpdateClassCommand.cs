using System.Collections.Generic;
using MediatR;
using Shared.DTO;
using Shared.DTO.Schedule;

namespace Modules.Timetable.Core.Features.Classes.Commands
{
    public class UpdateClassCommand : IRequest<ClassDto>
    {
        public int Id { get; init; }
        public string Format { get; init; }
        public string Type { get; init; }
        public string WeekDay { get; init; }
        public string WeekType { get; init; }
        public TimeDto StartTime { get; init; }
        public TimeDto EndTime { get; init; }
        public int CourseId { get; init; }
        public IEnumerable<int> Teachers { get; init; } = new List<int>();
        public IEnumerable<int> Audiences { get; init; } = new List<int>();
        public IEnumerable<int> Groups { get; init; } = new List<int>();
    }
}