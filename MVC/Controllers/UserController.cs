using System;
using System.Web.Mvc;
using Common;
using Common.Exceptions;
using Common.Models;
using Microsoft.AspNet.Identity;
using MVC.Identity;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class UserController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(IFactory factory, IUserContext userContext, IStore store)
            : base(factory, userContext)
        {
            _userManager = new UserManager<IdentityUser>(store);
        }
        
        #region Create

        // POST: /User/Create/
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
                if (myWork.User.GetByEmail(model.Email) != null)
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

        // GET: /User/Deposit/
        [Authorize]
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

        // POST: /User/Deposit/
        [Authorize]
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
                // Ensure we repopulate the balance field.
                using (var myWork = GetUOF)
                {
                    var user = myWork.User.Get(GetUserName);
                    model.CurrentBalance = user.Balance;
                }

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

        #region EditProfile

        // GET: /User/EditProfile/
        [Authorize]
        [HttpGet]
        public ActionResult EditProfile()
        {
            // Get the user from the identity.
            var userName = GetUserName;

            // Lookup the user in the repository.
            var user = GetUOF.User.Get(userName);

            // Populate the viewmodel.
            var viewModel = new EditProfileViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return View("EditProfile", viewModel);
        }

        // POST: /User/EditProfile/
        [Authorize]
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

                // Is the email in use?
                if (myWork.User.GetByEmail(viewModel.Email) != null)
                {
                    ModelState.AddModelError("Email", Resources.User.ErrorEmailInUse);
                    return View("EditProfile", viewModel);
                }

                user.Email = viewModel.Email;
                user.FirstName = viewModel.FirstName;
                user.LastName = viewModel.LastName;

                myWork.Complete();
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region Index

        // GET: /User/
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            // Is the user logged in?
            if (!IsAuthenticated)
            {
                return HttpUnauthorized();
            }

            // Get the user from the identity.
            var userName = GetUserName;

            // Lookup the user in the repository.
            var user = GetUOF.User.Get(userName);

            // Populate the viewmodel.
            var viewModel = new UserProfileViewModel()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Username,
                Balance = user.Balance,
                Friendlist = user.Friendlist
            };

            return View("Profile", viewModel);
        }

        #endregion

        #region Withdraw

        // GET: /User/Withdraw/
        [Authorize]
        [HttpGet]
        public ActionResult Withdraw()
        {
            // Get the logged in user
            var userName = GetUserName;

            // Lookup the user in the repository.
            var user = GetUOF.User.Get(userName);

            // Populate the viewmodel.
            var model = new WithdrawViewModel()
            {
                CurrentBalance = user.Balance,
            };

            return View("~/Views/Money/Withdraw.cshtml", model);
        }

        // POST: /User/Withdraw/
        [Authorize]
        [HttpPost]
        public ActionResult Withdraw(WithdrawViewModel model)
        {
            // Get the logged in user
            var userName = GetUserName;

            using (var myWork = GetUOF)
            {
                // Lookup the user in the repository.
                var user = myWork.User.Get(userName);
                
                // Alter user balance.
                try
                {
                    user.WithdrawMoney(model.Withdraw);
                    myWork.Complete();
                }
                catch (NotEnoughFundsException)
                {
                    ModelState.AddModelError("Withdraw", Resources.User.ErrorNotEnoughFunds);
                }
                catch (NegativeWithdrawException)
                {
                    ModelState.AddModelError("Withdraw", Resources.User.ErrorNegativeWithdraw);
                }

                if (!ModelState.IsValid)
                {
                    // Ensure we repopulate the balance field.
                    model.CurrentBalance = user.Balance;

                    return View("~/Views/Money/Withdraw.cshtml", model);
                }
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}
