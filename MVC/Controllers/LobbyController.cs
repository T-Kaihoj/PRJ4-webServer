﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVC.Models.Userlogic;
using MVC.ViewModels;



// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC.Controllers
{
    public class LobbyController : Controller
    {
        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

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
