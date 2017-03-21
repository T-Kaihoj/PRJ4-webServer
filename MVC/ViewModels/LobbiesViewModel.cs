using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MVC.Models.Userlogic;

namespace MVC.ViewModels
{
    public class LobbiesViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IList<Lobby> MemberOfLobbies { get; set; }
        public IList<Lobby> InvitedToLobbies { get; set; }

        public LobbiesViewModel()
        {
            MemberOfLobbies = new List<Lobby>();
            InvitedToLobbies = new List<Lobby>();
        }

    }
}