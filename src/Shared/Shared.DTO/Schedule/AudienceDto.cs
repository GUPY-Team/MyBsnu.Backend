namespace Shared.DTO.Schedule
{
    public record AudienceDto
    {
        public int Id { get; init; }

        public int Corps { get; init; }
        public int Floor { get; init; }
        public int Room { get; init; }

        public string FullNumber { get; init; }
    }
}