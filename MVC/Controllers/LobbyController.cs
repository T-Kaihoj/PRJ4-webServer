using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Common;
using Common.Models;
using MVC.Identity;
using MVC.ViewModels;

namespace MVC.Controllers
{
    [Authorize]
    public class LobbyController : Controller
    {
        private readonly IFactory _factory;
        private readonly IUserContext _userContext;

        public LobbyController(IFactory factory, IUserContext userContext)
        {
            _factory = factory;
            _userContext = userContext;
        }

        #region Accept

        //GET: /<controller/Accept/<id>
        public ActionResult Accept(long id)
        {
            using (var myWork = _factory.GetUOF())
            {
                var lobby = myWork.Lobby.Get(id);

                if (lobby != null)
                {
                    var myUser = myWork.User.Get((_userContext.Identity.Name));
                    lobby.AcceptLobby(myUser);
                }

                myWork.Complete();
            }

            //TODO: More error handling?
            return Redirect("/Lobby/List");
        }

        #endregion

        #region Create

        // GET: /<controller>/Create
        public ActionResult Create()
        {
            return View(new CreateLobbyViewModel());
        }

        [HttpPost]
        // POST: /<controller>/Create
        public ActionResult Create(CreateLobbyViewModel viewModel)
        {
            using (var myWork = _factory.GetUOF())
            {
                // Perform data validation.
                if (!TryValidateModel(viewModel))
                {
                    // Errors, return and let the user handle it.
                    return View(viewModel);
                }

                // Create the domain lobby object.
                var lobby = new Lobby()
                {
                    Name = viewModel.Name,
                };

                // Save to the database.
                myWork.Lobby.Add(lobby);
                var aUser = myWork.User.Get(_userContext.Identity.Name);
                lobby.MemberList.Add(aUser);
                myWork.Complete();

                // Show the newly created lobby.
                return Redirect($"/Lobby/Show/{lobby.LobbyId}");
            }

            //TODO: Return error.
        }

        #endregion

        #region Index

        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region Invite

        [HttpGet]
        public ActionResult Invite(long id)
        {
            var viewModel = new InviteToLobbyViewModel()
            {
                Id = id
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Invite(InviteToLobbyViewModel viewModel)
        {
            using (var myWork = _factory.GetUOF())
            {
                var user = myWork.User.Get(viewModel.Username);
                var lobby = myWork.Lobby.Get(viewModel.Id);

                // Does the user exits?
                if (user != null)
                {
                    // Is the user already a member of the lobby?
                    if (lobby.MemberList.Contains(user))
                    {
                        ModelState.AddModelError("Username", Resources.Lobby.ErrorUserAlreadyInLobby);

                        return View("Invite", viewModel);
                    }

                    lobby.InviteUserToLobby(user);
                    myWork.Complete();
                }
                else
                {
                    // User doesn't exist.
                    ModelState.AddModelError("Username", Resources.Lobby.ErrorInvitedUserDoesNotExist);

                    return View("Invite", viewModel);
                }

                return Redirect($"/Lobby/Show/{lobby.LobbyId}");
            }
        }

        #endregion

        #region List

        // GET: /<controller>/List
        public ActionResult List()
        {
            using (var myWork = _factory.GetUOF())
            {
                var aUser = myWork.User.Get(_userContext.Identity.Name);

                // Display the lobbies.
                var viewModel = new LobbiesViewModel()
                {
                    MemberOfLobbies = aUser.MemberOfLobbies,
                    InvitedToLobbies = aUser.InvitedToLobbies
                };

                return View(viewModel);
            }   
        }

        #endregion

        #region Leave

        // GET: /<controller>/Show/<id>
        public ActionResult Leave(long id)
        {
            using (var myWork = _factory.GetUOF())
            {
                // Get the lobby from the database.
                var lobby = myWork.Lobby.Get(id);

                if (lobby == null)
                {
                    // Error.
                    throw new Exception("No such lobby");
                }

                lobby.RemoveMemberFromLobby(myWork.User.Get(_userContext.Identity.Name));
                myWork.Complete();
                return Redirect($"/Lobby/List");
            }
        }

        #endregion

        #region Remove

        // GET: /<controller>/Remove/<id>
        public ActionResult Remove(long id)
        {
            using (var myWork = _factory.GetUOF())
            {
                // Get the lobby from the database.
                var lobby = myWork.Lobby.Get(id);

                if (lobby == null)
                {
                    // Error.
                    throw new Exception("No such lobby");
                }

                lobby.RemoveLobby();

                myWork.Lobby.Remove(lobby);
                myWork.Complete();
                return Redirect("Lobby/List");
            }
        }

        #endregion

        #region Show

        // GET: /<controller>/Show/<id>
        public ActionResult Show(long id)
        {
            using (var myWork = _factory.GetUOF())
            {
                // Get the lobby from the database.
                var lobby = myWork.Lobby.Get(id);

                if (lobby == null)
                {
                    // Error.
                    throw new Exception("No such lobby");
                }
                List<Bet> myBets = new List<Bet>();
                foreach (var bet in lobby.Bets)
                {
                    var tempBet = myWork.Bet.GetEager(bet.BetId);
                    myBets.Add(tempBet);
                }

                // Create a viewmodel for the lobby.
                var viewModel = new LobbyViewModel()
                {
                    ID = lobby.LobbyId,
                    Name = lobby.Name,
                    Bets = myBets,
                    Participants = lobby.MemberList,
                    InvitedUsers = lobby.InvitedList
                };

                return View(viewModel);
            }
        }

        #endregion
    }
}
