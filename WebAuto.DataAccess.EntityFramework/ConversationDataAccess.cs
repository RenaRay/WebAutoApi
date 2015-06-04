using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAuto.DataAccess.EntityFramework
{
    public class ConversationDataAccess : IConversationDataAccess
    {
        public Task<Conversation> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Conversation>> FindByUserAsync(string user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateConversationsWithEmptyUser(string user, string plate)
        {
            return Task.FromResult<Object>(null);
        }

        public Task<Conversation> FindByMembersAsync(List<ConversationMember> members)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Conversation conversation)
        {
            throw new NotImplementedException();
        }

        public Task PostMessageAsync(string conversation, ConversationMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalUnreadCountAsync(string user)
        {
            return Task.FromResult(0);
        }

        public Task ReadMessages(string conversation, string user, int messageCount)
        {
            throw new NotImplementedException();
        }
    }
}
