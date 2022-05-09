using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class User
    {
        public User()
        {
            Messages = new List<Message>();
            Ratings = new List<Rating>();
            SikumFiles = new List<SikumFile>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public int NumUploads { get; set; }
        public string Password { get; set; }
        public double UserRating { get; set; }

        public virtual List<Message> Messages { get; set; }
        public virtual List<Rating> Ratings { get; set; }
        public virtual List<SikumFile> SikumFiles { get; set; }
    }
}
