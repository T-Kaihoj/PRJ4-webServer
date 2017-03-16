using System.Collections.Generic;
using BetLogic;
using MVC.Models.Userlogic;
using UserLogic;


namespace LobbyLogic
{
    public class Lobby
    {
        public long LobbyID { get; set; }
        public string Describtion { get; set; }
        public string LobbyName { get; set; }
        public List<User> Participants { get; set; }
        //Måske laves om til en liste af BetID'er?
        public List<Bet> Bets { get; set; }

        public static Lobby Get(int id)
        {
            var l = new Lobby();
            l.LobbyID = id;
            return l;
        }

        public void Persist()
        {
            //throw new System.NotImplementedException();
        }
    }
}