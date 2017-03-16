using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserLogic;

namespace MVC.Models.Userlogic
{
    interface ILobbyInfo
    {
        // INFO ___________________________________________
        //Preconditon:
        //Modtager et LobbyID
        //Postcondition
        //Returner et lobby navn.
        string getLobbyName(long lobbyID);

        //Preconditon:
        //Modtager et LobbyID
        //Postcondition
        //Returner lobby deskribtionLobbyID.
        string getDescribtion(long lobbyID);

        //Precondition:
        //Modtager et lobbyID
        //Postcondition:
        //Returner lobbyens startdato
        string getStartDate(long lobbyID);

        //Precondition:
        //Modtager et lobbyID
        //Postcondition:
        //Returner lobbyens startdato
        string getEndDate(long lobbyID);

        //Precondition:
        //Modtager et lobbyID
        //Postcondition:
        //Returner lobbyens Judge
        User getJudge(long lobbyID);

        //Precondition:
        //Modtager et LobbyID
        //Postcondition
        //Returner en liste af Users som er medlem af lobbyen.
        List<User> getParticipants(long lobbyID);



    }
}
