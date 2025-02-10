using Microsoft.AspNetCore.Identity;
using System.Security.Policy;
using System.Text.Json.Serialization;

namespace Findgroup_Backend.Models
{
    public partial class User : IdentityUser
    {
        public RefreshToken RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        [JsonIgnore]
        public IList<Post> Posts { get; set; } = [];
        public IList<OAuthAccount> OAuthAccounts { get; set; } = [];

        public IList<Group> JoinedGroups { get; set; } = [];

    }
}
