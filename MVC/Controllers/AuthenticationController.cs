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
        private UserManager<IdentityUser, string> _userManager;
        private IAuthenticationManager _authenticationManager;
        private SignInManager<IdentityUser, string> _signInManager;

        public AuthenticationController(UserManager<IdentityUser, string> userManager, IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;

            _signInManager = new SignInManager<IdentityUser, string>(_userManager, _authenticationManager);
        }

        // POST
        [HttpPost]
        public ActionResult SignIn(string userName, string password)
        {
            if(string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return View("InvalidCredentials");
            }

            var result = _signInManager.PasswordSignIn(userName, password, true, false);

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
