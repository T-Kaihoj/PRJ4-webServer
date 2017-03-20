using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Database.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Username { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public virtual ICollection<Lobby> MemberOfLobbies { get; set; }
        public virtual ICollection<Lobby> InvitedToLobbies { get; set; }
        public virtual ICollection<Bet> Bets { get; set; } 
        public virtual ICollection<Outcome> Outcomes { get; set; }
    }
}
