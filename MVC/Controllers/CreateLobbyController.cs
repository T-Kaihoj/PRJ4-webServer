using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVC.Models.Userlogic;
using MVC.ViewModels;



// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC.Controllers
{
    public class CreateLobbyController : Controller
    {
        // GET: /<controller>/
        public ActionResult Index()
        {
            return View(new CreateLobbyViewModel());
        }

        [HttpPost]
        public ActionResult post(CreateLobbyViewModel viewModel)
        {
            Debug.WriteLine("Create lobby" + viewModel.Name + " ");

            if (!TryValidateModel(viewModel))
            {
                return View("Index", viewModel);
            }

            var lobby = new Lobby(viewModel.Name);
            lobby.Description = viewModel.Description;
            

            lobby.Persist();

            

            /*if (objLobby.Name == null || objLobby.Description== null )
            {
                return Redirect("/404");
            }*/

            //Send return to home page 
            return Redirect($"/Lobby/Show/{lobby.LobbyID}") ;

        }

    }
}
