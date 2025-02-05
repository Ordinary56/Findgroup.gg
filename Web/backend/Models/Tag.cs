using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Findgroup_Backend.Models
{
    [Microsoft.EntityFrameworkCore.Index(nameof(Id), IsUnique = true)]
    public class Tag
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; } = "";
        [ForeignKey("Post")]
        public int? PostId { get; set; }
        public Post Post { get; set; } = default!;

    }
}
