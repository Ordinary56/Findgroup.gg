using Findgroup_Backend.Models;

namespace Findgroup_Backend.Data.Repositories
{
    public interface IUserRepository : IDisposable
    {
        public IAsyncEnumerable<User> GetUsers();
        public Task<User> GetUserById(string id);

        public Task AddNewUser(User newUser, string password);

        public Task DeleteUser(string id);

        public Task UpdateUser(User updatedUser);
        public Task Save();
    }
}
