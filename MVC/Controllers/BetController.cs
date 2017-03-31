using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Common;
using Common.Models;
using DAL;
using MVC.Identity;
using MVC.ViewModels;

namespace MVC.Controllers
{
    [Authorize]
    public class BetController : Controller
    {
        private IFactory _factory;
        private IUserContext _userContext;

        public BetController(IFactory factory, IUserContext userContext)
        {
            _factory = factory;
            _userContext = userContext;
        }

        private void Setup(IFactory factory = null)
        {
            // If no factory passed, create a default factory.
            _factory = factory ?? new Factory();
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
                    Judge = bet.Judge?.Username,//bet.Judge.Username,
                    //LobbyID = 0,
                    //Users = new List<User>(),
                    Title = bet.Name,
                    StartDate = bet.StartDate.ToString(),
                    MoneyPool = bet.Pot
                };
                foreach (var outcome in bet.Outcomes)
                {
                    betPage.Outcomes.Add(outcome.Name);
                }

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
        // GET: /<controller>/Create/<id>
        [HttpGet]
        public ActionResult Join(long id)
        {

            var viewModel = new BetViewModel();
            using (var myWork = _factory.GetUOF())
            {
                var myBet = myWork.Bet.Get(id);
                foreach (var outcomes in myBet.Outcomes)
                {
                    viewModel.Outcomes.Add(outcomes.Name);
                }
                viewModel.Title = myBet.Name;
                viewModel.Description = myBet.Description;
                viewModel.MoneyPool = myBet.BuyIn;
                viewModel.Id = id;

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Join(BetViewModel model)
        {
            using (var myWork = _factory.GetUOF())
            {
                //throw new NotImplementedException();
                //TODO BetController JoinViewModel NullReference

                var user = myWork.User.Get(_userContext.Identity.Name);

                //Retrieves Bet from DB using BetId, then calls joinBet on retrieved Bet and adds user+selected outcome to Bet.
                myWork.Bet.Get(model.Id).joinBet(user, model.SelectedOutcome);
                myWork.Complete();

                return Redirect("/Lobby/List");
            }
        }
        // GET: /<controller>/Remove/<Lobby>/<Bet>
        [HttpGet]
        public ActionResult Remove(long Lobby, long Bet)
        {
            using (var myWork = _factory.GetUOF())
            {
                var myBet = myWork.Bet.Get(Bet);
                
                //TODO: Check is user is owner of lobby before removing!
                myWork.Bet.Remove(myBet);
                string baseUrl = Request.Url.GetLeftPart(UriPartial.Authority);
                return Redirect($"{baseUrl}/Lobby/Show/{Lobby}");
            }
        }
    }
}
