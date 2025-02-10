using Findgroup_Backend.Models;

namespace Findgroup_Backend.Data.Repositories
{
    public interface IGroupRepository : IDisposable
    {
        public IAsyncEnumerable<Group> GetGroups();
        public Task<Group?> GetGroupById(Guid id);
        public Task CreateNewGroup(string Name, string description, int memberLimit, User Creator);
        public Task JoinGroup(Group targetGroup, User newMember);
        public Task LeaveGroup(Group targetGroup, User targetUser);
        public Task DeleteGroup(string name);
        public Task Save();
    }
}
