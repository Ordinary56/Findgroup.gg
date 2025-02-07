using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Findgroup_Backend.Models
{
    ///<summary>Refreh Token model</summary>
    public sealed class RefreshToken
    {
        /// <summary>
        /// The token's ID (primary Key)
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The token's hash (indexed)
        /// </summary>
        /// <remarks>This property should always be unique</remarks>
        [Required, MaxLength(200)]
        public required string TokenHash { get; set; }
        /// <summary>
        /// The user's ID (foreign key with one-to-one relationship)
        /// </summary>
        [ForeignKey(nameof(UserId))]
        public string? UserId { get; set; }
        /// <summary>
        /// Expiration Date
        /// </summary>
        public DateTime ExpiresOnUTC { get; set; }
        public User? User { get; set; }

        /// <summary>
        /// Check if <c>ExpiresOnUTC</c> already surpassed it's due date
        /// Expired refresh tokens are automatically revoked
        /// 
        /// </summary>
        public bool IsRevoked => DateTime.UtcNow >= ExpiresOnUTC;

    }
}
