namespace Findgroup_Backend.Models.DTOs.Input
{
    public sealed record LoginDTO
    {
        public required string Username { get; init; }
        public required string Password { get; init; }
    }
}
