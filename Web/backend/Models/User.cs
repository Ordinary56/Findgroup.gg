using Microsoft.AspNetCore.Identity;

namespace Findgroup_Backend.Models
{
    public partial class User : IdentityUser
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}
