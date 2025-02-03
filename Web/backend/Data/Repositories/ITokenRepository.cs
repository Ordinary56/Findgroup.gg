using Findgroup_Backend.Models;

namespace Findgroup_Backend.Data.Repositories
{
    public interface ITokenRepository
    {
        public Task<RefreshToken?> GetStoredToken(string token);
        public IAsyncEnumerable<RefreshToken> GetAllTokens();
        public Task AddToken(RefreshToken token);
        public void RemoveToken(RefreshToken token);
    }
}
