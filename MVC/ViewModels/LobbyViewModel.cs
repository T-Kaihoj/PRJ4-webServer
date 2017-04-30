using System.Collections.Generic;
using Common.Models;

namespace MVC.ViewModels
{
    public class LobbyViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> Participants { get; set; }
        public ICollection<Bet> ActiveBets { get; set; }
        public ICollection<Bet> InactiveBets { get; set; }
        public ICollection<User> InvitedUsers { get; set; }
    }
}