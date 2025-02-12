namespace Findgroup_Backend.Models.DTOs
{
    public sealed record PostDTO
    {
        public int? Id { get; init; }
        public required string Title { get; init; }
        public required string Content { get; init; }
        public int MemberSize { get; init; }
        public int? CategoryId { get; init; }

    }
}
