using System.Collections.Generic;
using Common.Models;

namespace MVC.ViewModels
{
    public class LobbyViewModel
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<User> Participants { get; set; }
        //Måske laves om til en liste af BetId'er?
        public ICollection<Bet> Bets { get; set; }
        public ICollection<User> InvitedUsers { get; set; }

    }
}