using Microsoft.AspNetCore.Http;
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
    public class SikumkumController : ControllerBase // I NEED TO ADD THE PORT BINDINGS
    {
        DBSikumkumContext context;

        public SikumkumController(DBSikumkumContext context)
        {
            this.context = context;
            
            
        }


        [Route("Login")]
        [HttpGet]
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
        [HttpPost]
        public async Task<List<SikumFile>> GetFiles(bool getSummary, bool getPractice, bool getEssay)
        {
            List<SikumFile> files = context.GetChosenFiles(getSummary, getEssay, getPractice);

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
    }
}
