using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVC.Models.Userlogic;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class LobbiesController : Controller
    {
        public ActionResult Index()
        {
            var LobbiesPage = new LobbiesViewModel();

            var lobby = Lobbies.getLobby(1);
            LobbiesPage.MemberOfLobbies = lobby.MemberOfLobbies;

            return View(LobbiesPage);
        }
    }
}
