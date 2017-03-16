using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Database.Models
{

    // klasse til many to many relationship, som ikke er supported i Core :) :) :) :) :) :) :) :(

    public class UserLobbyMember
    {
        // Sammensat primærnøgle
        [Key]
        [Column(Order = 1)]
        public string UserName { get; set; }

        public User User { get; set; }

        [Key]
        [Column(Order = 2)]
        public long LobbyId { get; set; }

        public Lobby Lobby { get; set; }
    }
}
