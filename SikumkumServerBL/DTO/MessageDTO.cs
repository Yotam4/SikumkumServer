using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SikumkumServerBL.DTO
{
    public class MessageDTO
    {
        public int MessageId { get; set; }
        public int FileId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Message1 { get; set; }
        public DateTime Date { get; set; }

        public MessageDTO() { }
    }
}
