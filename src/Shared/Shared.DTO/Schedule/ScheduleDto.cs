namespace Shared.DTO.Schedule
{
    public record ScheduleDto
    {
        public string HalfYear { get; init; }
        public int Year { get; init; }
        public int Version { get; init; }
        public bool IsPublished { get; init; }
    }
}