using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte IsAdmin { get; set; }
        public int NumUploads { get; set; }
        public string Password { get; set; }
        public double Rating { get; set; }
    }
}
