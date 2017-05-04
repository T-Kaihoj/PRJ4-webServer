using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            return View("~/Views/Shared/FileNotFound.cshtml");
        }

        public ActionResult NotPermitted()
        {
            return View("~/Views/Shared/UnauthorizedAccess.cshtml");
        }

        public ActionResult Index()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}