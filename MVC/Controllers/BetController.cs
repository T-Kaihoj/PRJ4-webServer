using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Common;
using Common.Exceptions;
using Common.Models;
using MVC.Identity;
using MVC.ViewModels;

namespace MVC.Controllers
{
    [Authorize]
    public class BetController : BaseController
    {
        public BetController(IFactory factory , IUserContext userContext)
            : base(factory, userContext)
        {
            
        }
        
        #region Conclude

        // GET: /<controller>/Conclude/<id>
        public ActionResult Conclude(long id)
        {
            using (var myWork = GetUOF)
            {
                // Find the bet.
                var bet = myWork.Bet.Get(id);

                // Does the bet exist?
                if (bet == null)
                {
                    return HttpNotFound();
                }

                // Populate the viewmodel.
                var model = new ConcludeViewModel(bet);

                // Check access restrictions.
                if (GetUserName == bet.Judge.Username)
                {
                    return View("Conclude", model);
                }
            }

            // Error, redirect to homepage.
            return HttpForbidden();
        }

        // POST: /<controller>/Conclude/
        [HttpPost]
        public ActionResult Conclude(ConcludeViewModel model)
        {
            // Validate the model.
            TryValidateModel(model);

            // Ensure the id is valid.
            if (model.SelectedOutcome < 0)
            {
                using (var myWork = GetUOF)
                {
                    // Find the bet.
                    var bet = myWork.Bet.Get(model.BetId);

                    // Does the bet exist?
                    if (bet == null)
                    {
                        return HttpNotFound();
                    }

                    // Populate the viewmodel.
                    var newModel = new ConcludeViewModel(bet);

                    model.Description = newModel.Description;
                    model.Title = newModel.Title;
                    model.Outcomes = newModel.Outcomes;
                }

                ModelState.AddModelError("SelectedOutcome", Resources.Bet.ErrorSelectOutcomeRequired);
            }
            
            if (!ModelState.IsValid)
            {
                return View("Conclude", model);
            }

            using (var myWork = GetUOF)
            {
                // Locate the outcome.
                var outcome = myWork.Outcome.Get(model.SelectedOutcome);

                // Does the outcome exist?
                if (outcome == null)
                {
                    return HttpNotFound();
                }

                // Extract the bet.
                var bet = outcome.bet;

                // Get the current user.
                var user = myWork.User.Get(GetUserName);

                // Try to conclude the bet.
                bool result;

                try
                {
                    result = bet.ConcludeBet(user, outcome);
                }
                catch (UserNotJudgeException)
                {
                    return HttpForbidden();
                }

                // Is the bet already concluded?
                if (!result)
                {
                    throw new Exception(Resources.Bet.ExceptionBetAlreadyConcluded);
                }

                // Save the changes.
                myWork.Complete();

                return Redirect($"/Bet/Show/{bet.BetId}");
            }
        }

        #endregion

        #region Create

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
            // Perform general validation.
            TryValidateModel(viewModel);

            // Check the dates.
            if (viewModel.StopDateTime <= viewModel.StartDateTime)
            {
                ModelState.AddModelError("StopDate", Resources.Bet.ErrorEndDateBeforeStartDate);
            }

            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }

