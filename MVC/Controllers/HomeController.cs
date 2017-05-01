using System.Web.Mvc;
using Common;
using MVC.Identity;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IFactory factory, IUserContext userContext)
            : base(factory, userContext)
        {
            
        }

        public ActionResult Index()
        {
            if (IsAuthenticated)
            {
               return UserHomepage();
            }

            return View("Index", new CreateUserViewModel());
        }

        public PartialViewResult LoginBox()
        {
            return PartialView("_LoginBox", new AuthenticationViewModel());
        }

        [HttpGet]
        public ActionResult UserHomepage()
        {
            // Get the logged in user
            var userName = GetUserName;

            // Lookup the user in the repository.

            var user = GetUOF.User.Get(userName);

            // Populate the viewmodel.
            var viewModel = new HomeViewModel()
            {
                Name = (user.FirstName + "   " + user.LastName),
                CurrentBalance = user.Balance,
                MemberOfLobbies = user.MemberOfLobbies,
                InvitedToLobbies = user.InvitedToLobbies,
                Bets = user.Bets,
                Friendlist = user.Friendlist,
                Username = user.Username,
                BetsJudged = user.BetsJudged
               
            };

            return View("IndexAuth", viewModel);
        }
    }
}
