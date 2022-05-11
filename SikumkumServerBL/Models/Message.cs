using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class Message
    {
        public int MessageId { get; set; }
        public int FileId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string TheMessage { get; set; }
        public DateTime Date { get; set; }

        public virtual SikumFile File { get; set; }
        public virtual User User { get; set; }
    }
}
