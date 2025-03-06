using AutoMapper;
using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Models;
using Findgroup_Backend.Models.DTOs.Output;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Findgroup_Backend.Data.Repositories
{
    public class UserRepository : IUserRepository, IDisposable
    {
        // Fields
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _manager;
        private readonly IMapper _mapper;
        private bool _disposed = false;

        public UserRepository(ApplicationDbContext context, UserManager<User> manager, IMapper mapper)
        {
            _context = context;
            _manager = manager;
            _mapper = mapper;
        }

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
            ArgumentException.ThrowIfNullOrEmpty(id);
            var user = await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
            if (user is null)
            {
                throw new InvalidOperationException($"User with specified Id: {id} was not found");
            }
            return user;

        }

        public IAsyncEnumerable<UserDTO> GetUsers()
        {
            return _context.Users.Select(u => _mapper.Map<UserDTO>(u)).AsAsyncEnumerable();
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
