using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Findgroup_Backend.Data.Repositories
{
    public class MessageRepository : IMessageRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed;
        public async Task AddNewMessage(Message message)
        {
            await _context.Messages.AddAsync(message);
            await Save();
        }

        public async Task<Message> ModifyMessage(Message oldMessage, Message newMessage)
        {
           throw new NotImplementedException();
        }

        public async Task RemoveMessage(int id)
        {
            Message? target = await _context.Messages.FirstOrDefaultAsync(x => x.Id == id);
            if (target == null) return;
            _context.Messages.Remove(target);
            await Save();
        }
        public void Dispose() 
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing) 
        {
            if (disposed) return;
            if (disposing) 
            {
                _context.Dispose(); 
                disposed = true;
            }
        }

        public IAsyncEnumerable<Message> GetMessages()
        {
            return _context.Messages.Include(m => m.User).Include(m => m.Group).AsAsyncEnumerable();
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();  
        }
    }
}
