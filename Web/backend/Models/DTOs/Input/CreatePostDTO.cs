namespace Findgroup_Backend.Models.DTOs.Input
{
    public sealed record CreatePostDTO
    {
        public required string Title { get; init; } 
        public required string Content { get; init; }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        public int CategoryId { get; init; }
        public Category? Category { get; init; }


    }
}
