using System.ComponentModel.DataAnnotations;

namespace Findgroup_Backend.Models
{
    public record Post
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public string? UserId { get; set; }
        public int? CategoryId { get; set; }
        public User? Creator { get; set; }
        public Category? Category { get; set; } = default!;
        public virtual Group? Group { get; set; } = default!;
    }
}
