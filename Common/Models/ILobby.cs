using System.Collections.Generic;

namespace DAL.Models
{
    public interface ILobby
    {
        long LobbyId { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        ICollection<IBet> Bets { get; set; }
        ICollection<IUser> MemberList { get; set; }
        ICollection<IUser> InvitedList { get; set; }
    }
}