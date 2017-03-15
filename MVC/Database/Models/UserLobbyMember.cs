using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Database.Models
{

    // klasse til many to many relationship, som ikke er supported i Core :) :) :) :) :) :) :) :(

    public class UserLobbyMember
    {
        public string UserName { get; set; }
        public User User { get; set; }

        public long LobbyId { get; set; }
        public Lobby Lobby { get; set; }
    }
}
