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

        public async Task<List<SikumFileDTO>> GetChosenFiles(bool getSummary, bool getEssay, bool getPractice, string subjectName, int yearID, string headlineSearch) //Add lazy loading to remove includes.
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
                    if (file.Approved == true && file.YearId == yearID && DoesContainHeadline(headlineSearch, file.Headline) ) //Checks that the file is approved and year is correct.
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
        private bool DoesContainHeadline(string headlineSearch, string headline) //returns true if headline contains the headline search, or if user didn't search for anything.
        {
            if (headlineSearch == "" || headlineSearch == null) //If user didn't search for anything in particular.
                return true;

            if (headline.Contains(headlineSearch))
                return true;

            return false;

        }

        public async Task<List<SikumFileDTO>> GetUserFiles(int userID, int isApproved ) //Returns the files of the user.
        {
            
            
            try
            {
                User realUser = new User();
                foreach (User u in this.Users.Include("SikumFiles.Year").Include("SikumFiles.Type").Include("SikumFiles.Subject"))
                {
                    if (u.UserId == userID)
                    {
                        realUser = u;
                        break;
                    }
                }
                if (realUser.UserId != userID) //Didn't find the right user. 
                    return null;

                List<SikumFile> userFiles = new List<SikumFile>();
                foreach (var sikum in realUser.SikumFiles)
                {
                    if (isApproved == 0 && sikum.Approved == false) //If isApproved = 0, get files that are not approved. 
                    {
                        userFiles.Add(sikum);
                    }
                    if (isApproved == 1 && sikum.Approved == true)//If isApproved = 1, gett files that are approved.
                    {
                        userFiles.Add(sikum);
                    }
                }

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

        public async Task<User> ChangeUserPassword(UserDTO newUserPass) //Changes users password.
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
                SikumFile uploadFile = new SikumFile //Constructor of SikumFike. Could be changed to normal constructor if I ever need to.
                {
                    //Creates new lists of Messages and Ratings.
                    Messages = new List<Message>(), 
                    Ratings = new List<Rating>(),

                    UserId = realUser.UserId,
                    Headline = fileDto.Headline,
                    Approved = false, //to approve upload to app.
                    Disapproved = false, //Disapproved is when the admin disapproves but lets the user know why before deleting the sikum.
                    Url = $"{fileDto.Username}-{fileDto.Headline}-", //Base URL for images, after the second - the number of the file appears. Example: yotam-fakeheadline-1. yotam-fakeheadline-2.
                    TextDesc = fileDto.TextDesc,
                    TypeId = fileDto.TypeID,
                    SubjectId = fileDto.SubjectID,
                    YearId = fileDto.YearID,
                    FileRating = 0.00, //Sets it as zero before it is rated.                    
                    NumOfFiles = fileDto.NumOfFiles, //Num of files (Images/Pdf's etc) uploaded to server.
                    HasImage = fileDto.HasImage,
                    HasPdf = fileDto.HasPdf,
                    PdfFileName = fileDto.PdfFileName
                    
                };

                if(uploadFile == null) //upload failed.
                    return false;


                this.SikumFiles.Add(uploadFile);
                realUser.NumUploads++; //Adds user's sikumfile upload num. Equals to User's sikumfiles count.


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

        public async Task<bool> RejectUpload(SikumFileDTO sikum) //Rejects an upload.
        {
            try
            {
                SikumFile file = this.SikumFiles.Find(sikum.FileId);
                file.Disapproved = true;
                this.SikumFiles.Update(file);
                this.SaveChanges();
                return true;
            }
            catch //Error occured. 
            {
                return false;
            }
        }
        public async Task<bool> TryDeleteSikumFile(SikumFileDTO sikum)
        {
            try
            {
                SikumFile realFile = this.SikumFiles.Find(sikum.FileId);

                if (realFile == null)
                    return false;

                this.SikumFiles.Remove(realFile);
                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateSikum(SikumFileDTO sikum)//Might need approval of changes, but again, kinda pointless, and just more work for Admins and me. 
            //Function might not be needed.
        {
            try
            {
                SikumFile realFile = this.SikumFiles.Find(sikum.FileId);

                if (realFile == null)
                    return false;

                realFile.TextDesc = sikum.TextDesc; // Some more changes can be made, but there is really no point in changing anything but the description.


                this.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<double> AddRating(RatingDTO addRating) //Needs to have a way to save which users already rated. Meaning database changes are needed. Work in progress.
        {
            const int FAILED = -1;//Negative number to indicate that operation failed.
            try
            {
                double returnRating = FAILED; //Setting it to failed unless proven otherwise.

                User realUser = await this.Users.FindAsync(addRating.UserId);
                SikumFile realFile = await this.SikumFiles.Include("Ratings").FirstOrDefaultAsync(sikum => sikum.FileId == addRating.FileId);
                Rating theRating = new Rating(addRating);


                if (ValidateRating(realFile, theRating, realUser)) //Validates that the rating is okay, user didn't already rate the sikum.
                    return FAILED;

                realFile.Ratings.Add(theRating);
                returnRating = CalculateTotalRating(realFile);
                realFile.FileRating = returnRating;
                realUser.Ratings.Add(theRating);

                this.SaveChanges();


                return returnRating;
            }
            catch
            {
                return FAILED;
            }
        }
        private bool ValidateRating(SikumFile file, Rating rating, User user)
        {
            if (user == null)
                return false;
            if (file == null)
                return false;
            if (rating == null)
                return false;

            for (int i = 0; i < file.Ratings.Count; i++)
            {
                if (file.Ratings[i].UserId == rating.UserId)
                    return false;
            }

            return true;
        }
        private double CalculateTotalRating(SikumFile file)
        {
            double totalRating = 0;

            for (int i = 0; i < file.Ratings.Count; i++)
            {
                totalRating += file.Ratings[i].RatingGiven;
            }
            return totalRating / file.Ratings.Count; //Returns average of ratings.
        }
    }

}
