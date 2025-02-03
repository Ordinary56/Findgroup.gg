using Findgroup_Backend.Models;

namespace Findgroup_Backend.Data.Repositories
{
    public interface ITokenService
    {
        public Task<RefreshToken> GetStoredToken(string token, User user);
        public IAsyncEnumerable<RefreshToken> GetAllTokens();
        public Task AddToken(RefreshToken token);
        public Task RemoveToken(RefreshToken token);
    }
}
