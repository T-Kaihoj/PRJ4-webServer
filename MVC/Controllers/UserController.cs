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
    public class UserController : BaseController
    {
        private UserManager<IdentityUser> _userManager;
        private IStore _store;

        public UserController(IFactory factory, IUserContext userContext, IStore store)
            : base(factory, userContext)
        {
            _store = store;
            _userManager = new UserManager<IdentityUser>(_store);
        }

        // GET: /User/
        [HttpGet]
        public ActionResult Index()
        {
            // Get the user from the identity.
            var userName = GetUserName;

            // Lookup the user in the repository.
            var user = GetUOF.User.Get(userName);

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

            using (var myWork = GetUOF)
            {
                var user = myWork.User.Get(GetUserName);

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
            var userName = GetUserName;

            // Lookup the user in the repository.
            var user = GetUOF.User.Get(userName);

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
            var userName = GetUserName;

            // Lookup the user in the repository.
            var user = GetUOF.User.Get(userName);
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

        


        [HttpPost]
        public ActionResult Withdraw(WithdrawViewModel model)
        {
            // Get the logged in user
            var userName = GetUserName;

            using (var myWork = GetUOF)
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

        

        #region Create

        // POST: /User/Create
        [HttpPost]
        public ActionResult Create(CreateUserViewModel model)
        {
            // Validate the model.
            TryValidateModel(model);

            using (var myWork = GetUOF)
            {
                // Is the username already taken?
                if (myWork.User.Get(model.UserName) != null)
                {
                    ModelState.AddModelError("UserName", Resources.User.ErrorUserNameTaken);
                }

                // Is the email in use?
                if (myWork.User.GetByEmail(model.UserName) != null)
                {
                    ModelState.AddModelError("Email", Resources.User.ErrorEmailInUse);
                }
            }

            if (!ModelState.IsValid)
            {
                return View("~/Views/Home/Index.cshtml", model);
            }

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

            using (var myWork = GetUOF)
            {
                myWork.User.Add(user);
                myWork.Complete();
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Deposit

        [HttpGet]
        public ActionResult Deposit()
        {
            // Get the logged in user
            var userName = GetUserName;

            // Lookup the user in the repository.
            var user = GetUOF.User.Get(userName);

            // Populate the viewmodel.
            var model = new DepositViewModel()
            {
                CurrentBalance = user.Balance,
            };

            return View("~/Views/Money/Deposit.cshtml", model);
        }

        [HttpPost]
        public ActionResult Deposit(DepositViewModel model)
        {
            // Is the model valid?
            if (model.Deposit < 0m)
            {
                ModelState.AddModelError("Deposit", Resources.User.ErrorNegativeDeposit);
            }

            if (!ModelState.IsValid)
            {
                return View("~/Views/Money/Deposit.cshtml", model);
            }

            // Get the logged in user
            var userName = GetUserName;

            using (var myWork = GetUOF)
            {
                // Lookup the user in the repository.
                var user = myWork.User.Get(userName);

                //Justerer users balance
                user.DepositMoney(model.Deposit);
                myWork.Complete();
            }

            return RedirectToAction("Index");
        }

        #endregion
    }
}
