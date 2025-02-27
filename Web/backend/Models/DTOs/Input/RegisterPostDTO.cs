namespace Findgroup_Backend.Models.DTOs.Input
{
    public sealed record RegisterPostDTO
    {
        public required string Title { get; init; }
        public required string Content { get; init; }
        public DateTime CreatedDate { get; init; }
        public required string UserId { get; init; }
        public int CategoryId { get; init; }

    }
}
