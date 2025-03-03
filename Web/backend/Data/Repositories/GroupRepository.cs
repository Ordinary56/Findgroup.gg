using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs.Input;
using Microsoft.EntityFrameworkCore;
using System.CodeDom;
using System.Runtime.InteropServices;

namespace Findgroup_Backend.Data.Repositories
{
    public class GroupRepository(ApplicationDbContext context) : IGroupRepository, IDisposable
    {
        private bool _disposed = false;
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
            return _context.Groups.Include(g => g.Users).Include(g => g.Post).AsAsyncEnumerable();
        }

        public async Task<Group?> GetGroupById(Guid id)
        {
            return await _context.Groups.FindAsync(id);
        }

        public async Task CreateNewGroup(Group newGroup, User Creator)
        {
            newGroup.Id = Guid.NewGuid();
            newGroup.Users.Add(Creator);
            Creator.JoinedGroups.Add(newGroup);
            await _context.Groups.AddAsync(newGroup);
            await Save();
        }

        public async Task JoinGroup(Group targetGroup, User newMember)
        {
            if (targetGroup.Users.Count + 1 > targetGroup.MemberLimit)
                throw new InvalidOperationException("Can't add more users to this");
            targetGroup.Users.Add(newMember);
            newMember.JoinedGroups.Add(targetGroup);
            await Save();

        }

        public async Task DeleteGroup(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            Group? target = await _context.Groups.FirstOrDefaultAsync(x => x.GroupName == name) ??
                throw new InvalidOperationException("Target group not found");
            _context.Groups.Remove(target);
            await Save();
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task LeaveGroup(Group targetGroup, User targetUser)
        {
            if (_context.Entry(targetUser).State == EntityState.Detached)
            {
                _context.Attach(targetUser);
            }
            if (_context.Entry(targetGroup).State == EntityState.Detached)
            {
                _context.Attach(targetGroup);
            }
            await _context.Entry(targetUser).Collection(u => u.JoinedGroups).LoadAsync();
            await _context.Entry(targetGroup).Collection(u => u.Users).LoadAsync();
            targetGroup.Users.Remove(targetUser);
            targetUser.JoinedGroups.Remove(targetGroup);
            await Save();
        }
    }
}
