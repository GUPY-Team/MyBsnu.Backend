namespace Modules.Timetable.Core.Features.ScheduleDesigner.Queries
{
    public interface IIdleEntityQuery
    {
        public int ScheduleId { get; init; }
        public string WeekDay { get; init; }
        public string WeekType { get; init; }
        public string StartTime { get; init; }
    }
}