            using (var myWork = GetUOF)
            {
                // Create the bet.
                var bet = new Bet()
                {
                    BuyIn = viewModel.BuyInDecimal,
                    Description = viewModel.Description,
                    Name = viewModel.Title,
                    Owner = myWork.User.Get(GetUserName),
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
                // Get the lobby.
                var lobby = myWork.Lobby.Get(viewModel.LobbyId);
                if (!lobby.MemberList.Contains(bet.Judge))
                {
                    ModelState.AddModelError("Judge", Resources.Bet.ErrorJudgeIsNotMemberOfLobby);
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


                lobby.Bets.Add(bet);

                myWork.Complete();

                // Redirect to the bet page.
                return RedirectToRoute(new
                    {
                        controller = "Bet",
                        action = "Join",
                        id = bet.BetId
                    });
            }
        }

        #endregion

        #region Join

        // GET: /<controller>/Join/<id>
        [HttpGet]
        public ActionResult Join(long id)
        {
            var viewModel = new JoinBetViewModel();

            using (var myWork = GetUOF)
            {
                // Get the bet.
                var bet = myWork.Bet.Get(id);

                // Does the bet exist?
                if (bet == null)
                {
                    return HttpNotFound();
                }

                // Extract data.
                foreach (var outcomes in bet.Outcomes)
                {
                    var ovm = new OutcomeViewModel()
                    {
                        Id = outcomes.OutcomeId,
                        Name = outcomes.Name
                    };

                    viewModel.Outcomes.Add(ovm);
                }

                viewModel.Title = bet.Name;
                viewModel.Description = bet.Description;
                viewModel.MoneyPool = bet.BuyIn;
                viewModel.Id = id;
                viewModel.LobbyTitle = myWork.Lobby.Get(bet.Lobby.LobbyId).Name;
                viewModel.LobbyId = bet.Lobby.LobbyId;

                return View("Join", viewModel);
            }
        }

        // POST: /<controller>/Join/
        [HttpPost]
        public ActionResult Join(OutcomeViewModel model)
        {
            using (var myWork = GetUOF)
            {
                // Find the outcome in the database.
                var outcome = myWork.Outcome.Get(model.Id);

                // Does the outcome exist?
                if (outcome == null)
                {
                    return HttpNotFound();
                }

                // Get the current user.
                var user = myWork.User.Get(GetUserName);

                // Get the bet from the database.
                var bet = outcome.bet;

                // Join the bet.
                try
                {
                    bet.JoinBet(user, outcome);
                }
                catch (BetConcludedException)
                {
                    return View("Concluded");
                }

                myWork.Complete();

                return RedirectToAction("Show", new {id = bet.BetId});
            }
        }

        #endregion

        #region Remove

        // GET: /<controller>/Remove/<id>
        [HttpGet]
        public ActionResult Remove(long id)
        {
            using (var myWork = GetUOF)
            {
                // Locate the bet.
                var bet = myWork.Bet.Get(id);

                if (bet == null)
                {
                    return HttpNotFound();
                }

                // Is the current user the owner?
                if (GetUserName != bet.Owner.Username)
                {
                    return HttpForbidden();
                }

                // Are we past the start date?
                if (bet.StartDate < DateTime.Now)
                {
                    return HttpForbidden();
                }

                var lobbyId = bet.Lobby.LobbyId;

                // Remove the bet and redirect to the lobby.
                myWork.Bet.Remove(bet);
                myWork.Complete();

                return RedirectToRoute(new
                    {
                        controller = "Lobby",
                        action = "Show",
                        id = lobbyId
                    }
                );
            }
        }

        #endregion

        #region Show

        // GET: /<controller>/Show/<id>
        public ActionResult Show(long id)
        {
            using (var myWork = GetUOF)
            {
                // Find the bet.
                var bet = myWork.Bet.Get(id);

                // Check for the existence of the bet.
                if (bet == null)
                {
                    return HttpNotFound();
                }

                // Is the user a member of the bet?
                var currentUser = myWork.User.Get(GetUserName);

                if (!bet.Participants.Contains(currentUser))
                {
                    return HttpForbidden();
                }

                // Create the viewmodel, and copy over data.
                var viewmodel = new ShowBetViewModel()
                {
                    Description = bet.Description,
                    Judge = bet.Judge?.Username,
                    Title = bet.Name,
                    StartDate = bet.StartDate.ToLongDateString(),
                    StopDate = bet.StopDate.ToLongDateString(),
                    MoneyPool = bet.Pot,
                    LobbyTitle = myWork.Lobby.Get(bet.Lobby.LobbyId).Name,
                    LobbyId = bet.Lobby.LobbyId
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

                    // Is this outcome the winner?
                    if (bet.IsConcluded)
                    {
                        if (bet.Result == outcome)
                        {
                            vm.winner = true;
                        }
                        else
                        {
                            vm.loser = true;
                        }
                    }

                    viewmodel.Outcomes.Add(vm);
                }

                return View("Show", viewmodel);
            }
        }

        #endregion
    }
}
