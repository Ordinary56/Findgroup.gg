namespace Findgroup_Backend.Models.DTOs
{
    public sealed record MessageDTO
    {
        public int Id { get; init; }
        public string GroupId { get; init; }
        public string UserId { get; init; }
        public string Content { get; init; }
    }
}
