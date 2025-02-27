namespace Findgroup_Backend.Models.DTOs.Input
{
    public sealed record RegisterUserDTO
    {
        public string Id = Guid.NewGuid().ToString();
        public required string UserName { get; init; }  
        public required string Password { get; init; }  
        public required string Email { get; init; }
        public string? PhoneNumber { get; init; }
    }
}
