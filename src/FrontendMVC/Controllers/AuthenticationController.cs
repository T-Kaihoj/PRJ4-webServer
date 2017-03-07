using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FrontendMVC.Controllers
{
    public class AuthenticationController : Controller
    {
        // POST
        [HttpPost]
        public IActionResult Post([FromForm]string userName, [FromForm]string password)
        {
            if(string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return View("InvalidCredentials");
            }

            // Check the data.
            if (userName != password)
            {
                return View("InvalidCredentials");
            }

            // Login the user.

            return View("ValidCredentials");
        }
    }
}
