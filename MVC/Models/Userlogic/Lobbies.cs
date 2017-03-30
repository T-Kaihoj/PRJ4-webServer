using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LobbyLogic;

namespace MVC.Models.Userlogic
{
    public class Lobbies
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<Lobby> MemberOfLobbies { get; set; }
        public List<Lobby> InvitedToLobbies { get; set; }

        public Lobbies()
        {
            MemberOfLobbies = new List<Lobby>();
            InvitedToLobbies = new List<Lobby>();
        }
        public static Lobbies getLobby(int id)
        {
            Lobbies newLobby = new Lobbies();

            Lobby l1 = new Lobby("Test lobby");
            newLobby.MemberOfLobbies.Add(l1);
            Lobby l2 = new Lobby("Test lobby 2");
            newLobby.MemberOfLobbies.Add(l2);

            return newLobby;
        }
    }
}