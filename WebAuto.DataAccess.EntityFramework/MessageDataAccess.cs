using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WebAuto.DataAccess.EntityFramework
{
    public class MessageDataAccess : IMessageDataAccess
    {
        private static List<WebAuto.DataAccess.Message> _messages = new List<WebAuto.DataAccess.Message>();

        public Task CreateAsync(WebAuto.DataAccess.Message message)
        {
            _messages.Add(message);
            return Task.FromResult<object>(null);
        }
    }
}
