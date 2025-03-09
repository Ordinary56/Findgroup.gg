using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Findgroup_Backend.Data.Repositories
{
    public class TokenRepository(ApplicationDbContext context) : ITokenRepository, IDisposable
    {
        private bool _disposed = false;
        private readonly ApplicationDbContext _context = context;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
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
            RefreshToken? target = await _context.RefreshTokens.Include(t => t.User).FirstOrDefaultAsync(t => t.TokenHash == token);
            return target;
        }

        public IAsyncEnumerable<RefreshToken> GetAllTokens()
        {
            return _context.RefreshTokens.Include(t => t.User).AsAsyncEnumerable();
        }

        public async Task AddToken(RefreshToken token)
        {
            await _context.RefreshTokens.AddAsync(token);
            await Save();
        }

        public async void RemoveToken(RefreshToken token)
        {
            _context.RefreshTokens.Remove(token);
            await Save();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
