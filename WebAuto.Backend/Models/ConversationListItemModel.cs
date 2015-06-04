using System;
using System.Collections.Generic;

namespace WebAuto.Backend.Models
{
    public class ConversationListItemModel
    {
        public string Id { get; set; }

        public List<ConversationMemberModel> Members { get; set; }

        public int UnreadCount { get; set; }
    }
}