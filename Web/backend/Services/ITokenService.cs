using Findgroup_Backend.Models;

namespace Findgroup_Backend.Services
{
    public interface ITokenService
    {
        public Task<string> GenerateAccessToken(User user);
        public RefreshToken GenerateRefreshToken();

    }
}
