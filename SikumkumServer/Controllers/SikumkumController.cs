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
    [Route("api/[controller]")]
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
        public User Login([FromQuery] string username, [FromQuery] string pass)
        {
            User user = context.Login(username, pass);
            
            if(user != null)
            {
                HttpContext.Session.SetObject("theUser", user);

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;

                return user; //MIGHT NEED TO BE DIFFERENT, CHECK IN CLASS.
            }

            else
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                return null;
            }
        }

        [Route("SignUp")]
        [HttpPost]
        public User SignUp([FromBody] string username, [FromBody] string email, [FromBody] string password)
        {
            User signedUp = context.SignUp(username, email, password);

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
