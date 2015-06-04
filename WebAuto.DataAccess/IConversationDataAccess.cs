using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAuto.DataAccess
{
    public interface IConversationDataAccess
    {
        Task<Conversation> FindByIdAsync(string id);

        Task<List<Conversation>> FindByUserAsync(string user);

        Task UpdateConversationsWithEmptyUser(string user, string plate);

        Task<Conversation> FindByMembersAsync(List<ConversationMember> members);
        
        Task CreateAsync(Conversation conversation);

        Task PostMessageAsync(string conversation, ConversationMessage message);

        Task<int> GetTotalUnreadCountAsync(string user);

        Task ReadMessages(string conversation, string user, int messageCount);
    }
}
