using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class UserController : Controller
    {
        // GET: /UserController/
        [HttpGet]
        public ActionResult Get()
        {
            // TODO: Get the user from the identity.
            var user = new UserViewModel();
            // TODO: Get the user in repository.

            return View("Profile", user);
        }

        // GET: /UserController/
        /*[HttpGet]
        public IActionResult Get(string userName)
        {
            // TODO: Get the user from the identity.
            var user = new UserViewModel();
            // TODO: Get the user in repository.

            return View("Profile", user);
        }*/

        // PATCH: /UserController/
        [HttpPatch]
        public ActionResult Patch(UserViewModel model)
        {
            // Validate the model.
            if (!TryValidateModel(model))
            {
                // Error, return to main page with the model.
                return View("Profile", model);
            }

            // TODO: Persist user in repository.

            return Redirect("~");
        }

        // POST: /UserController/CreateUser
        [HttpPost]
        public ActionResult CreateUser(UserViewModel model)
        {
            // Validate the model.
            if (!TryValidateModel(model))
            {
                // Error, return to main page with the model.
                return View("~/Views/Home/Index.cshtml", model);
            }
            

            // TODO: Check if username is in use.

            // TODO: Persist user in repository.
            // add new User to DB



            // TODO: Authentication
            




            model.UserName = "Tak -- Tak";

            return View("~/Views/Home/Index.cshtml",model);
        }
    }
}
