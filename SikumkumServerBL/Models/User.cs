using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
            Ratings = new HashSet<Rating>();
            SikumFiles = new HashSet<SikumFile>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public int NumUploads { get; set; }
        public string Password { get; set; }
        public double UserRating { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<SikumFile> SikumFiles { get; set; }
    }
}
