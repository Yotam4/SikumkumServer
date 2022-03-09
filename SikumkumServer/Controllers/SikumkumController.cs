﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SikumkumServerBL.Models;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
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


        [Route("Login")]
        [HttpGet] //Change to post.
        public async Task<UserDTO> Login([FromQuery] string username, [FromQuery] string pass)
        {
            UserDTO user = await context.Login(username, pass);
            
            if(user != null)
            {
                HttpContext.Session.SetObject("theUser", user);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                return user; 
            }

            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("SignUp")]
        [HttpPost]
        public async Task<bool> SignUp([FromBody] UserDTO userDTO)
        {
            User signedUp = await context.SignUp(userDTO);

            if(signedUp != null)
            {
                HttpContext.Session.SetObject("theUser", signedUp);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                return true;
            }

            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return false;
            }
        }
        [Route("GetSubjects")]
        [HttpGet]
        public async Task<List<Subject>> GetSubjects()
        {
            List<Subject> subjects = context.GetAllSubjects();

            if(subjects != null)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                return subjects;
            }
            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.NoContent;

                return null;
            }
        }

        [Route("GetFiles")]
        [HttpGet]
        public async Task<List<SikumFile>> GetFiles([FromQuery] bool getSummary, [FromQuery] bool getPractice, [FromQuery] bool getEssay, [FromQuery] string subjectName)
        {
            List<SikumFile> files = context.GetChosenFiles(getSummary, getEssay, getPractice, subjectName);

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
        [Route ("ChangePassword")]
        [HttpPost]
        public async Task<bool> ChangePassword([FromBody] UserDTO newUserPass)
        {
            if (newUserPass == null)
                return false;
            User currentUser = HttpContext.Session.GetObject<User>("theUser"); //Might not work. Should though.

            User newUser = context.ChangeUserPassword(newUserPass, currentUser);
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

        [Route("UploadImage")]
        [HttpPost]

        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            User user = HttpContext.Session.GetObject<User>("theUser");
            //Check if user logged in and its ID is the same as the contact user ID
            if (user != null)
            {
                if (file == null)
                {
                    return BadRequest();
                }

                try
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", file.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }


                    return Ok(new { length = file.Length, name = file.FileName });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest();
                }
            }
            return Forbid();
        }
    }
}
