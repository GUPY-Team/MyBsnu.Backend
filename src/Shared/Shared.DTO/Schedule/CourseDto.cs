namespace Shared.DTO.Schedule
{
    public record CourseDto
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public string ShortName { get; init; }
    }
}