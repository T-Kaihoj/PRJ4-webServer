using System;
using System.Web.Mvc;
using Common;
using Microsoft.AspNet.Identity;
using MVC.Identity;
using MVC.ViewModels;
using Common.Models;
using DAL;


namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private IUserContext _context;
        private IFactory _factory;
        private UserManager<IdentityUser> _userManager;
        private IStore _store;

        public HomeController(IUserContext context, IFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        public ActionResult Index()
        {
            if (_context.Identity.IsAuthenticated)
            {
               return UserHomepage();
            }

        return View("Index", new CreateUserViewModel());
          
        }

        public PartialViewResult LoginBox()
        {
            return PartialView(new AuthenticationViewModel());
        }

        [HttpGet]
        public ActionResult UserHomepage()
        {
            // Get the logged in user
            var userName = _context.Identity.Name;

            // Lookup the user in the repository.

            var user = _factory.GetUOF().User.Get(userName);     
            // user should NEVER be null, but we check anyway.
            if (user == null)
            {
                throw new Exception("You are not logged in");
            }

            // Populate the viewmodel.
            var viewModel = new HomeViewModel()
            {
                Name = (user.FirstName + user.LastName),
                CurrentBalance = user.Balance,
                MemberOfLobbies = user.MemberOfLobbies,
                InvitedToLobbies = user.InvitedToLobbies,
                Bets = user.Bets
            };

            return View("~/Views/Home/IndexAuth.cshtml", viewModel);
        }
    }
}
