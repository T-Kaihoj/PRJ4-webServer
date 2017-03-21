using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVC.ViewModels;
using MVC.Models.Userlogic;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC.Controllers
{
    public class CreateBetController : Controller
    {
        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult post(BetPageViewModel viewModel)
        {
            Debug.WriteLine("Create bet" + viewModel.Title + " ");
            long LobbyID = 2; // HARDCODE
            if (!TryValidateModel(viewModel))
            {
                return View("Index", viewModel);
            }
            */

            var bet = new Bet(viewModel.Title, viewModel.Description, LobbyID, viewModel.Judge.Username, viewModel.StartDate, viewModel.EndDate);
           
            bet.Persist();


            /*if (objLobby.Name == null || objLobby.Description== null )
            {
                return Redirect("/404");
            }*/

            //Send return to home page 
            //return Redirect($"/CreateBet/Show/{bet.BetID}");
            return Redirect($"/BetPage/Show/0");
        }
    }
}
