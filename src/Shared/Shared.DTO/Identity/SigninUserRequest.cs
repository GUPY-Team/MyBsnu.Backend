namespace Shared.DTO.Identity
{
    public record SigninUserRequest
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}