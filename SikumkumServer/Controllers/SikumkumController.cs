using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SikumkumServerBL.Models;

namespace SikumkumServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SikumkumController : ControllerBase // I NEED TO ADD THE PORT BINDINGS // I NEED TO ADD THE PORT BINDINGS
    {
        DBSikumkumContext context;

        public SikumkumController(DBSikumkumContext context)
        {
            this.context = context;
        }
    }
}
