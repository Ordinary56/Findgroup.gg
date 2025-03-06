using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs.Output;

namespace Findgroup_Backend.Data.Repositories.Interfaces
{
    public interface IGroupRepository : IDisposable
    {
        public IAsyncEnumerable<GroupDTO> GetGroups();
        public Task<Group?> GetGroupById(Guid id);
        public Task CreateNewGroup(Group newGroup, User Creator);
        public Task JoinGroup(Group targetGroup, User newMember);
        public Task LeaveGroup(Group targetGroup, User targetUser);
        public Task DeleteGroup(string name);
        public Task Save();
    }
}
