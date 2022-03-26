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
        public async Task<List<SubjectDTO>> GetAllSubjects() //Ask about Async.
        {
            try
            {
                List<SubjectDTO> subjects = new List<SubjectDTO>();

                foreach (Subject s in this.Subjects)
                {
                    SubjectDTO sd = new SubjectDTO(s);
                    subjects.Add(sd);
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

        public async Task<List<FileTypeDTO>> GetAllFileTypes()
        {
            try
            {
                List<FileTypeDTO> fileTypes = new List<FileTypeDTO>();

                foreach (FileType f in this.FileTypes)
                {
                    FileTypeDTO fDTO = new FileTypeDTO(f);
                    fileTypes.Add(fDTO);
                }

                if (fileTypes.Count == 0)
                    return null;
                else
                    return fileTypes;
            }

            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<StudyYearDTO>> GetAllStudyYears()
        {
            try
            {
                List<StudyYearDTO> studyYears = new List<StudyYearDTO>();

                foreach (StudyYear sy in this.StudyYears)
                {
                    StudyYearDTO syDTO = new StudyYearDTO(sy);
                    studyYears.Add(syDTO);
                }

                if (studyYears.Count == 0)
                    return null;
                else
                    return studyYears;
            }

            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<SikumFile>> GetChosenFiles(bool getSummary, bool getEssay, bool getPractice, string subjectName) //Change to work faster. Work in progress.
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

        public List<SikumFile> GetUserFiles(string username) //Returns the files that contain the same username.
        {

            try //Needs to be changed, Work in progress.
            {
                List<SikumFile> userFiles = new List<SikumFile>();


                //var getUserFiles = //Find the files that match the username.
                //from file in this.SikumFiles
                //where file.Username == username
                //select file;

                //userFiles = getUserFiles.ToList();

                if (userFiles == null) //User has no files.
                    return null;

                return userFiles;
            }

            catch
            {
                return null;
            }
        }

        public User ChangeUserPassword(UserDTO newUserPass) //Changes users password.
        {
            try
            {
                User theUser = this.Users.Single(u => u.Username == newUserPass.Username);
                if (theUser == null) //No user was found.
                    throw new Exception("User not found in DB");

                theUser.Password = newUserPass.Password;
                this.SaveChanges();

                return theUser;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> TryUploadSikumFile(SikumFileDTO fileDto, User user) 
        {
            try
            {
                if (fileDto == null || user == null)
                    return false;

                //Getting the actual values from the DB. work in progress.
                User realUser = this.Users.Single(u => u.UserId == user.UserId);

                //Creating sikumfile.
                SikumFile uploadFile = new SikumFile //Needs to be changed, work in progress.
                {
                    UserId = realUser.UserId,
                    Headline = fileDto.Headline,
                    Approved = false,
                    Url = $"{fileDto.Username}-{fileDto.Headline}-",
                    TextDesc = fileDto.TextDesc,
                    TypeId = fileDto.TypeID,
                    SubjectId = fileDto.SubjectID,
                    YearId = fileDto.YearID,
                    Rating = 0.00,
                    NumRated = 0                   
                };

                if(uploadFile == null) //upload failed.
                    return false;

                realUser.NumUploads++; //Adds user's sikumfile upload num
                this.SikumFiles.Add(uploadFile);
                this.SaveChanges();

                return true;
            }

            catch (Exception e)
            {
                string mess = e.Message;
                return false;
            }
        }


    }
}
