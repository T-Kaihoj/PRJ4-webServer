using System.Web.Mvc;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new CreateUserViewModel());
        }

        public PartialViewResult LoginBox()
        {
            return PartialView(new AuthenticationViewModel());
        }
    }
}
