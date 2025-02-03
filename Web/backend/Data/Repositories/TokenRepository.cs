using Findgroup_Backend.Models;

namespace Findgroup_Backend.Data.Repositories
{
    public class TokenRepository(ApplicationDbContext context) : ITokenRepository, IDisposable
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _context = context;
        public void Dispose()
        {
            Dispose(true);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public async Task<RefreshToken?> GetStoredToken(string token)
        {
            RefreshToken? target = await _context.RefreshTokens.FindAsync(token);
            return target;
        }

        public IAsyncEnumerable<RefreshToken> GetAllTokens()
        {
            return _context.RefreshTokens.AsAsyncEnumerable();
        }

        public async Task AddToken(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
        }

        public void RemoveToken(RefreshToken token)
        {
             _context.RefreshTokens.Remove(token);
        }
    }
}
