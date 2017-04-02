using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common;
using Common.Models;
using MVC.Identity;
using MVC.ViewModels;

namespace MVC.Controllers
{
    [Authorize]
    public class BetController : Controller
    {
        private readonly IFactory _factory;
        private readonly IUserContext _userContext;

        public BetController(IFactory factory, IUserContext userContext)
        {
            _factory = factory;
            _userContext = userContext;
        }
        
        // GET: /<controller>/Show/<id>
        public ActionResult Show(long id)
        {
            using (var myWork = _factory.GetUOF())
            {
                // Find the bet.
                var bet = myWork.Bet.Get(id);

                // TODO: Remove hardcode.
                List<Outcome> outcomes = new List<Outcome>();

                outcomes.Add(new Outcome() { Description = "Han taber sig" });
                outcomes.Add(new Outcome() { Description = "Han når det ikke" });

                // Create the viewmodel, and copy over data.
                var viewmodel = new ShowBetViewModel()
                {
                    Description = bet.Description,
                    Judge = bet.Judge?.Username,
                    Title = bet.Name,
                    StartDate = bet.StartDate.ToLongDateString(),
                    StopDate = bet.StopDate.ToLongDateString(),
                    MoneyPool = bet.Pot
                };

                // Extract users for each outcome.
                foreach (var outcome in bet.Outcomes)
                {
                    var users = new List<string>();

                    foreach (var user in outcome.Participants)
                    {
                        users.Add(user.Username);
                    }

                    // Create the nested viewmodel.
                    var vm = new ShowBetOutcomeViewModel()
                    {
                        Name = outcome.Name,
                        Participants = users
                    };

                    viewmodel.Outcomes.Add(vm);
                }

                return View("Show", viewmodel);
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
                    Judge = myWork.User.Get(viewModel.Judge),
                    StartDate = DateTime.Parse(viewModel.StartDate),
                    StopDate = DateTime.Parse(viewModel.StopDate)
                };
                /* TODO: Hardcoded indtil vi nemt kan hente et User-objekt fra databasen givet et Username! */
                
                var outcome1 = new Common.Models.Outcome()
                {
                    Name = viewModel.Outcome1,
                    Description = viewModel.Outcome1
                };
                var outcome2 = new Common.Models.Outcome()
                {
                    Name = viewModel.Outcome2,
                    Description = viewModel.Outcome2
                };
                bet.Outcomes.Add(outcome1);
                bet.Outcomes.Add(outcome2);

                myWork.Bet.Add(bet);

                // Get the lobby.
                var lobby = myWork.Lobby.Get(viewModel.LobbyID);
                lobby.Bets.Add(bet);
                
                myWork.Complete();

                // Redirect to the bet page.
                return Redirect($"/Bet/Show/{bet.BetId}");
            }
        }

        // GET: /<controller>/Join/<id>
        [HttpGet]
        public ActionResult Join(long id)
        {
            var viewModel = new JoinBetViewModel();
            using (var myWork = _factory.GetUOF())
            {
                var myBet = myWork.Bet.Get(id);
                foreach (var outcomes in myBet.Outcomes)
                {
                    var ovm = new OutcomeViewModel()
                    {
                        Id = outcomes.OutcomeId,
                        Name = outcomes.Name
                    };

                    viewModel.Outcomes.Add(ovm);
                }
                viewModel.Title = myBet.Name;
                viewModel.Description = myBet.Description;
                viewModel.MoneyPool = myBet.BuyIn;
                viewModel.Id = id;

                return View("Join", viewModel);
            }
        }

        // POST: /<controller>/Join/
        [HttpPost]
        public ActionResult Join(OutcomeViewModel model)
        {
            using (var myWork = _factory.GetUOF())
            {
                // Get the current user.
                var user = myWork.User.Get(_userContext.Identity.Name);

                // Find the outcome in the database.
                var outcome = myWork.Outcome.Get(model.Id);

                // Get the bet from the database, and join the selected outcome.
                var bet = myWork.Bet.Find(b => b.Outcomes.Any(o => o.OutcomeId.Equals(outcome.OutcomeId))).First();

                // Join the bet.
                bet.joinBet(user, outcome);

                myWork.Complete();

                return Redirect($"/Bet/Show/{bet.BetId}");
            }
        }
    }
}
