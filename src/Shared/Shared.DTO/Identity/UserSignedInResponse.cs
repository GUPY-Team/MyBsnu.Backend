namespace Shared.DTO.Identity
{
    public record UserSignedInResponse
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; init; }
    }
}