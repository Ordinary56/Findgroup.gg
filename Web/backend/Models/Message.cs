using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Findgroup_Backend.Models
{
    public sealed class Message
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(Group))]
        public int GroupId { get; set; }

        
        public required string Content { get; set; } = "";

        public User User { get; set; } = default!;

        public Group Group { get; set; } = default!;
    }
}
