using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs.Input;

namespace Findgroup_Backend.Data.Repositories.Interfaces
{
    public interface IGroupRepository : IDisposable
    {
        public IAsyncEnumerable<Group> GetGroups();
        public Task<Group?> GetGroupById(Guid id);
        public Task CreateNewGroup(CreateGroupDTO dto, User Creator);
        public Task JoinGroup(Group targetGroup, User newMember);
        public Task LeaveGroup(Group targetGroup, User targetUser);
        public Task DeleteGroup(string name);
        public Task Save();
    }
}
