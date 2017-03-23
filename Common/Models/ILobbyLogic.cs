using System.Collections.Generic;

namespace Common.Models
{
    interface ILobbyLogic
    {
        //Preconditon:
        //Modtager et UserID
        //Postcondition
        //Tilføjer User til Lobby Members.
        void InviteUserToLobby(User User);

        //Preconditon:
        //Modtager et UserID
        //Postcondition
        //Tilføjer User til Lobbys Invited og Lobby til Users InvitedToLobbies.
        void InviteUserToLobby(List<User> Users);

        void AcceptLobby(User User);



    }
}
