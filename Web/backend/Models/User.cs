using Microsoft.AspNetCore.Identity;
using System.Security.Policy;

namespace Findgroup_Backend.Models
{
    public partial class User : IdentityUser
    {
        public RefreshToken RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public IList<Post> Posts { get; set; } = [];
        public IList<OAuthAccount> OAuthAccounts { get; set; } = [];

        public IList<Group> JoinedGroups { get; set; } = [];

    }
}
