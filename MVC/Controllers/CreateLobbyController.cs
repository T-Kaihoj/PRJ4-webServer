using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVC.ViewModels;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC.Controllers
{
    public class CreateLobbyController : Controller
    {
        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult post(CreateLobbyViewModel objLobby)
        {
            Debug.WriteLine("Create lobby" + objLobby.Name + " ");

            if (objLobby.Name == null || objLobby.Description== null )
            {
                return Redirect("/404");
            }

            //Send return to home page 
            return Redirect("/") ;

        }

    }
}
