using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC.ViewModels
{
    public class BetViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MoneyPool { get; set; }
        public List<string> Users { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<string> Outcomes { get; set; } = new List<string>();
        
        //public List<Outcome> Outcomes { get; set; }

        public string Judge { get; set; }

        [HiddenInput]
        public long LobbyID { get; set; }
    }
}