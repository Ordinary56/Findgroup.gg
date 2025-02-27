namespace Findgroup_Backend.Models.DTOs.Output
{
    public sealed record CategoryDTO
    {
        public int Id { get; init; }
        public required string CategoryName { get; init; }
    }
}
