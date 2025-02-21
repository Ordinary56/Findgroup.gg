using Findgroup_Backend.Data.Repositories.Interfaces;
using Findgroup_Backend.Models;

namespace Findgroup_Backend.Data.Repositories
{
    public class MessageRepository : IMessageRepository, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private bool disposed;
        public Task AddNewMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public Task<Message> ModifyMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public Task RemoveMessage(Message message)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public IAsyncEnumerator<Message> GetGroupMessages(Guid groupId)
        {
            throw new NotImplementedException();
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();  
        }
    }
}
