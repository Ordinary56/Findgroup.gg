using Findgroup_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Findgroup_Backend.Data.Repositories
{
    public class GroupRepository(ApplicationDbContext context) : IGroupRepository, IDisposable
    {
        private  bool _disposed = false;
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

        public IAsyncEnumerable<Group> GetGroups()
        {
            return _context.Groups.AsAsyncEnumerable();
        }

        public async Task<Group?> GetGroupById(Guid id)
        {
            return await _context.Groups.FindAsync(id);
        }

        public async Task CreateNewGroup(string Name, int memberLimit, User Creator)
        {
            Group newGroup = new()
            {
                GroupName = Name,
                MemberLimit = memberLimit,
            };
            newGroup.Users = new List<User>()
            {
                Capacity = newGroup.MemberLimit
            };
            newGroup.Users.Add(Creator);
            Creator.JoinedGroups.Add(newGroup);
            await _context.Groups.AddAsync(newGroup);
        }

        public async Task JoinGroup(Group targetGroup, User newMember)
        {

            try
            {
                targetGroup.Users.Add(newMember);
                await Save();
            }
            catch (Exception ex) 
            {
                throw;
            }
        }

        public async Task DeleteGroup(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            Group? target = await _context.Groups.FirstOrDefaultAsync(x => x.GroupName == name);
            if (target is null) throw new InvalidOperationException("Target group not found");
            _context.Groups.Remove(target);
            await Save();
        } 
        public Task UpdateGroup(Group group)
        {
            throw new NotImplementedException();
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
