﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Models;


namespace MVC.Models.Userlogic
{
    interface ILobbyInfo
    {
        // INFO ___________________________________________
        //Preconditon:
        //Modtager et LobbyId
        //Postcondition
        //Returner et lobby navn.
        string getLobbyName(long lobbyID);

        //Preconditon:
        //Modtager et LobbyId
        //Postcondition
        //Returner lobby describtion.
        string getDescribtion(long lobbyID);

       
        //Precondition:
        //Modtager et LobbyId
        //Postcondition
        //Returner en liste af Users som er medlem af lobbyen.
        List<User> getParticipants(long lobbyID);

        void Persist();
        List<Bet> getBets(long lobbyID);

    }
}
