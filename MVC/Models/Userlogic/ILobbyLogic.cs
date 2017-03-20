using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Models.Userlogic
{
    interface ILobbyLogic
    {
        //Preconditon:
        //Modtager et UserID
        //Postcondition
        //Tilføjer User til Lobby Members.
        void addUserToLobby(long userID);

        //Preconditon:
        //Modtager et UserID
        //Postcondition
        //Tilføjer User til Lobbys Invited og Lobby til Users InvitedToLobbies.
        void inviteUsersToLobby(List<long> userIDs);

    }
}
