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

        public AuthenticationController() : this(null, null)
        {
            
        }

        public AuthenticationController(UserManager<IdentityUser, string> userManager, IAuthenticationManager authenticationManager)
        {
            if (userManager == null)
            {
                userManager = new UserManager<IdentityUser, string>(new Store(new Factory()));
            }

            _userManager = userManager;
            _authenticationManager = authenticationManager;
        }

        // POST
        [HttpPost]
        public ActionResult SignIn(string userName, string password)
        {
            SignInManager<IdentityUser, string> signInManager;

            if (_authenticationManager == null)
            {
                signInManager = new SignInManager<IdentityUser, string>(_userManager, HttpContext.GetOwinContext().Authentication);
            }
            else
            {
                signInManager = new SignInManager<IdentityUser, string>(_userManager, _authenticationManager);
            }

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return View("InvalidCredentials");
            }

            var result = signInManager.PasswordSignIn(userName, password, true, false);

            switch (result)
            {
                case SignInStatus.Success:
                    return View("ValidCredentials");
            }

            return View("InvalidCredentials");
        }
    }
}
