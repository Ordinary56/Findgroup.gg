using Findgroup_Backend.Models;
namespace Findgroup_Backend.Data.Repositories
{
    public interface IPostRepository : IDisposable
    {
        public IAsyncEnumerable<Post> GetPosts();
        public Task<Post> GetPostById(int id);
        public Task DeletePostAsync(int id);
        public Task ModifyPostAsync(Post post);
        public Task CreateNewPost(Post newPost);
        public Task Save();
    }
}
