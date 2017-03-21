using System;
using System.Diagnostics;
using System.Web.Mvc;
using MVC.Models.Userlogic;
using MVC.ViewModels;

namespace MVC.Controllers
{
    public class LobbyController : Controller
    {
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

        [HttpPost]
        // POST: /<controller>/Create
        public ActionResult Create(CreateLobbyViewModel viewModel)
        {
            // Perform data validation.
            if (!TryValidateModel(viewModel))
            {
                // Errors, return and let the user handle it.
                return View(viewModel);
            }

            // Create the domain lobby object.
            var lobby = new Lobby(viewModel.Name)
            {
                Description = viewModel.Description
            };

            // Save to the database.
            lobby.Persist();

            // Show the newly created lobby.
            return Redirect($"/Lobby/Show/{lobby.LobbyID}");
        }

        // GET: /<controller>/Show/<id>
        public ActionResult Show(long id)
        {
            // Get the lobby from the database.
            var lobby = Lobby.Get(id);

            if (lobby == null)
            {
                // Error.
                throw new Exception("No such lobby");
            }

            // Create a viewmodel for the lobby.
            var viewModel = new LobbyViewModel()
            {
                ID = lobby.LobbyID,
                Name = lobby.LobbyName,
                Description = lobby.Description
            };

            return View(viewModel);
        }
    }
}
