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
        public ActionResult Show(int id)
        {
            try
            {
                var l = Lobby.Get(id);
                if (l == null)
                {
                    return Redirect("Lobby");
                }
                var viewModel = new LobbyViewModel();
                viewModel.ID = (int)l.LobbyID;
                viewModel.Name = "Hello Tobias";
                Bet bet = new Bet("100 meter run");

                
                Bet bet1 = new Bet("weightloss");

              
                viewModel.Bets.Add(bet1);


                return View(viewModel);
            }
            catch (Exception)
            {
                return Redirect("/Lobby");
                
            }

        }
    }
}
