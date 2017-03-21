using System.Collections.Generic;
using MVC.Models.Userlogic;

namespace MVC.ViewModels
{
    public class LobbyViewModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<User> Participants { get; set; }
        //Måske laves om til en liste af BetID'er?
        public List<Bet> Bets { get; set; }
    }
}