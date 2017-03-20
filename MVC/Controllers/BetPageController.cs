using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVC.ViewModels;
using MVC.Models.Userlogic;


// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC.Controllers
{
    public class BetPageController : Controller
    {
        // GET: /<controller>/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show(int id)
        {
            var betPage = new BetViewModel();

            var bet = Bet.getBet(id);
                

            //betPage.Title = bet.BetTitle;
            betPage.Description = bet.Description;
            betPage.StartDate = bet.StartDate;
            betPage.EndDate = bet.EndDate;
            betPage.Judge = bet.Judge;
            for (int i =0; i< bet.Outcomes.Count ;i++)
            {
                betPage.Outcomes[i] = bet.Outcomes[i].Name;
            }
            
             
            return View(betPage);
        }
    }
}
