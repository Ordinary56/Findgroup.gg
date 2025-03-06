using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs.Output;

namespace Findgroup_Backend.Data.Repositories.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        public IAsyncEnumerable<UserDTO> GetUsers();
        public Task<User> GetUserById(string id);

        public Task AddNewUser(User newUser, string password);

        public Task DeleteUser(string id);

        public Task UpdateUser(User updatedUser);
        public Task Save();
    }
}
