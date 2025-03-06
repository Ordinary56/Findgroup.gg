using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs.Output;
namespace Findgroup_Backend.Data.Repositories.Interfaces
{
    public interface IPostRepository : IDisposable
    {
        public IAsyncEnumerable<PostDTO> GetPosts();
        public Task<PostDTO?> GetPostById(int id);
        public Task DeletePostAsync(int id);
        public Task ModifyPostAsync(Post post);
        public Task CreateNewPost(Post newPost, User Creator, Category category);
        public Task Save();
    }
}
