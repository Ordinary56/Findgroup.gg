namespace Findgroup_Backend.Models.DTOs
{
    public sealed record RefreshTokenDTO
    {
        public required string Token { get; init; }
    }
}
