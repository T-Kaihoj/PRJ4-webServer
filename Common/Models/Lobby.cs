using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Lobby
    {
        [Key]
        public long LobbyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Bet> Bets { get; set; }
        public virtual ICollection<User> MemberList { get; set; }
        public virtual ICollection<User> InvitedList { get; set; }
    }
}
