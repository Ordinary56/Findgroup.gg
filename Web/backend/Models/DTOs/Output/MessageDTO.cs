namespace Findgroup_Backend.Models.DTOs.Output
{
    public sealed record MessageDTO
    {
        public int Id { get; init; }
        public required string Content { get; init; }
        public required string GroupId { get; init; }
    }
}
