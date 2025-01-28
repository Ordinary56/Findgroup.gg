using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Findgroup_Backend.Models
{
    public sealed class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; }

        [ForeignKey("User")]
        public string? UserId { get; set; }
        public bool IsActive { get; set; } = true;
        public User User { get; set; } = default!;
        public IList<Tag> Tags { get; set; } = [];
        


    }
}
