namespace Shared.DTO.Identity
{
    public record SignupUserRequest
    {
        public string UserName { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
    }
}