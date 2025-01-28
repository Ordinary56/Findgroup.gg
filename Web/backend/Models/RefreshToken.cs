using System.ComponentModel.DataAnnotations;

namespace Findgroup_Backend.Models
{
    public sealed class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Token { get; set; }   
        [Required]
        public Guid UserId { get; set; }
        public DateTime ExpiresOnUTC { get; set; }
        public User User { get; set; }

    }
}
