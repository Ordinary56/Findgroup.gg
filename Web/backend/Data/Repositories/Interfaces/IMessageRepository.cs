﻿using Findgroup_Backend.Models;

namespace Findgroup_Backend.Data.Repositories.Interfaces
{
    public interface IMessageRepository : IDisposable
    {
        public IAsyncEnumerable<Message> GetMessages();

        public Task AddNewMessage(Message message);

        public Task RemoveMessage(int id);

        public Task ModifyMessage(Message newMessage);

        public Task Save();

    }
}
