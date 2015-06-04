using System;
using System.Collections.Generic;

namespace WebAuto.Backend.Models
{
    public class ConversationModel
    {
        public List<ConversationMemberModel> Members { get; set; }

        public List<ConversationMessageModel> Messages { get; set; }

        public int UnreadCount { get; set; }
    }
}