using Microsoft.AspNetCore.Identity;
using System.Security.Policy;

namespace Findgroup_Backend.Models
{
    public partial class User : IdentityUser
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public IList<Post> Posts { get; set; } = [];
        public IList<OAuthAccount> OAuthAccounts { get; set; } = [];

    }
}
