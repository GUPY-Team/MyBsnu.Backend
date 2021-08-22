namespace Shared.DTO.Schedule
{
    public record TeacherDto
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Patronymic { get; init; }
        public string FullName { get; init; }
    }
}