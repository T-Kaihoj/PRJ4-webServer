using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LobbyLogic;

namespace MVC.ViewModels
{
    public class LobbiesViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public List<Lobby> MemberOfLobbies { get; set; }
        public List<Lobby> InvitedToLobbies { get; set; }

        public LobbiesViewModel()
        {
            MemberOfLobbies = new List<Lobby>();
            InvitedToLobbies = new List<Lobby>();
        }

    }
}