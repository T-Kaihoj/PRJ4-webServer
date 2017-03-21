using System.Collections.Generic;

namespace DAL.Models
{
    public interface IUser
    {
        string Username { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        decimal Balance { get; set; }
        string Hash { get; set; }
        string Salt { get; set; }
        ICollection<ILobby> MemberOfLobbies { get; set; }
        ICollection<ILobby> InvitedToLobbies { get; set; }
        ICollection<IBet> Bets { get; set; }
        ICollection<IOutcome> Outcomes { get; set; }
    }
}