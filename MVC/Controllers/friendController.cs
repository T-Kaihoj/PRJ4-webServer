using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using MVC.Identity;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class friendController : BaseController
    {
        // GET: friend
        public friendController(IFactory factory, IUserContext userContext) : base(factory, userContext)
        {
        }

        public ActionResult Index()
        {
            return View( new FriendAddViewmodel());
        }

        [HttpPost]
        public ActionResult Index(FriendAddViewmodel model)
        {
            // getting you
            string yourname = User.Identity.Name;

            using (var myWork = GetUOF)
            {
                //getting you from database
                var you = myWork.User.Get(yourname);

                //Getting your friend
                var friend = myWork.User.Get(model.Username);


                //Returning different does not exist
                if (friend == null)
                {
                    ModelState.AddModelError("Username", "User does not exist");

                    return View(model);
                }

                //add to friends list    
                you.addFriend(friend);

                myWork.Complete();

            }
            return Redirect("/");
        }
    }
}