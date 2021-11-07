using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikumkumServerBL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SikumkumServerBL.DTO;

namespace SikumkumServerBL.Models
{
    public partial class DBSikumkumContext : DbContext
    {

        public async Task<User> Login(string username, string password)
        {
            try
            {
               
                User loginUser = this.Users.Single(u => (u.Username == username && u.Password == password)); //There could be a better option than single. Research when you're not lazy.S

                return loginUser;
            }

            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<User> SignUp(UserDTO user)
        {
            try
            {
                User addUser = new User(user.Username, user.Email, user.Password);

                if (addUser == null) //If the user was not created.
                {
                    throw new Exception("User values are incorrect."); //Might need change.
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
