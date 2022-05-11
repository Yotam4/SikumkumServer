using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikumkumServerBL.DTO;

namespace SikumkumServerBL.Models
{
    partial class Message
    {
        public Message() { }

        public Message(MessageDTO msgDTO)
        {
            this.Date = msgDTO.Date;
            this.TheMessage = msgDTO.TheMessage;
            this.FileId = msgDTO.FileId;
            this.UserId = msgDTO.UserId;
            this.Username = msgDTO.Username;
        }

    }
}
