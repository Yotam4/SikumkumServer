using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikumkumServerBL.Models;

namespace SikumkumServerBL.DTO
{
    public class MessageDTO
    {
        public int MessageId { get; set; }
        public int FileId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string TheMessage { get; set; }
        public DateTime Date { get; set; }

        public MessageDTO() { }

        public MessageDTO(Message msg)
        {
            this.TheMessage = msg.TheMessage;
            this.MessageId = msg.MessageId;
            this.FileId = msg.FileId;
            this.UserId = msg.UserId;
            this.Username = msg.Username;
            this.Date = msg.Date;
        }
    }
}
