using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SikumkumServerBL.Models;
using System.IO;
using Microsoft.Extensions.DependencyInjection;


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
        public async Task<User> Login([FromQuery] string username, [FromQuery] string pass)
        {
            User user = await context.Login(username, pass);
            
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
        public User SignUp([FromBody] User userDTO)
        {
            User signedUp = context.SignUp(userDTO.Username, userDTO.Email, userDTO.Password);

            if(signedUp != null)
            {
                HttpContext.Session.SetObject("theUser", signedUp);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                return signedUp;
            }

            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }
    }
}
