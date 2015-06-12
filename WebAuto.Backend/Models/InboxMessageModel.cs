using System;

namespace WebAuto.Backend.Models
{
    public class InboxMessageModel
    {
        public DateTime Sent { get; set; }

        public string Text { get; set; }
    }
}