using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        // POST
        [HttpPost]
        public ActionResult Post(string userName, string password)
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
