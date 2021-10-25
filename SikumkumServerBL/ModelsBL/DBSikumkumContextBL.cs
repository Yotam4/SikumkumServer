using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikumkumServerBL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SikumkumServerBL.Models
{
    public partial class DBSikumkumContext : DbContext
    {

        public User Login(string username, string password)
        {
            User loginUser = this.Users.Single(u => (u.Username == username && u.Password == password));

            return loginUser;
        }

        public User SignUp(string username, string email, string password)
        {
            try
            {
                User addUser = new User(username, email, password);

                if (addUser == null) //If the user was not created.
                {
                    throw new Exception("User values are incorrect.");
                }                

                this.Users.Add(addUser); //ADD THE THINGS THAT CHECKS IF IT WAS ADDED SUCCESSFULLY OR NOT.
                this.SaveChanges();

                return addUser;
            }

            catch
            {
                return null;
            }
        }
    }
}
