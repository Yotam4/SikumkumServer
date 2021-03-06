using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SikumkumServerBL.Models;
using System.IO;
using SikumkumServerBL.DTO;

namespace SikumkumServer.Controllers
{
    [Route("SikumkumAPIController")]
    [ApiController]
    public class SikumkumController : ControllerBase
    {
        DBSikumkumContext context;

        public SikumkumController(DBSikumkumContext context)
        {
            this.context = context;


        }


        //[Route("Login")]
        //[HttpGet] //Change to post.
        //public async Task<UserDTO> Login([FromQuery] string username, [FromQuery] string pass)
        //{
        //    try
        //    {
        //        UserDTO user = await context.Login(username, pass);
        //        UserDTO userDTO = new UserDTO(user);
        //        if (user != null)
        //        {
        //            HttpContext.Session.SetObject("theUser", user);

        //            Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

        //            return user;
        //        }

        //        else
        //        {
        //            Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
        //            return null;
        //        }
        //    }

        //    catch
        //    {
        //        Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
        //        return null;
        //    }
        //}

        [Route("Login")]
        [HttpPost]
        public async Task<UserDTO> Login([FromBody] UserDTO userDTO)
        {
            try
            {
                User user = await context.Login(userDTO);

                if (user == null)
                    return null;

                UserDTO returnUser = new UserDTO(user); //Returns user DTO.

                if (returnUser != null)
                {
                    HttpContext.Session.SetObject("theUser", user); //Sets real user in session.

                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return returnUser;
                }

                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }
            }

            catch
            {
                return null;
            }
        }

