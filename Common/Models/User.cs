using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Username { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        //[Required]
        [Index(IsUnique = true)]
        [StringLength(200)]
        public string Email { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public string Hash { get; set; }

        [Required]
        public string Salt { get; set; }
        public virtual ICollection<Lobby> MemberOfLobbies { get; set; }
        public virtual ICollection<Lobby> InvitedToLobbies { get; set; }
        public virtual ICollection<Bet> Bets { get; set; } 
        public virtual ICollection<Outcome> Outcomes { get; set; }
    }
}
