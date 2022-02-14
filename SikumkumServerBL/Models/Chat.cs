using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class Chat
    {
        public int ChatBoxId { get; set; }
        public string ChatTitle { get; set; }
        public string ChatDesc { get; set; }

        public virtual SikumFile SikumFile { get; set; }
        public virtual UserMessage UserMessage { get; set; }
    }
}
