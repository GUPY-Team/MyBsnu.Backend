namespace Shared.DTO.Schedule
{
    public record TeacherDto
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string ThirdName { get; init; }
        public string FullName { get; init; }
        public string ShortName { get; init; }
    }
}