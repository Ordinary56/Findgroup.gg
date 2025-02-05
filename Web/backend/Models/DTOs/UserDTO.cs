namespace Findgroup_Backend.Models.DTOs
{
    public sealed record UserDTO
    {
        public string? Id { get; init; } = Guid.NewGuid().ToString();
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

    }
}
