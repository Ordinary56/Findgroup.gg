using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Findgroup_Backend.Data.Repositories
{
    public class UserRepository(ApplicationDbContext context, UserManager<User> manager) : IUserRepository, IDisposable
    {
        private readonly ApplicationDbContext _context = context;
        private readonly UserManager<User> _manager = manager;
        private bool _disposed = false;
        public async Task AddNewUser(User newUser, string password)
        {
            ArgumentNullException.ThrowIfNull(newUser);
            var result = await _manager.CreateAsync(newUser, password);
            if (result.Succeeded) return;
            var errors = string.Join(',', result.Errors);
            throw new Exception(errors);
        }

        public async Task DeleteUser(string id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id);
            User? target = await _context.Users.FindAsync(id);
            ArgumentNullException.ThrowIfNull(target);
            _context.Users.Remove(target);
            await Save();
        }


        public async Task<User> GetUserById(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id) ?? throw new Exception();
        }

        public IAsyncEnumerable<User> GetUsers()
        {
            return _context.Users.AsAsyncEnumerable();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User updatedUser)
        {
            ArgumentNullException.ThrowIfNull(_context);
            User target = await _context.Users.FindAsync(updatedUser.Id) ?? throw new Exception("target is null");
            target.UserName = updatedUser.UserName;
            target.PhoneNumber = updatedUser.PhoneNumber;
            target.Email = updatedUser.Email;
            await Save();

        }
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
