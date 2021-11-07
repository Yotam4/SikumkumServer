﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikumkumServerBL.Models;

namespace SikumkumServerBL.DTO
{
    public class UserDTO
    {
        public UserDTO()
        {

        }
        public UserDTO(User u)
        {
            this.Username = u.Username;
            this.Email = u.Email;
            this.Password = u.Password;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
