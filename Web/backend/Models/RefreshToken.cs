using System.ComponentModel.DataAnnotations;

namespace Findgroup_Backend.Models
{
    ///<summary>Refreh Token model</summary>
    public record RefreshToken
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
        public required string TokenHash { get; init; }
        /// <summary>
        /// The user's ID (foreign key with one-to-one relationship)
        /// </summary>
        public string? UserId { get; set; }
        /// <summary>
        /// Expiration Date
        /// </summary>
        public DateTime ExpiresOnUTC { get; set; }

        public virtual User? User { get; set; }

        /// <summary>
        /// Check if <c>ExpiresOnUTC</c> already surpassed it's due date
        /// Expired refresh tokens are automatically revoked
        /// 
        /// </summary>
        public bool IsRevoked => DateTime.UtcNow >= ExpiresOnUTC;

    }
}
