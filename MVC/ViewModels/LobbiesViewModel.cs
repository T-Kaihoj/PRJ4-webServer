using System.Collections.Generic;

using Common.Models;

namespace MVC.ViewModels
{
    public class LobbiesViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IEnumerable<Lobby> MemberOfLobbies { get; set; }
        public IEnumerable<Lobby> InvitedToLobbies { get; set; }

        public LobbiesViewModel()
        {
            MemberOfLobbies = new List<Lobby>();
            InvitedToLobbies = new List<Lobby>();
        }
    }
}