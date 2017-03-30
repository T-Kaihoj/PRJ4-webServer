using System.Web.Mvc;
using MVC.Identity;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private IUserContext _context;

        public HomeController(IUserContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            if (_context.Identity.IsAuthenticated)
            {
                return View("IndexAuth");
            }

            return View("Index", new CreateUserViewModel());
        }

        public PartialViewResult LoginBox()
        {
            return PartialView(new AuthenticationViewModel());
        }
    }
}
