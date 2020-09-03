using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    static class Globals
    {
        public static bool authenticated = false;
    }

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private bool PerformLogin(Credential credential)
        {
            if (credential.Username == "admin" && credential.Password == "adminpass")
            {
                Globals.authenticated = true;
            }
            else
            {
                Globals.authenticated = false;
            }
            return Globals.authenticated;
        }

        // POST api/login
        [HttpPost]
        public ActionResult Login(Credential credential)
        {

            if (!PerformLogin(credential))
            {
                return Unauthorized();
            }
            return Ok();
        }

    }
}
