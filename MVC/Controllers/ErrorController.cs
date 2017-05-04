using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;

            return View("FileNotFound");
        }

        public ActionResult NotPermitted()
        {
            Response.StatusCode = 403;

            return View("UnauthorizedAccess");
        }

        public ActionResult Index()
        {
            Response.StatusCode = 500;

            return View("Error");
        }
    }
}