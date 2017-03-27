using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MVC.Identity;

namespace MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        // POST
        [HttpPost]
        public ActionResult SignIn(string userName, string password)
        {
            if(string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return View("InvalidCredentials");
            }

            var uManager = new UserManager<IdentityUser, string>(new Store(new Factory()));
            IAuthenticationManager aManager = HttpContext.Request.GetOwinContext().Authentication;
            var manager = new SignInManager<IdentityUser, string>(uManager, aManager);

            var result = manager.PasswordSignIn(userName, password, true, false);

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
