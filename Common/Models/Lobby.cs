using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Models
{
    public class Lobby
    {
        private string _name;
        private string _description;

        [Key]
        public long LobbyId { get; set; }

        public string Name
        {
            get { return _name; }
            set { _name = Utility.DatabaseSecure( value); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = Utility.DatabaseSecure( value); }
        }

        public virtual ICollection<Bet> Bets { get; set; }
        public virtual ICollection<User> MemberList { get; set; }
        public virtual ICollection<User> InvitedList { get; set; }
    }
}
