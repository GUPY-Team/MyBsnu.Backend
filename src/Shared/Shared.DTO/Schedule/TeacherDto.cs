namespace Shared.DTO.Schedule
{
    public record TeacherDto
    {
        public int Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string ThirdName { get; init; }
        public string FullName => $"{FirstName} {LastName} {LastName}";
        public string ShortName => $"{LastName} {FirstName[0]}.{ThirdName[0]}";
    }
}