using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class UserMessage
    {
        public int MessageId { get; set; }
        public int ChatBoxId { get; set; }
        public string Username { get; set; }
        public string TextMessage { get; set; }

        public virtual Chat ChatBox { get; set; }
    }
}
