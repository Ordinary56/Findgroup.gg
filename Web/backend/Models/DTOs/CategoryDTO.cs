namespace Findgroup_Backend.Models.DTOs
{
    public sealed record CategoryDTO
    {
        public required int Id { get; set; }
        public required string CategoryName { get; set; }

    }
}
