using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class User : IUser
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
        public virtual ICollection<ILobby> MemberOfLobbies { get; set; }
        public virtual ICollection<ILobby> InvitedToLobbies { get; set; }
        public virtual ICollection<IBet> Bets { get; set; } 
        public virtual ICollection<IOutcome> Outcomes { get; set; }
    }
}
