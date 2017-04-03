using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MVC.Identity;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<IdentityUser, string> _userManager;
        private readonly IAuthenticationManager _authenticationManager;

        public AuthenticationController(UserManager<IdentityUser, string> userManager, IAuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
            _userManager = userManager;
        }

        // POST
        [HttpPost]
        public ActionResult SignIn(string userName, string password)
        {
            var signInManager = GetSignInManager();

            // Convert to viewmodel.
            AuthenticationViewModel viewModel = new AuthenticationViewModel()
            {
                Password = password,
                UserName = userName
            };

            if (!TryValidateModel(viewModel))
            {
                return View("InvalidCredentials");
            }

            var result = signInManager.PasswordSignIn(viewModel.UserName, viewModel.Password, true, false);

            switch (result)
            {
                case SignInStatus.Success:
                    return Redirect("/");
            }

            return View("InvalidCredentials");
        }

        // GET /AuthenticationController/SignOut
        [HttpGet]
        public RedirectResult SignOut()
        {
            var signInManager = GetSignInManager();

            signInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return Redirect("/");
        }

        // The following is made to allow for testing, and to avoid injecting runtime information. As such, it is not included in our coverage analysis.
        [ExcludeFromCodeCoverage]
        private SignInManager<IdentityUser, string> GetSignInManager()
        {
            var auth = _authenticationManager as EmptyAuthenticationManager;

            if (auth != null)
            {
                return new SignInManager<IdentityUser, string>(_userManager, HttpContext.GetOwinContext().Authentication);
            }

            return new SignInManager<IdentityUser, string>(_userManager, _authenticationManager);
        }
    }
}
