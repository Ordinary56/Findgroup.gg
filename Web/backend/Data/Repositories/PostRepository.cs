using AutoMapper;
using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Findgroup_Backend.Data.Repositories
{
    public class PostRepository : IPostRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private bool _disposed = false;

        public PostRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateNewPost(Post newPost)
        {
            await _context.AddAsync(newPost);
            await Save();
        }

        public async Task DeletePostAsync(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(id);
            Post? targetPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (targetPost != null)
            {
                _context.Posts.Remove(targetPost);
                await Save();
            }
        }


        public async Task<Post?> GetPostById(int id)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(id);
            Post? target = await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Group)
                .FirstOrDefaultAsync(p => p.Id == id);
            return target;
        }

        public IAsyncEnumerable<Post> GetPosts()
        {
            return _context.Posts.Include(p => p.Category)
                .Include(p => p.User)
                .Include(p => p.Group).AsAsyncEnumerable();
        }

        public async Task ModifyPostAsync(Post post)
        {
            Post? target = await _context.Posts.FirstOrDefaultAsync(p => p.Id == post.Id);
            if (target != null)
            {
                _context.Entry(target).CurrentValues.SetValues(post);
                await Save();
            }
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
