using System.Collections.Generic;

namespace WebAuto.DataAccess
{
    public class Conversation
    {
        public string Id { get; set; }

        public List<ConversationMember> Members { get; set; }

        public List<ConversationMessage> Messages { get; set; }
    }
}
