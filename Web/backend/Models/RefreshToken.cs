using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Findgroup_Backend.Models
{
    public sealed class RefreshToken
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(200)]
        public required string TokenHash { get; set; }
        [ForeignKey(nameof(UserId))]
        public string? UserId { get; set; }
        public DateTime ExpiresOnUTC { get; set; }
        public User? User { get; set; }

        public bool IsRevoked => DateTime.UtcNow >= ExpiresOnUTC;

    }
}
