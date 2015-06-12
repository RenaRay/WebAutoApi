using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAuto.DataAccess
{
    public interface IMessageDataAccess
    {
        Task CreateAsync(Message message);

        Task<int> GetUnreadCount(int userId);

        Task<List<Message>> GetInboxMessages(int userId);

        Task ReadInboxMessages(int userId);

        Task AddMessagesToUser(int userId);
    }
}
