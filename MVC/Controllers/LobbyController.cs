using System;
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
        private IFactory _factory;
        private IUserContext _userContext;

        public LobbyController(IFactory factory, IUserContext userContext)
        {
            _factory = factory;
            _userContext = userContext;
        }

        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /<controller>/Create
        public ActionResult Create()
        {
            return View(new CreateLobbyViewModel());
        }


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
                lobby.InviteUserToLobby(user);

                myWork.Complete();

                return Redirect($"/Lobby/Invite/{lobby.LobbyId}");

            }
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
                    Description = viewModel.Description,
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

        // GET: /<controller>/List
        public ActionResult List()
        {
            using (var myWork = _factory.GetUOF())
            {
                // Get all lobbies, and convert to the domain model.
                var hej = User.Identity.Name;
                ;

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
            return Redirect($"/Lobby/List");
        }

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

        // GET: /<controller>/Show/<id>
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
                

                // Create a viewmodel for the lobby.
                var viewModel = new LobbyViewModel()
                {
                    ID = lobby.LobbyId,
                    Name = lobby.Name,
                    Description = lobby.Description,
                    Bets = lobby.Bets
                   
                };

                return View(viewModel);
            }
        }
        
    }
}
