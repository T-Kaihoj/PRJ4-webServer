using BetLogic;
using UserLogic;


namespace MVC.Models.Userlogic
{
    public class Lobby
    {
        public string LobbyName { get; set; }
        public string Description { get; set; }
        public List<User> Members { get; set; }
        //Måske laves om til en liste af BetID'er?
        public List<User> Invited { get; set; }
        public List<Bet> Bets { get; set; }

    }
}

