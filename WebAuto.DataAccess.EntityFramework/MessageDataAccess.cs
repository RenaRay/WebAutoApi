using System;
using System.Linq;
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

        public Task<int> GetUnreadCount(int userId)
        {
            var count = _messages
                .Where(m => m.ToUserId == userId && !m.IsRead)
                .Count();
            return Task.FromResult(count);
        }
    }
}
