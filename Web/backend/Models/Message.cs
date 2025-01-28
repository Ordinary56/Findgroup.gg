using System.ComponentModel.DataAnnotations;

namespace Findgroup_Backend.Models
{
    public sealed class Message
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public string Content { get; set; } = "";

        public User User { get; set; }

    }
}
