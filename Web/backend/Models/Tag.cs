using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Findgroup_Backend.Models
{
    public class Tag
    {
        [Required]
        public string Content { get; set; } = "";
        [ForeignKey("Post")]
        public int? PostId { get; set; }
        public Post Post { get; set; } = default!;

    }
}
