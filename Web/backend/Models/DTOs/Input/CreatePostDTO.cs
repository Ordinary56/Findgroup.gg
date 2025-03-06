namespace Findgroup_Backend.Models.DTOs.Input
{
    public sealed record CreatePostDTO
    {
        public required string Title { get; init; }
        public required string Content { get; init; }
        public DateTime CreatedDate { get; init; } = DateTime.UtcNow;
        public required int CategoryId { get; init; }
        public required string UserId { get; init; }
    }
}
