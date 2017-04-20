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

        public BetController(IFactory factory , IUserContext userContext)

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

                // Check for the existence of the bet.
                if (bet == null)
                {
                    return Redirect("/");
                }

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
                LobbyId = id
            };

            return View("Create", viewModel);
        }

        // POST: /<controller>/Create
        [HttpPost]
        public ActionResult Create(CreateBetViewModel viewModel)
        {
            if (!TryValidateModel(viewModel))
            {
                return View("Create", viewModel);
            }

            using (var myWork = _factory.GetUOF())
            {
                // Create the bet.
                var bet = new Bet()
                {
                    BuyIn = viewModel.BuyInDecimal,
                    Description = viewModel.Description,
                    Name = viewModel.Title,
                    Owner = myWork.User.Get(_userContext.Identity.Name),
                    Judge = myWork.User.Get(viewModel.Judge),
                    StartDate = viewModel.StartDateTime,
                    StopDate = viewModel.StopDateTime
                };

                // Ensure both owner and judge was found.
                if (bet.Judge == null)
                {
                    ModelState.AddModelError("Judge", Resources.Bet.ErrorJudgeDoesntExist);

                    return View("Create", viewModel);
                }

                if (bet.Owner == null)
                {
                    // We should never be able to get to this line.
                    throw new Exception("Owner is not logged in");
                }

                var outcome1 = new Outcome()
                {
                    Name = viewModel.Outcome1,
                    Description = viewModel.Outcome1
                };
                var outcome2 = new Outcome()
                {
                    Name = viewModel.Outcome2,
                    Description = viewModel.Outcome2
                };
                bet.Outcomes.Add(outcome1);
                bet.Outcomes.Add(outcome2);

                myWork.Bet.Add(bet);

                // Get the lobby.
                var lobby = myWork.Lobby.Get(viewModel.LobbyId);
                lobby.Bets.Add(bet);
                
                myWork.Complete();

                // Redirect to the bet page.
                return Redirect($"/Bet/Join/{bet.BetId}");
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
                bet.JoinBet(user, outcome);

                myWork.Complete();

                return Redirect($"/Bet/Show/{bet.BetId}");
            }
        }

        // GET: /<controller>/Conclude/<id>
        public ActionResult Conclude(long id)
        {
            using (var myWork = _factory.GetUOF())
            {
                //throw new NotImplementedException();
                //TODO BetController JoinViewModel NullReference

                var user = myWork.User.Get(User.Identity.Name);
                var Bet = myWork.Bet.Get(id);
                //bool betConclude = myWork.Bet.Get(id);
                myWork.Complete();

                var model = new ConcludeViewModel();
                model.setup(Bet);

                if (_userContext.Identity.Name == Bet.Judge.Username)
                    return View(model);
            }

            return Redirect("/");
        }

        // POST: /<controller>/Conclude/
        [HttpPost]
        public ActionResult Conclude( ConcludeViewModel Model)
        {
            
            using (var myWork = _factory.GetUOF())
            {
                //throw new NotImplementedException();
                //TODO BetController JoinViewModel NullReference
                var bet = myWork.Outcome.Get(Model.SelectedOutcome).bet;
                var user = myWork.User.Get(User.Identity.Name);

                 bool betConclude = bet.ConcludeBet(user, myWork.Outcome.Get(Model.SelectedOutcome));
                myWork.Complete();
                if (betConclude)
                return Redirect("/");
            }

            return Redirect("/");
        }

        // GET: /<controller>/Remove/<Lobby>/<Bet>
        [HttpGet]
        public ActionResult Remove(long Lobby, long Bet)
        {
            using (var myWork = _factory.GetUOF())
            {
                var myBet = myWork.Bet.Get(Bet);
            
                if (_userContext.Identity.Name == myBet.Owner.Username && myBet.StartDate > DateTime.Now)
                {
                    myWork.Bet.Remove(myBet);
                    myWork.Complete();
                }
                string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
                return Redirect($"{baseUrl}/Lobby/Show/{Lobby}");
            }
        }
    }
}
