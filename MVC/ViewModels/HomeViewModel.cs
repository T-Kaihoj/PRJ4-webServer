using System.Collections.Generic;

using Common.Models;

namespace MVC.ViewModels
{
    public class HomeViewModel
    {
        public int ID { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public decimal CurrentBalance { get; set; }
        public IEnumerable<Lobby> MemberOfLobbies { get; set; }
        public IEnumerable<Lobby> InvitedToLobbies { get; set; }
        public ICollection<Bet> Bets { get; set; }
        public ICollection<User> Friendlist { get; set; }

    }
}