using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Findgroup_Backend.Models
{
    public sealed record Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsActive { get; set; } = true;

        public string? UserId { get; set; }

        public int? CategoryId { get; set; }
        public User? User { get; set; }
        public Category? Category { get; set; }



    }
}
