﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SikumkumServerBL.Models
{
    public partial class User //Add sikumfiles to thingy.
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public int NumUploads { get; set; }
        public string Password { get; set; }
        public double Rating { get; set; }

        public User()
        {

        }

        public User(string username, string email, string password)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;
            this.IsAdmin = false;
            this.NumUploads = 0;
            this.Rating = 0.00;
        }
    }
}
