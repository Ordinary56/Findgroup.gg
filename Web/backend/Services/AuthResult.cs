namespace Findgroup_Backend.Services
{
    public sealed record AuthResult
    {
        public required string Token { get; init; }
        public required string RefreshToken { get; init; }
    }
}
