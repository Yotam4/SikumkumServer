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

        public async Task<UserDTO> Login(string username, string password)
        {
            try
            {

                User loginUser = this.Users.Single(u => (u.Username == username && u.Password == password)); //There could be a better option than single. Research when you're not lazy.S
                UserDTO returnUser = new UserDTO(loginUser);
                return returnUser;
            }

            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<User> SignUp(UserDTO user)
        {
            try
            {
                User isDuplicate = this.Users.FirstOrDefault(u => (u.Username == user.Username || u.Email == user.Email)); //Checks if the Username or Email already exists.
                if (isDuplicate != null)
                {
                    if (user.Username == isDuplicate.Username)
                        throw new Exception("This Username is already in use. Please pick a new one.");

                    if (user.Email == isDuplicate.Email)
                        throw new Exception("This Email is already in use. Please pick a new one.");
                }

                User addUser = new User(user.Username, user.Email, user.Password);
                if (addUser == null) //If the user was not created.
                {
                    throw new Exception("User values are incorrect, could not create new user."); //Might need change.
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
        public List<Subject> GetAllSubjects() //Ask about Async.
        {
            try
            {
                List<Subject> subjects = new List<Subject>();

                foreach (Subject s in this.Subjects)
                {
                    subjects.Add(s);
                }

                if (subjects.Count == 0)
                    return null;
                else
                    return subjects;
            }

            catch (Exception e)
            {
                return null;
            }
        }

        public List<SikumFile> GetChosenFiles(bool getSummary, bool getEssay, bool getPractice, string subjectName)
        {
            try
            {
                List<SikumFile> files = new List<SikumFile>();

                string nameSummary = "#"; //Trash value that will never return true.
                string nameEssay = "#";
                string namePractice = "#";

                if (getSummary) //If true, add correct value to search for.
                    nameSummary = "Summary";
                if (getEssay)
                    nameSummary = "Essay";
                if (getPractice)
                    nameSummary = "Practice";

                var getCorrectFiles =
                from file in this.SikumFiles
                where  file.Subject.SubjectName == subjectName && (file.Type.TypeName == nameSummary || file.Type.TypeName == nameEssay || file.Type.TypeName == namePractice)
                select file;

                files = getCorrectFiles.ToList();


                if (files.Count == 0) //If there is nothing in the list.
                    return null;
                else
                    return files;
            }

            catch (Exception e)
            {
                return null;
            }
        }

        public User ChangeUserPassword(UserDTO newUserPass, User oldUser)
        {
            try
            {
                if (oldUser != null) //If found, change it's password.
                {
                    oldUser.Password = newUserPass.Password;
                    this.SaveChanges();
                    return oldUser;
                }
                else //if not, return null to make sure the request didnt work.
                    return null;
            }
            catch
            {
                return null;
            }
    }
}
