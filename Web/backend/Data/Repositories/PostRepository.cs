using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Tls;

namespace Findgroup_Backend.Data.Repositories
{
    public class PostRepository(ApplicationDbContext context) : IPostRepository, IDisposable
    {
        private readonly ApplicationDbContext _context = context;
        private bool _disposed = false;

        public async Task CreateNewPost(Post newPost)
        {
            if (await _context.Posts.AnyAsync(p => p.Id == newPost.Id)) throw new Exception();
            await _context.AddAsync(newPost);
            await Save();
        }

        public async Task DeletePostAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(id);
            Post target = await _context.Posts.FindAsync(id) ?? throw new Exception();
            _context.Posts.Remove(target);
            await Save();
        }


        public async Task<Post> GetPostById(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(id);
            Post target = await _context.FindAsync<Post>(id) ?? throw new Exception();
            return target;
        }

        public IAsyncEnumerable<Post> GetPosts()
        {
            return _context.Posts.Include(p => p.Category).Include(p => p.User).AsAsyncEnumerable();
        }

        public async Task ModifyPostAsync(Post post)
        {
            Post target = await _context.Posts.FindAsync(post.Id) ?? throw new Exception();
            target.Content = post.Content;
            target.IsActive = post.IsActive;
            await Save();
        }
        public async Task Save() => await _context.SaveChangesAsync();
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
    }
}
