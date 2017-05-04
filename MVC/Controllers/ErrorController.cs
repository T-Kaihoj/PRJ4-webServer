using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;

            return View("~/Views/Shared/FileNotFound.cshtml");
        }

        public ActionResult NotPermitted()
        {
            Response.StatusCode = 403;

            return View("~/Views/Shared/UnauthorizedAccess.cshtml");
        }

        public ActionResult Index()
        {
            Response.StatusCode = 500;

            return View("~/Views/Shared/Error.cshtml");
        }
    }
}