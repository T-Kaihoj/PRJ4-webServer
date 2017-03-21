using System.Collections.Generic;
using System.Web.Mvc;

namespace MVC.Models.Userlogic
{
    public class BetPageViewModel
    {
        public BetPageViewModel()
        {
            Outcomes = new string[2];
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal MoneyPool { get; set; }
        public List<User> Users { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string[] Outcomes { get; set; }
        
        //public List<Outcome> Outcomes { get; set; }

        public string Judge { get; set; }

        [HiddenInput]
        public long LobbyID { get; set; }
    }
}