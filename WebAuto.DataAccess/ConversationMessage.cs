using System;

namespace WebAuto.DataAccess
{
    public class ConversationMessage
    {
        public string Author { get; set; }

        public DateTime Posted { get; set; }

        public string Text { get; set; }
    }
}
