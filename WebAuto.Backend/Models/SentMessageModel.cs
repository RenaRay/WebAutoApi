using System;

namespace WebAuto.Backend.Models
{
    public class SentMessageModel
    {
        public DateTime Sent { get; set; }

        public string Text { get; set; }

        public string To { get; set; }
    }
}