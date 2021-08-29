namespace Shared.DTO.Schedule
{
    public record ScheduleDto
    {
        public int Id { get; init; }
        public string Semester { get; init; }
        public int Year { get; init; }
        public int Version { get; init; }
        public bool IsPublished { get; init; }
    }
}