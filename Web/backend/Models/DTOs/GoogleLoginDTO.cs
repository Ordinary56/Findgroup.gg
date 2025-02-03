namespace Findgroup_Backend.Models.DTOs
{
    public sealed record GoogleLoginDTO
    {
        public required string Token { get; init; }
    }
}
