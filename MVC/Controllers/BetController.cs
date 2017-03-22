using System;
using System.Linq;
using System.Web.Mvc;
using MVC.Models.Userlogic;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class BetController : Controller
    {
        // GET: /<controller>/Join/<id>
        public ActionResult Join(long id)
        {
            throw new Exception("Not implemented");
        }

        // GET: /<controller>/Show/<id>
        public ActionResult Show(long id)
        {
            var bet = Bet.getBet(id);

            var betPage = new BetViewModel()
            {
                Description = bet.Description,
                EndDate = bet.EndDate,
                //Judge = "",
                //LobbyID = 0,
                //Users = new List<User>(),
                Title = bet.BetName,
                StartDate = bet.StartDate,
                Outcomes = bet.Outcomes.Select(o => o.Name).ToList(),
                MoneyPool = bet.Pot
            };

            /*

            betPage.Title = bet.BetName;
            betPage.Description = bet.Description;
            betPage.StartDate = bet.StartDate;
            betPage.EndDate = bet.EndDate;
            betPage.Judge = bet.Judge.Username;
            for (int i = 0; i < bet.Outcomes.Count(); i++)
            {
                betPage.Outcomes[i] = bet.Outcomes[i].Name;
            }*/

            return View(betPage);
        }

        // GET: /<controller>/Create/<id>
        [HttpGet]
        public ActionResult Create(long id)
        {
            var viewModel = new CreateBetViewModel()
            {
                LobbyID = id
            };

            return View(viewModel);
        }

        // POST: /<controller>/Create
        [HttpPost]
        public ActionResult Create(BetViewModel viewModel)
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
