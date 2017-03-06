using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FrontendMVC.Controllers
{
    public class CreateLobbyController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult post(Models.Model.Lobby objLobby)
        {
            Debug.WriteLine("Create lobby" + objLobby.Name + " ");
            


            //Send return to home page 
            return Redirect("/") ;

        }

    }
}
