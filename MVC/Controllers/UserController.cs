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
        private IUserContext _context;

        public UserController(IFactory factory, IStore store, IUserContext context)
        {
            _store = store;
            _userManager = new UserManager<IdentityUser>(_store);
            _factory = factory;
            _context = context;
        }


        // GET: /User/
        [HttpGet]
        public ActionResult Index()
        {
            // Get the user from the identity.
            var userName = _context.Identity.Name;

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
            // Ensure the data is vaid.
            if (!TryValidateModel(viewModel))
            {
                return View("EditProfile", viewModel);
            }

            using (var myWork = _factory.GetUOF())
            {
                var user = myWork.User.Get(_context.Identity.Name);

                // user should NEVER be null, but we check anyway.
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                user.Email = viewModel.Email;
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;

                myWork.Complete();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            // Get the user from the identity.
            var userName = _context.Identity.Name;

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
            };

            return View("EditProfile", viewModel);
        }

        [HttpGet]
        public ActionResult WithdrawMoney()
        {
            // Get the logged in user
            var userName = _context.Identity.Name;

            // Lookup the user in the repository.
            var user = _factory.GetUOF().User.Get(userName);
            // user should NEVER be null, but we check anyway.
            if (user == null)
            {
                throw new Exception("You are not logged in");
            }

            // Populate the viewmodel.
            var viewModel = new WithdrawViewModel()
            {
                CurrentBalance = user.Balance,
            };

            return View("~/Views/Money/Withdraw.cshtml",viewModel);
        }

        [HttpGet]
        public ActionResult DepositMoney()
        {
            // Get the logged in user
            var userName = _context.Identity.Name;

            // Lookup the user in the repository.
            var user = _factory.GetUOF().User.Get(userName);
            // user should NEVER be null, but we check anyway.
            if (user == null)
            {
                throw new Exception("You are not logged in");
            }

            // Populate the viewmodel.
            var viewModel = new DepositViewModel()
            {
                CurrentBalance = user.Balance,
            };

            return View("~/Views/Money/Deposit.cshtml", viewModel);
        }


        [HttpPost]
        public ActionResult Withdraw(WithdrawViewModel model)
        {
            // Get the logged in user
            var userName = _context.Identity.Name;

            using (var myWork = _factory.GetUOF())
            {
                // Lookup the user in the repository.
                var user = myWork.User.Get(userName);
                // user should NEVER be null, but we check anyway.
                if (user == null)
                {
                    throw new Exception("You are not logged in");
                }
                //Justerer users balance
                user.WithdrawMoney(model.Withdraw);
                myWork.Complete();
            }
            return Redirect($"/Home/Index/");
        }

        [HttpPost]
        public ActionResult Deposit(DepositViewModel model)
        {
            // Get the logged in user
            var userName = _context.Identity.Name;

            using (var myWork = _factory.GetUOF())
            {
                // Lookup the user in the repository.
                var user = myWork.User.Get(userName);
            // user should NEVER be null, but we check anyway.
            if (user == null)
            {
                throw new Exception("You are not logged in");
            }
           
                //Justerer users balance
                user.DepositMoney(model.Deposit);
                myWork.Complete();
            }

            return Redirect($"/Home/Index/");
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
