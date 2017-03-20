using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LobbyLogic;
using MVC.ViewModels;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC.Controllers
{
    public class LobbyController : Controller
    {
        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show(int id)
        {
            var l = Lobby.Get(id);
            
            var viewModel = new LobbyViewModel();
            viewModel.ID = (int)l.LobbyID;
            viewModel.Name = "Hello Tobias";

            return View(viewModel);
        }
    }
}
