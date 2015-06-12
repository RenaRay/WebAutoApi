using System;

namespace WebAuto.Backend.Models
{
    public class InboxMessageModel
    {
        public int Id { get; set; }

        public DateTime Sent { get; set; }

        public string Text { get; set; }

        public bool IsLiked { get; set; }
    }
}