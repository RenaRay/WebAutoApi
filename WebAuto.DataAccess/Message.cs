using System;
using System.Collections.Generic;

namespace WebAuto.DataAccess
{
    public class Message
    {
        public int Id { get; set; }

        public int FromUserId { get; set; }

        public string Text { get; set; }

        public string ToPlate {get; set;}

        public int? ToUserId { get; set; }

        public List<int> Icons { get; set; }

        public bool IsRead { get; set; }

        public bool IsLiked { get; set; }

        public DateTime Sent { get; set; }
    }
}
