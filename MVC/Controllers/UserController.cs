using System;
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

        public UserController(IFactory factory = null, IStore store = null)
        {
            if (factory == null)
            {
                factory = new Factory();
                throw new Exception("No factory");
            }

            if (store == null)
            {
                store = new Store(factory);
            }

            _store = store;
            _userManager = new UserManager<IdentityUser>(_store);
            _factory = factory;
        }


        // GET: /User/
        [HttpGet]
        public ActionResult Index()
        {
            // Get the user from the identity.
            var userName = User.Identity.Name;

            // Lookup the user in the repository.
            var user = _factory.GetUOF().User.Get(userName);

            // user should NEVER be null, but we check anyway.
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Populate the viewmodel.
            var viewModel = new UserProfileViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Username
            };

            return View("Profile", viewModel);
        }

        [HttpPost]
        public ActionResult EditProfile(EditProfileViewModel viewModel)
        {
            using (var myWork = _factory.GetUOF())
            {
                var user = myWork.User.Get(User.Identity.Name);

                user.Email = viewModel.Email;
                user.Username = viewModel.UserName;
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;

                // TODO: Update the user credentials in the authentication system.

                myWork.Complete();

            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            // Get the user from the identity.
            var userName = User.Identity.Name;

            // Lookup the user in the repository.
            var user = _factory.GetUOF().User.Get(userName);

            // user should NEVER be null, but we check anyway.
            if (user == null)
            {
                throw new Exception("User not found");
            }

            // Populate the viewmodel.
            var viewModel = new EditProfileViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Username
            };

            return View(viewModel);
        }

        // POST: /User/Create
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

            return View("~/Views/Home/Index.cshtml", model);
        }
    }
}
