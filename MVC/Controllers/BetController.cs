using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using MVC.Models.Userlogic;

namespace MVC.Controllers
{
    public class BetController : Controller
    {
        // GET: /<controller>/Show/<id>
        public ActionResult Show(long id)
        {
            var betPage = new BetPageViewModel();

            var bet = Bet.getBet(id);


            betPage.Title = bet.BetName;

            //betPage.Title = bet.BetTitle;

            betPage.Description = bet.Description;
            betPage.StartDate = bet.StartDate;
            betPage.EndDate = bet.EndDate;
            betPage.Judge = bet.Judge.Username;
            for (int i = 0; i < bet.Outcomes.Count(); i++)
            {
                betPage.Outcomes[i] = bet.Outcomes[i].Name;
            }


            return View(betPage);
        }

        // GET: /<controller>/Create/<id>
        [HttpGet]
        public ActionResult Create(long id)
        {
            BetPageViewModel viewModel = new BetPageViewModel()
            {
                LobbyID = id
            };

            return View(viewModel);
        }

        // POST: /<controller>/Create
        [HttpPost]
        public ActionResult Create(BetPageViewModel viewModel)
        {
            /*if (!TryValidateModel(viewModel))
            {
                return View(viewModel);
            }*/

            // Create the bet.
            var bet = new Bet(viewModel.Title, viewModel.Description, viewModel.LobbyID, viewModel.Judge, viewModel.StartDate, viewModel.EndDate);
           
            bet.Persist();

            // Redirect to the bet page.
            return Redirect($"/Bet/Show/{bet.BetID}");
        }
    }
}
