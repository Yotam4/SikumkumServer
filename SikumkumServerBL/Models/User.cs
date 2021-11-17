using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class User
    {
        public User()
        {

        }
        public User(string username, string email, string password)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.NumUploads = 0; //No uploads.
            this.Rating = 0.0; //No ratings
            this.IsAdmin = 0; //0 for not an admin (byte). 1 for admin.

        }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public byte IsAdmin { get; set; }
        public int NumUploads { get; set; }
        public string Password { get; set; }
        public double Rating { get; set; }
    }
}
