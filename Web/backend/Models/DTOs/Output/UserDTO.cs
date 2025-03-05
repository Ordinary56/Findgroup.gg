namespace Findgroup_Backend.Models.DTOs.Output
{
    public sealed record UserDTO
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
    }
}
