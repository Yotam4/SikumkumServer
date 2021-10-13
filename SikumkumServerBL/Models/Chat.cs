using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class Chat
    {
        public Chat()
        {
            SikumFiles = new HashSet<SikumFile>();
        }

        public int ChatBoxId { get; set; }
        public string ChatTitle { get; set; }
        public string ChatDesc { get; set; }

        public virtual UserMessage UserMessage { get; set; }
        public virtual ICollection<SikumFile> SikumFiles { get; set; }
    }
}
