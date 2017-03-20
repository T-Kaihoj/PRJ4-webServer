using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC.Database.Models;

namespace MVC.ViewModels
{
    public class LobbyViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<User> Participants { get; set; }
        //Måske laves om til en liste af BetID'er?
        public List<Bet> Bets { get; set; }
    }
}