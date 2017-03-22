using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common;
using Common.Models;
using DAL;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class BetController : Controller
    {
        private IFactory _factory;

        public BetController()
        {
            Setup();
        }

        public BetController(IFactory factory = null)
        {
            Setup(factory);
        }

        private void Setup(IFactory factory = null)
        {
            // If no factory passed, create a default factory.
            _factory = factory ?? new Factory();
        }

        // GET: /<controller>/Join/<id>
        public ActionResult Join(long id)
        {
            throw new Exception("Not implemented");
        }

        // GET: /<controller>/Show/<id>
        public ActionResult Show(long id)
        {
            using (var myWork = _factory.GetUOF())
            {
                var bet = myWork.Bet.Get(id);

                // TODO: Remove hardcode.
                List<Outcome> outcomes = new List<Outcome>();

                outcomes.Add(new Outcome() { Description = "Han taber sig" });
                outcomes.Add(new Outcome() { Description = "Han når det ikke" });

                var betPage = new BetViewModel()
                {
                    Description = bet.Description,
                    EndDate = bet.StopDate.ToString(),
                    //Judge = "",
                    //LobbyID = 0,
                    //Users = new List<User>(),
                    Title = bet.Name,
                    StartDate = bet.StartDate.ToString(),
                    Outcomes = outcomes.Select(o => o.Name).ToList(),
                    MoneyPool = bet.Pot
                };

                /*

                betPage.Title = bet.BetName;
                betPage.Description = bet.Description;
                betPage.StartDate = bet.StartDate;
                betPage.StopDate = bet.StopDate;
                betPage.Judge = bet.Judge.Username;
                for (int i = 0; i < bet.Outcomes.Count(); i++)
                {
                    betPage.Outcomes[i] = bet.Outcomes[i].Name;
                }*/

                return View(betPage);
            }
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
        public ActionResult Create(CreateBetViewModel viewModel)
        {
            // TODO: Fix validation.
            if (!TryValidateModel(viewModel))
            {
                return View(viewModel);
            }

            using (var myWork = _factory.GetUOF())
            {
                // Create the bet.
                var bet = new Bet()
                {
                    BuyIn = Decimal.Parse(viewModel.BuyIn),
                    Description = viewModel.Description,
                    Name = viewModel.Title,
                    StartDate = DateTime.Parse(viewModel.StartDate),
                    StopDate = DateTime.Parse(viewModel.StopDate)
                };

                myWork.Bet.Add(bet);

                // Get the lobby.
                var lobby = myWork.Lobby.Get(viewModel.LobbyID);
                lobby.Bets.Add(bet);
                
                myWork.Complete();

                // Redirect to the bet page.
                return Redirect($"/Bet/Show/{bet.BetId}");
            }
        }
    }
}
