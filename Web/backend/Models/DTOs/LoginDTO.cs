namespace Findgroup_Backend.Models.DTOs
{
    public sealed record LoginDTO
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