        [Route("Logout")]
        [HttpPost]
        public async Task<bool> Logout([FromBody] UserDTO user)
        {
            try
            {
                User realUser = HttpContext.Session.GetObject<User>("theUser");

                if (realUser != null)
                {
                    HttpContext.Session.Remove("theUser");

                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                    return true;
                }

                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
                    return false;
                }
            }

            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.NotFound;
                return false;
            }
        }

        [Route("SignUp")]
        [HttpPost]
        public async Task<bool> SignUp([FromBody] UserDTO userDTO)
        {
            User signedUp = await context.SignUp(userDTO);

            if (signedUp != null)
            {
                HttpContext.Session.SetObject("theUser", signedUp);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return true;
            }
            //Maybe add status codes that tell user if username\email was taken
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }
        [Route("GetOpeningObject")]
        [HttpGet]
        public async Task<OpeningObject> GetSubjects()
        {
            List<SubjectDTO> subjects = await context.GetAllSubjects();
            List<FileTypeDTO> fileTypes = await context.GetAllFileTypes();
            List<StudyYearDTO> studyYears = await context.GetAllStudyYears();

            OpeningObject openingOb = new OpeningObject(subjects, fileTypes, studyYears);

            if (openingOb != null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                return openingOb;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.NoContent;

                return null;
            }
        }

        [Route("GetFiles")]
        [HttpGet]
        public async Task<List<SikumFileDTO>> GetFiles([FromQuery] bool getSummary, [FromQuery] bool getPractice, [FromQuery] bool getEssay, [FromQuery] string subjectName, [FromQuery] int yearID, [FromQuery] string headlineSearch)
        {
            try
            {
                List<SikumFileDTO> files = await context.GetChosenFiles(getSummary, getEssay, getPractice, subjectName, yearID, headlineSearch);

                if (files != null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                    return files;
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.NoContent;

                    return null;
                }
            }

            catch
            {
                return null;
            }
        }

        [Route("GetUserFiles")]
        [HttpGet]
        public async Task<List<SikumFileDTO>> GetUserFiles([FromQuery] int userID, [FromQuery] int isApproved)
        {
            try
            {
                List<SikumFileDTO> userFiles = await context.GetUserFiles(userID, isApproved);

                if (userFiles != null && userFiles.Count > 0)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                    return userFiles;
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.NoContent;

                    return null;
                }
            }

            catch
            {
                return null;
            }
        }



        [Route("ChangePassword")]
        [HttpPost]
        public async Task<bool> ChangePassword([FromBody] UserDTO newUserPass)
        {
            if (newUserPass == null)
                return false;

            User newUser = await context.ChangeUserPassword(newUserPass); //Should be awaitable?
            if (newUser != null)
            {
                HttpContext.Session.SetObject("theUser", newUser); //Set new user to the session.

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                return true;
            }

            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }


        [Route("UploadSikumFile")]
        [HttpPost]

        public async Task<bool> UploadSikumFile([FromBody] SikumFileDTO sikumFile)
        {
            try
            {
                if (sikumFile == null)
                    return false;

                User user = HttpContext.Session.GetObject<User>("theUser");

                bool uploaded = await context.TryUploadSikumFile(sikumFile, user);

                if (uploaded)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return true;
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return false;
                }
            }

            catch
            {
                return false;
            }
        }

        [Route("UploadImages")]
        [HttpPost]

        public async Task<IActionResult> UploadImages([FromForm] IFormFileCollection getFiles) //Uploads images to server.
        {
            IFormFileCollection files = (IFormFileCollection)Request.Form.Files; //For some unfathomable reason, getFiles is empty, and this is the only way I found to get the true files.
            User user = HttpContext.Session.GetObject<User>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (user != null)
            {
                if (files.Count <= 0)
                {
                    return BadRequest();
                }

                try
                {
                    long size = files.Sum(f => f.Length);

                    foreach (IFormFile file in files)
                    {
                        //Add find type and change directory of images or pdfs.

                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName + ".jpg");

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                    }
                    return Ok(new { count = files.Count, size });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest();
                }
            }
            return Forbid();
        }

        [Route("UploadPdfs")]
        [HttpPost]

        public async Task<IActionResult> UploadPdfs([FromForm] IFormFileCollection getFiles) //Uploads PDF files to server.
        {
            IFormFileCollection files = (IFormFileCollection)Request.Form.Files; //For some unfathomable reason, getFiles is empty, and this is the only way I found to get the true files.

            User user = HttpContext.Session.GetObject<User>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (user != null)
            {
                if (files.Count <= 0)
                {
                    return BadRequest();
                }

                try
                {
                    long size = files.Sum(f => f.Length);

                    foreach (IFormFile file in files)
                    {
                        //Add find type and change directory of images or pdfs.

                        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/pdfs", file.FileName + ".pdf");

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                    }
                    return Ok(new { count = files.Count, size });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest();
                }
            }
            return Forbid();
        }

        [Route("TryAcceptUpload")]
        [HttpPost]
        public async Task<bool> TryAcceptUpload([FromBody] SikumFileDTO sikum)
        {
            try
            {
                bool accepted = await context.AcceptUpload(sikum);
                if (!accepted) //If for some reason it didn't accept.
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return false;
                }
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return true;

            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return false;
            }
        }

        [Route("TryRejectUpload")]
        [HttpPost]
        public async Task<bool> TryRejectUpload([FromBody] SikumFileDTO sikum)
        {
            try
            {
                bool accepted = await context.RejectUpload(sikum);
                if (!accepted) //If for some reason it didn't accept.
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return false;
                }
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return true;

            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return false;
            }
        }

        [Route("GetPendingFiles")]
        [HttpGet]
        public async Task<List<SikumFileDTO>> GetPendingFiles()
        {
            try
            {
                List<SikumFileDTO> unappFiles = await context.GetPendingFiles(); //Gets pending files

                if (unappFiles != null && unappFiles.Count > 0) //If some are found.
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                    return unappFiles;
                }
                else //If none are found.
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.NoContent;

                    return null;
                }
            }

            catch
            {
                return null;
            }
        }

        [Route("DeleteSikumFile")]
        [HttpPost]
        public async Task<bool> DeleteSikumFile(SikumFileDTO sikum)
        {
            try
            {
                bool sikumDeleted = await context.TryDeleteSikumFile(sikum); //Deletes the sikum files from database.


                if (sikumDeleted == false) //Sikum was not deleted.
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return false;
                }

                //Continues to delete the other files associated with the sikumfile.
                const string IMAGES = "images";
                const string PDFS = "pdfs";
                string addEnding = ""; //Stays empty unless file is an image.
                string fileContains = ""; //Set what the file contains for settings correct path.

                if (sikum.HasImage)
                {
                    fileContains = IMAGES;
                    addEnding = ".jpg";
                }
                if (sikum.HasPdf)
                {
                    fileContains = PDFS;
                    addEnding = ".pdf";
                }

                for (int i = 1; i <= sikum.NumOfFiles; i++) //Deletes all correspanding images/pdfs.
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), ("wwwroot/" + fileContains), (sikum.Url + i + addEnding));
                    //File is a controller method, so we need to specify the system.io.
                    System.IO.File.Delete(path);

                }
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK; //Everything was deleted correctly.
                return true;
            }

            catch
            {
                return false;
            }
        }

        [Route("UpdateSikum")]
        [HttpPost]
        public async Task<bool> UpdateSikum([FromBody] SikumFileDTO sikum)
        {
            try
            {
                bool updated = await context.UpdateSikum(sikum);
                if (!updated) //If it did not update
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return false;
                }
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return true;

            }
            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return false;
            }
        }


        [Route("AddRating")]
        [HttpPost]
        public async Task<double> AddRating([FromBody] RatingDTO newRating)                                                                            
        {
            try
            {
                if (newRating == null)
                    return Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;

                double newTotalRating = await context.AddRating(newRating);

                if (newTotalRating >= 0.00)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return newTotalRating;
                }
                else
                {
                    return Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                }
            }

            catch
            {
                return Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            }
        }

        [Route("AddMessage")]
        [HttpPost]
        public async Task<bool> AddMessage([FromBody] MessageDTO newMessage)
        {
            try
            {
                if (newMessage == null)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return false;
                }

                bool messageAdded= await context.AddMessage(newMessage);

                if (messageAdded == true)
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    return true;
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                    return false;
                }
            }

            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return false;
            }
        }

        [Route("GetMessages")]
        [HttpGet]
        public async Task<List<MessageDTO>> GetMessages([FromQuery] int fileID)
        {
            try
            {
                List<MessageDTO> messages = await context.GetMessagesAsync(fileID);

                if (messages != null)
                {

                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                    return messages;
                }

                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                    return null;
                }
            }

            catch
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
    }

}
