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

        public async Task<List<SikumFileDTO>> GetChosenFiles(bool getSummary, bool getEssay, bool getPractice, string subjectName, int yearID) //Add lazy loading to remove includes.
        {
            try
            {
                //Values of the names used in the database. Change here if database changes. DB CHANGE.
                const string SUMMARY_NAME = "סיכום";
                const string ESSAY_NAME = "מטלה";
                const string PRACTICE_NAME = "תרגול";


                List<SikumFile> subjectFiles;
                List<SikumFileDTO> returnFiles = new List<SikumFileDTO>(); //List of filesDTO to return to App.

                Subject chosenSubject = null;
                foreach (Subject sub in this.Subjects.Include("SikumFiles.Year").Include("SikumFiles.Type").Include("SikumFiles.User") )
                {
                    if (sub.SubjectName == subjectName)
                        chosenSubject = sub;
                    
                }
                subjectFiles = chosenSubject.SikumFiles.ToList(); //Gets all files of the current subject.


                foreach (SikumFile file in subjectFiles)
                {
                    if (file.Approved == true && file.YearId == yearID ) //Checks that the file is approved and year is correct.
                    {
                        if (getEssay && getPractice && getSummary) //If user chose to get all file types.                        
                            returnFiles.Add(new SikumFileDTO(file));

                        else //If user only chose some of the files types, check which ones, and if it's a match, add to file.
                        {
                            if (getSummary) //User wants summaries.
                            {
                                if (file.Type.TypeName == SUMMARY_NAME)
                                    returnFiles.Add(new SikumFileDTO(file));
                            }
                            if (getEssay) //User wants essays.
                            {
                                if (file.Type.TypeName == ESSAY_NAME)
                                    returnFiles.Add(new SikumFileDTO(file));
                            }
                            if (getPractice) //User wants practices.
                            {
                                if (file.Type.TypeName == PRACTICE_NAME)
                                    returnFiles.Add(new SikumFileDTO(file));
                            }
                        }
                    }

                }

                if (returnFiles.Count == 0) //If there are no files.
                    return null;                

                return returnFiles;
            }

            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<List<SikumFileDTO>> GetUserFiles(int userID) //Returns the files of the user.
        {

            try 
            {
                User realUser = this.Users.Single(u => u.UserId == userID);

                List<SikumFile> userFiles = realUser.SikumFiles.ToList();

                if (userFiles == null) //User has no files.
                    return null;

                List<SikumFileDTO> returnFiles = new List<SikumFileDTO>();
                foreach(SikumFile sikum in userFiles)
                {
                    returnFiles.Add(new SikumFileDTO(sikum));
                }



                return returnFiles;
            }

            catch
            {
                return null;
            }
        }

        public async Task<List<SikumFileDTO>> GetPendingFiles() //Gets all non approved files.
        {
            try
            {
                List<SikumFileDTO> returnFiles = new List<SikumFileDTO>();
                var sikumFiles = this.SikumFiles.Include("Year").Include("Type").Include("User");
                foreach (SikumFile sikum in sikumFiles) //Adds the non-approved sikumfiles.
                {
                    if (sikum.Approved == false)
                        returnFiles.Add(new SikumFileDTO(sikum));
                }
                return returnFiles;
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

                //Getting the actual values from the DB of user and subject.
                User realUser = this.Users.Find(user.UserId);
                int a = 4;
                //Creating sikumfile.
                SikumFile uploadFile = new SikumFile //Sikumfile does not contain the actual Type,Subject,Year, only the keys. Needs fix? Work in Progress.
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
                    NumRated = 0,
                    NumOfFiles = fileDto.NumOfFiles,
                    HasImage = fileDto.HasImage,
                    HasPdf = fileDto.HasPdf
                };

                if(uploadFile == null) //upload failed.
                    return false;

                realUser.NumUploads++; //Adds user's sikumfile upload num

                //Work in progress. DB needs changes.
                this.SikumFiles.Add(uploadFile);


                this.SaveChanges();

                return true;
            }

            catch 
            {
                return false;
            }
        }

        public async Task<bool> AcceptUpload(SikumFileDTO sikum)
        {
            try
            {
                SikumFile file = this.SikumFiles.Find(sikum.FileId);
                file.Approved = true;
                this.SikumFiles.Update(file);
                this.SaveChanges();
                return true;
            }
            catch //Error occured. 
            {
                return false;
            }
        }
        
    }
}
