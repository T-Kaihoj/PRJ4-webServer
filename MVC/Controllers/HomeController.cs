using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new UserViewModel());
        }

        public PartialViewResult LoginBox()
        {
            return PartialView(new AuthenticationViewModel());
        }
    }
}
