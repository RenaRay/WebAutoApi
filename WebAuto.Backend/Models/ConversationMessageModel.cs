using System;

namespace WebAuto.Backend.Models
{
    public class ConversationMessageModel
    {
        public string Author { get; set; }

        public DateTime Posted { get; set; }

        public string Text { get; set; }
    }
}