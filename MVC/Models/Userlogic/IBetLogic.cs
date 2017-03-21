using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Models.Userlogic
{
    interface IBetLogic
    {
        //Preconditon:
        //Modtager et UserID
        //Postcondition
        //Tilføjer User til Bet.
        void addUserToBet(long userID);

        //Preconditon:
        //Modtager et UserID
        //Postcondition
        //Sætter User til Judge af Bettet.
        void addJudgeToBet(long userID);

        //Preconditon:
        //Modtager et BetID
        //Postcondition
        //Returner Bet deskribtion.
        List<User> findWinners(long BetID);

        //Preconditon:
        //Modtager et BetID
        //Postcondition
        //Returner Bet deskribtion.
        User findWinner(long BetID);

    }
}
