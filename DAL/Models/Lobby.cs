using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Lobby : ILobby
    {
        [Key]
        public long LobbyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<IBet> Bets { get; set; }
        public virtual ICollection<IUser> MemberList { get; set; }
        public virtual ICollection<IUser> InvitedList { get; set; }
    }
}
