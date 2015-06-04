using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WebAuto.DataAccess.EntityFramework
{
    public class MessageDataAccess : IMessageDataAccess
    {
        private static List<Message> _messages = new List<Message>();

        public Task CreateAsync(Message message)
        {
            _messages.Add(message);
            return Task.FromResult<object>(null);
        }
    }
}
