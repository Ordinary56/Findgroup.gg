using Findgroup_Backend.Models;

namespace Findgroup_Backend.Data.Repositories.Interfaces
{
    public interface IMessageRepository : IDisposable
    {
        public IAsyncEnumerable<Message> GetMessages();
        public IAsyncEnumerator<Message> GetGroupMessages(Guid groupId);
        public Task AddNewMessage(Message message);

        public Task RemoveMessage(Message message);

        public Task<Message> ModifyMessage(Message message);
        public Task Save();

    }
}
