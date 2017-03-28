using System.Web.Mvc;
using Common;
using Common.Models;
using DAL;
using Microsoft.AspNet.Identity;
using MVC.Identity;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class UserController : Controller
    {
        private IFactory _factory;
        private UserManager<IdentityUser> _userManager;
        private IStore _store;

        public UserController() : this(null, null)
        {
            
        }

        public UserController(IFactory factory = null, IStore store = null)
        {
            if (factory == null)
            {
                factory = new Factory();
            }

            if (store == null)
            {
                store = new Store(factory);
            }

            _store = store;
            _userManager = new UserManager<IdentityUser>(_store);
            _factory = factory;
        }


        // GET: /UserController/
        [HttpGet]
        public ActionResult Get()
        {
            // TODO: Get the user from the identity.
            var user = new CreateUserViewModel();
            // TODO: Get the user in repository.

            return View("Profile", user);
        }

        // POST: /UserController/Create
        [HttpPost]
        public ActionResult Create(CreateUserViewModel model)
        {
            // Validate the model.
            if (!TryValidateModel(model))
            {
                // Error, return to main page with the model.
                return View("~/Views/Home/Index.cshtml", model);
            }
            

            // TODO: Check if username is in use.

            // Persist user in repository.
            var user = new User()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.UserName
            };

            // Set the password.
            user.Hash = _userManager.PasswordHasher.HashPassword(model.Password1);

            using (var myWork = _factory.GetUOF())
            {
                myWork.User.Add(user);
                myWork.Complete();
            }
            
            // TODO: Authentication

            return View("~/Views/Home/Index.cshtml",model);
        }
    }
}
