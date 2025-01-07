using System.ComponentModel.DataAnnotations;

namespace Findgroup_Backend.Models
{
    public class OAuthAccount
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Provider { get; set; }
        [Required]
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? TokenExpiry {  get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
