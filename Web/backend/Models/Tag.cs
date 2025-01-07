using System.ComponentModel.DataAnnotations;

namespace Findgroup_Backend.Models
{
    public class Tag
    {
        [Required]
        public string Content { get; set; } = "";
        public int PostId { get; set; }
        public Post Post { get; set; } = default!;

    }
}
